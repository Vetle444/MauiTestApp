namespace MauiTestApp;

public partial class MainPage 
{
	private WeakReference m_weakRegularPage;

	public MainPage()
	{
		InitializeComponent();
	}

	private void Button_OnClicked(object? sender, EventArgs e)
	{
		var regularPage = new RegularPage();

		m_weakRegularPage = new WeakReference(regularPage);
		
		Navigation.PushModalAsync(new NavigationPage(regularPage));
	}

	/*private async void Button_OnClicked2(object? sender, EventArgs e)
	{
		for (int i = 0; i < 10; i++)
		{
			Label.Text = "GC collecting: " + i;
			GC.Collect();
			await Task.Delay(10);
		}
		
		Label.Text = m_weakRegularPage.IsAlive ? "TestPage is alive" : "TestPage is not alive";
	}*/

	private void Button_OnClicked3(object? sender, EventArgs e)
	{
		var regularPage = new RegularPage();

		m_weakRegularPage = new WeakReference(regularPage);
		
		Navigation.PushAsync(regularPage);
	}
}

