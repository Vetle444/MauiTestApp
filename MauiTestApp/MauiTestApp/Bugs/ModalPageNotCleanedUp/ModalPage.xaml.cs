namespace MauiTestApp.Bugs.ModalPageNotCleanedUp;

public partial class ModalPage
{
    public ModalPage()
    {
        InitializeComponent();
    }

    private async void OnClose_Clicked(object? sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}
