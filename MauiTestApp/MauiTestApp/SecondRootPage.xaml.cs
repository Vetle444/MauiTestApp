namespace MauiTestApp;

public partial class SecondRootPage
{
	public SecondRootPage()
	{
		InitializeComponent();
		RefreshLog();
	}

	private async void OnForceGcClicked(object? sender, EventArgs e)
	{
		HandlerLog.Add("Forcing GC...");
		for (int i = 0; i < 5; i++)
		{
			GC.Collect();
			GC.WaitForPendingFinalizers();
			GC.Collect();
			await Task.Delay(100);
		}
		HandlerLog.Add("GC complete.");
		RefreshLog();
	}

	private async void OnSwapBackClicked(object? sender, EventArgs e)
	{
		HandlerLog.Add("========================================");
		HandlerLog.Add("Swapping back to MainPage...");
		HandlerLog.Add("========================================");

		var tabBar = new TabBar { Route = "root0" };
		tabBar.Items.Add(new Tab
		{
			Items =
			{
				new ShellContent
				{
					ContentTemplate = new DataTemplate(() => new MainPage())
				}
			}
		});

		Shell.Current.Items.Clear();
		Shell.Current.Items.Add(tabBar);

		await Shell.Current.GoToAsync("//root0", true);
	}

	private void RefreshLog()
	{
		LogLabel.Text = string.Join("\n", HandlerLog.Entries);
	}
}
