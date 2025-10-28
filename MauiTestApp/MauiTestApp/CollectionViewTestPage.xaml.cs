using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MauiTestApp;

public partial class CollectionViewTestPage : ContentPage
{
    private CollectionViewViewModel _viewModel;
    
    public CollectionViewTestPage()
    {
        InitializeComponent();
        _viewModel = new CollectionViewViewModel();
        BindingContext = _viewModel;
    }

    private void OnSwitchToTemplateB(object? sender, EventArgs e)
    {
        // Change all items to use Template B only
        foreach (var item in _viewModel.Items)
        {
            item.IsActive = false;
        }

        _viewModel.Refresh();
        
        StatusLabel.Text = "Status: All items using Template B (Template A should disconnect handlers)";
    }

    private void OnSwitchToMixed(object? sender, EventArgs e)
    {
        // Set first two items to use Template A
        for (int i = 0; i < Math.Min(2, _viewModel.Items.Count); i++)
        {
            _viewModel.Items[i].IsActive = true;
        }
        
        _viewModel.Refresh();
        
        StatusLabel.Text = "Status: Mixed templates active";
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

public class CollectionViewViewModel : INotifyPropertyChanged
{
    public List<TestItem> Items { get; set; }

    public CollectionViewViewModel()
    {
        Items =
        [
            new TestItem { Name = "Item 1", IsActive = true },
            new TestItem { Name = "Item 2", IsActive = true },
            new TestItem { Name = "Item 3", IsActive = false },
            new TestItem { Name = "Item 4", IsActive = false },
            new TestItem { Name = "Item 5", IsActive = false }
        ];
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public void Refresh()
    {
        Items = Items.ToList();
        OnPropertyChanged(nameof(Items));
    }
}

public class TestItem : INotifyPropertyChanged
{
    private string _name = string.Empty;
    private bool _isActive;

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

    public bool IsActive
    {
        get => _isActive;
        set
        {
            if (_isActive != value)
            {
                _isActive = value;
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

public class ItemTemplateSelector : DataTemplateSelector
{
    public DataTemplate? TemplateA { get; set; }
    public DataTemplate? TemplateB { get; set; }

    protected override DataTemplate? OnSelectTemplate(object item, BindableObject container)
    {
        if (item is TestItem testItem)
        {
            return testItem.IsActive ? TemplateA : TemplateB;
        }
        return TemplateB;
    }
}
