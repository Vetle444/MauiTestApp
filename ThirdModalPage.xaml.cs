namespace MauiTestApp;

public partial class ThirdModalPage : ContentPage
{
	public ThirdModalPage()
	{
		InitializeComponent();
		SetValue(NavigationPage.BarBackgroundColorProperty, Colors.Red);
		SetValue(NavigationPage.BarTextColorProperty, Colors.White);
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		NavigationBarDiagnostics.Print(this, nameof(ThirdModalPage));
	}

	private async void OnPopBackClicked(object? sender, EventArgs e)
	{
		await Navigation.PopAsync();
	}
}