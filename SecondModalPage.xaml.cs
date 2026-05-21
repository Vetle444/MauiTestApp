namespace MauiTestApp;

public partial class SecondModalPage : ContentPage
{
	public SecondModalPage()
	{
		InitializeComponent();
		SetValue(NavigationPage.BarBackgroundColorProperty, Colors.Green);
		SetValue(NavigationPage.BarTextColorProperty, Colors.Black);
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		NavigationBarDiagnostics.Print(this, nameof(SecondModalPage));
	}

	private async void OnPushThirdPageClicked(object? sender, EventArgs e)
	{
		await Navigation.PushAsync(new ThirdModalPage());
	}

	private async void OnPopBackClicked(object? sender, EventArgs e)
	{
		await Navigation.PopAsync();
	}
}