namespace MauiTestApp;

public partial class LeakPage : ContentPage
{
    private static int _instanceCount;
    private readonly int _id;

    public LeakPage()
    {
        InitializeComponent();
        _id = ++_instanceCount;
        System.Diagnostics.Debug.WriteLine($"LeakPage #{_id} CREATED. Total created: {_instanceCount}");
    }

    private void OnToolbarItemClicked(object? sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine($"LeakPage #{_id} toolbar item clicked.");
    }

    private async void OnCloseClicked(object? sender, EventArgs e)
    {
        if (Navigation.ModalStack.Count > 0)
            await Navigation.PopModalAsync();
        else
            await Navigation.PopAsync();
    }

    ~LeakPage()
    {
        System.Diagnostics.Debug.WriteLine($"LeakPage #{_id} FINALIZED (garbage collected).");
    }
}
