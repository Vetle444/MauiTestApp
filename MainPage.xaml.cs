namespace MauiTestApp;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

	private async void OnOpenNavigationPageColorsModalClicked(object? sender, EventArgs e)
	{
		await Navigation.PushModalAsync(
			new NavigationPage(new FirstModalPage(applyAttachedNavBarColors: false))
			{
				BarBackgroundColor = Colors.White,
				BarTextColor = Colors.Yellow
			});
	}

	private async void OnOpenAttachedPropertyModalClicked(object? sender, EventArgs e)
	{
		await Navigation.PushModalAsync(new NavigationPage(new FirstModalPage(applyAttachedNavBarColors: true)));
	}
}
