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
		if (HandlerLog.PreviousPageRef == null)
		{
			LogLabel.Text = "No previous page captured.";
			return;
		}

		if (!HandlerLog.PreviousPageRef.TryGetTarget(out var page))
		{
			LogLabel.Text = "MainPage has been garbage collected.";
			return;
		}

		var lines = new List<string>
		{
			$"MainPage alive: YES (not collected)",
			$"MainPage.Handler: {(page.Handler != null ? "STILL ACTIVE (leak!)" : "null (disconnected)")}",
			""
		};

		foreach (var child in GetAllChildren(page))
		{
			if (child is not VisualElement ve) continue;
			var name = ve.GetType().Name;
			if (ve.Handler != null)
				lines.Add($"  {name}.Handler: STILL ACTIVE (leak!)");
			else
				lines.Add($"  {name}.Handler: null (disconnected)");
		}

		LogLabel.Text = string.Join("\n", lines);
	}

	private static IEnumerable<IVisualTreeElement> GetAllChildren(IVisualTreeElement parent)
	{
		foreach (var child in parent.GetVisualChildren())
		{
			yield return child;
			foreach (var grandchild in GetAllChildren(child))
				yield return grandchild;
		}
	}
}
