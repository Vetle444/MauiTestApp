namespace MauiTestApp;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

	private async void OnOpenModalClicked(object? sender, EventArgs e)
	{
		await Navigation.PushModalAsync(
			new NavigationPage(new FirstModalPage())
			{
				BarBackgroundColor = Colors.Black,
				BarTextColor = Colors.White
			});
	}
}
