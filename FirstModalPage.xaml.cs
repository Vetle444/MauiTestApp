namespace MauiTestApp;

public partial class FirstModalPage : ContentPage
{
	public FirstModalPage() : this(applyAttachedNavBarColors: true)
	{
	}

	public FirstModalPage(bool applyAttachedNavBarColors)
	{
		InitializeComponent();

		if (applyAttachedNavBarColors)
		{
			SetValue(NavigationPage.BarBackgroundColorProperty, Colors.White);
			SetValue(NavigationPage.BarTextColorProperty, Colors.Yellow);
		}
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		NavigationBarDiagnostics.Print(this, nameof(FirstModalPage));
	}

	private async void OnPushSecondPageClicked(object? sender, EventArgs e)
	{
		await Navigation.PushAsync(new SecondModalPage());
	}

	private async void OnCloseModalClicked(object? sender, EventArgs e)
	{
		await Navigation.PopModalAsync();
	}
}