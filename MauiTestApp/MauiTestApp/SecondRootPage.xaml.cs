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
		for (int i = 0; i < 5; i++)
		{
			GC.Collect();
			GC.WaitForPendingFinalizers();
			GC.Collect();
			await Task.Delay(100);
		}
		RefreshLog();
	}

	private async void OnSwapBackClicked(object? sender, EventArgs e)
	{
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
		if (HandlerLog.TrackedElements.Count == 0)
		{
			LogLabel.Text = "No elements tracked yet.";
			return;
		}

		var lines = new List<string>();

		foreach (var tracked in HandlerLog.TrackedElements)
		{
			var disconnected = tracked.Element.Handler == null;
			lines.Add($"{tracked.Name}: {(disconnected ? "disconnected" : "not disconnected")}");
		}

		LogLabel.Text = string.Join("\n", lines);
	}
}
