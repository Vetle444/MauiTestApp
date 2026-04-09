using System.Diagnostics;

namespace MauiTestApp;

public partial class SecondRootPage
{
	public SecondRootPage()
	{
		InitializeComponent();
		Debug.WriteLine("[SecondRootPage] CREATED");
	}

	private async void OnForceGcClicked(object? sender, EventArgs e)
	{
		Debug.WriteLine("[SecondRootPage] Forcing GC...");
		for (int i = 0; i < 5; i++)
		{
			GC.Collect();
			GC.WaitForPendingFinalizers();
			GC.Collect();
			await Task.Delay(100);
		}
		Debug.WriteLine("[SecondRootPage] GC complete — check if MainPage was finalized.");
	}
}
