namespace MauiTestApp;

public partial class MainPage
{
	public MainPage()
	{
		InitializeComponent();
	}

	private async void OnSwapRootClicked(object? sender, EventArgs e)
	{
		HandlerLog.TrackedElements.Clear();
		HandlerLog.TrackedElements.Add(new("MainPage (ContentPage)", this));
		HandlerLog.TrackedElements.Add(new("MyLabel (Label)", MyLabel));
		HandlerLog.TrackedElements.Add(new("MyEntry (Entry)", MyEntry));

		var tabBar = new TabBar { Route = "root1" };
		tabBar.Items.Add(new Tab
		{
			Items =
			{
				new ShellContent
				{
					ContentTemplate = new DataTemplate(() => new SecondRootPage())
				}
			}
		});

		Shell.Current.Items.Clear();
		Shell.Current.Items.Add(tabBar);

		await Shell.Current.GoToAsync("//root1", true);
	}
}
