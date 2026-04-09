using System.Diagnostics;

namespace MauiTestApp;

public partial class MainPage
{
	public MainPage()
	{
		InitializeComponent();
		Debug.WriteLine("[MainPage] CREATED");
	}

	private async void OnSwapRootClicked(object? sender, EventArgs e)
	{
		Debug.WriteLine("========================================");
		Debug.WriteLine("[MainPage] Swapping root — clearing Shell.Items...");
		Debug.WriteLine("========================================");

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

		Debug.WriteLine("[MainPage] Navigation to root1 complete.");
		Debug.WriteLine("========================================");
		Debug.WriteLine("[MainPage] If child element handlers were properly disconnected,");
		Debug.WriteLine("[MainPage] you would see 'Handler DISCONNECTING' logs for MyLabel, MyEntry, etc.");
		Debug.WriteLine("========================================");
	}

	private async void OnForceGcClicked(object? sender, EventArgs e)
	{
		Debug.WriteLine("[MainPage] Forcing GC...");
		for (int i = 0; i < 5; i++)
		{
			GC.Collect();
			GC.WaitForPendingFinalizers();
			GC.Collect();
			await Task.Delay(100);
		}
		Debug.WriteLine("[MainPage] GC complete.");
	}

	private void OnPageHandlerChanging(object? sender, HandlerChangingEventArgs e)
	{
		if (e.OldHandler != null)
			Debug.WriteLine($"[MainPage] PAGE Handler DISCONNECTING (OldHandler: {e.OldHandler.GetType().Name})");
	}

	private void OnPageHandlerChanged(object? sender, EventArgs e)
	{
		if (Handler != null)
			Debug.WriteLine($"[MainPage] PAGE Handler CONNECTED ({Handler.GetType().Name})");
		else
			Debug.WriteLine("[MainPage] PAGE Handler set to NULL");
	}

	private void OnElementHandlerChanging(object? sender, HandlerChangingEventArgs e)
	{
		var element = (VisualElement)sender!;
		var name = GetElementName(element);

		if (e.OldHandler != null)
			Debug.WriteLine($"[MainPage] {name} Handler DISCONNECTING (OldHandler: {e.OldHandler.GetType().Name})");
	}

	private void OnElementHandlerChanged(object? sender, EventArgs e)
	{
		var element = (VisualElement)sender!;
		var name = GetElementName(element);

		if (element.Handler != null)
			Debug.WriteLine($"[MainPage] {name} Handler CONNECTED ({element.Handler.GetType().Name})");
		else
			Debug.WriteLine($"[MainPage] {name} Handler set to NULL");
	}

	private string GetElementName(VisualElement element)
	{
		if (element == MyLabel) return "MyLabel (Label)";
		if (element == MyEntry) return "MyEntry (Entry)";
		if (element == SwapRootButton) return "SwapRootButton (Button)";
		if (element == ForceGcButton) return "ForceGcButton (Button)";
		return element.GetType().Name;
	}

	~MainPage()
	{
		Debug.WriteLine("[MainPage] FINALIZED (garbage collected)");
	}
}
