namespace MauiTestApp;

public partial class MainPage
{
	public MainPage()
	{
		InitializeComponent();
		HandlerLog.Clear();
		HandlerLog.Add("[MainPage] CREATED");
	}

	private async void OnSwapRootClicked(object? sender, EventArgs e)
	{
		HandlerLog.Add("========================================");
		HandlerLog.Add("Swapping root — clearing Shell.Items...");
		HandlerLog.Add("========================================");

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

	private void OnPageHandlerChanging(object? sender, HandlerChangingEventArgs e)
	{
		if (e.OldHandler != null)
			HandlerLog.Add($"PAGE Handler DISCONNECTED");
	}

	private void OnPageHandlerChanged(object? sender, EventArgs e)
	{
		if (Handler != null)
			HandlerLog.Add($"PAGE Handler CONNECTED");
		else
			HandlerLog.Add("PAGE Handler set to NULL");
	}

	private void OnElementHandlerChanging(object? sender, HandlerChangingEventArgs e)
	{
		var element = (VisualElement)sender!;
		var name = GetElementName(element);

		if (e.OldHandler != null)
			HandlerLog.Add($"{name} Handler DISCONNECTED");
	}

	private void OnElementHandlerChanged(object? sender, EventArgs e)
	{
		var element = (VisualElement)sender!;
		var name = GetElementName(element);

		if (element.Handler != null)
			HandlerLog.Add($"{name} Handler CONNECTED");
		else
			HandlerLog.Add($"{name} Handler set to NULL");
	}

	private string GetElementName(VisualElement element)
	{
		if (element == MyLabel) return "MyLabel (Label)";
		if (element == MyEntry) return "MyEntry (Entry)";
		if (element == SwapRootButton) return "SwapRootButton (Button)";
		if (element == ForceGcButton) return "ForceGcButton (Button)";
		return element.GetType().Name;
	}
}
