using System.Diagnostics;

namespace MauiTestApp;

static class NavigationBarDiagnostics
{
	public static void Print(ContentPage page, string pageName)
	{
		var parentNavigationPage = page.Parent as NavigationPage;
		var lines = new[]
		{
			$"Page name: {pageName}",
			$"Parent NavigationPage.BarBackgroundColor: {Format(parentNavigationPage?.BarBackgroundColor)}",
			$"Parent NavigationPage.BarTextColor: {Format(parentNavigationPage?.BarTextColor)}",
			$"Page NavigationPage.BarBackgroundColorProperty value: {Format(page.GetValue(NavigationPage.BarBackgroundColorProperty))}",
			$"Page NavigationPage.BarTextColorProperty value: {Format(page.GetValue(NavigationPage.BarTextColorProperty))}"
		};

		foreach (var line in lines)
		{
			Debug.WriteLine(line);
			Console.WriteLine(line);
		}
	}

	static string Format(object? value)
	{
		return value?.ToString() ?? "<null>";
	}
}