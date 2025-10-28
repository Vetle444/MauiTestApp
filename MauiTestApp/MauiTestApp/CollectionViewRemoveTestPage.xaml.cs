using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MauiTestApp;

public partial class CollectionViewRemoveTestPage : ContentPage
{
    private RemoveTestViewModel _viewModel;
    private int _itemCounter = 0;
    
    public CollectionViewRemoveTestPage()
    {
        InitializeComponent();
        _viewModel = new RemoveTestViewModel();
        BindingContext = _viewModel;
    }

    private void OnAddItem(object? sender, EventArgs e)
    {
        _itemCounter++;
        _viewModel.Items.Add(new SimpleItem 
        { 
            Name = $"Item {_itemCounter}",
            Description = $"Added at {DateTime.Now:HH:mm:ss}"
        });
        
        StatusLabel.Text = $"Status: Added item {_itemCounter}. Total items: {_viewModel.Items.Count}";
    }

    private void OnRemoveFirstItem(object? sender, EventArgs e)
    {
        if (_viewModel.Items.Count > 0)
        {
            var item = _viewModel.Items[0];
            _viewModel.Items.RemoveAt(0);
            StatusLabel.Text = $"Status: Removed '{item.Name}'. Remaining items: {_viewModel.Items.Count}";
        }
        else
        {
            StatusLabel.Text = "Status: No items to remove";
        }
    }

    private void OnClearItems(object? sender, EventArgs e)
    {
        var count = _viewModel.Items.Count;
        _viewModel.Items.Clear();
        StatusLabel.Text = $"Status: Cleared {count} items. Handlers should be disconnected.";
    }

    private async void OnCheckHandlers(object? sender, EventArgs e)
    {
        StatusLabel.Text = "Status: Running GC...";
        
        for (int i = 0; i < 5; i++)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            await Task.Delay(100);
        }
        
        StatusLabel.Text = "Status: GC completed. Check console output for handler disconnection messages.";
    }
}

public class RemoveTestViewModel : INotifyPropertyChanged
{
    public ObservableCollection<SimpleItem> Items { get; set; }

    public RemoveTestViewModel()
    {
        Items = new ObservableCollection<SimpleItem>
        {
            new SimpleItem { Name = "Item 1", Description = "Initial item 1" },
            new SimpleItem { Name = "Item 2", Description = "Initial item 2" },
            new SimpleItem { Name = "Item 3", Description = "Initial item 3" },
            new SimpleItem { Name = "Item 4", Description = "Initial item 4" },
            new SimpleItem { Name = "Item 5", Description = "Initial item 5" },
        };
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

public class SimpleItem : INotifyPropertyChanged
{
    private string _name = string.Empty;
    private string _description = string.Empty;

    public string Name
    {
        get => _name;
        set
        {
            if (_name != value)
            {
                _name = value;
                OnPropertyChanged();
            }
        }
    }

    public string Description
    {
        get => _description;
        set
        {
            if (_description != value)
            {
                _description = value;
                OnPropertyChanged();
            }
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
