using System.Diagnostics;

namespace MauiTestApp;

public sealed class FirstModalPage : ContentPage
{
	public FirstModalPage()
	{
		Title = "First";
		SetValue(NavigationPage.BarBackgroundColorProperty, Colors.Black);
		SetValue(NavigationPage.BarTextColorProperty, Colors.White);

		var pushSecondPageButton = new Button
		{
			Text = "Push second page"
		};
		pushSecondPageButton.Clicked += OnPushSecondPageClicked;

		var closeModalButton = new Button
		{
			Text = "Close modal"
		};
		closeModalButton.Clicked += OnCloseModalClicked;

		Content = new ScrollView
		{
			Content = new VerticalStackLayout
			{
				Padding = new Thickness(24),
				Spacing = 16,
				Children =
				{
					new Label
					{
						Text = "This page should have a black navigation bar with white title text.",
						FontSize = 18
					},
					pushSecondPageButton,
					closeModalButton
				}
			}
		};
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

public sealed class SecondModalPage : ContentPage
{
	public SecondModalPage()
	{
		Title = "Second";
		SetValue(NavigationPage.BarBackgroundColorProperty, Colors.Green);
		SetValue(NavigationPage.BarTextColorProperty, Colors.Black);

		var pushThirdPageButton = new Button
		{
			Text = "Push third page"
		};
		pushThirdPageButton.Clicked += OnPushThirdPageClicked;

		var popBackButton = new Button
		{
			Text = "Pop back"
		};
		popBackButton.Clicked += OnPopBackClicked;

		Content = new ScrollView
		{
			Content = new VerticalStackLayout
			{
				Padding = new Thickness(24),
				Spacing = 16,
				Children =
				{
					new Label
					{
						Text = "This page should have a green navigation bar with black title and back text.",
						FontSize = 18
					},
					pushThirdPageButton,
					popBackButton
				}
			}
		};
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		NavigationBarDiagnostics.Print(this, nameof(SecondModalPage));
	}

	private async void OnPushThirdPageClicked(object? sender, EventArgs e)
	{
		await Navigation.PushAsync(new ThirdModalPage());
	}

	private async void OnPopBackClicked(object? sender, EventArgs e)
	{
		await Navigation.PopAsync();
	}
}

public sealed class ThirdModalPage : ContentPage
{
	public ThirdModalPage()
	{
		Title = "Third";
		SetValue(NavigationPage.BarBackgroundColorProperty, Colors.Red);
		SetValue(NavigationPage.BarTextColorProperty, Colors.White);

		var popBackButton = new Button
		{
			Text = "Pop back"
		};
		popBackButton.Clicked += OnPopBackClicked;

		Content = new ScrollView
		{
			Content = new VerticalStackLayout
			{
				Padding = new Thickness(24),
				Spacing = 16,
				Children =
				{
					new Label
					{
						Text = "This page should have a red navigation bar with white title and back text.",
						FontSize = 18
					},
					popBackButton
				}
			}
		};
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		NavigationBarDiagnostics.Print(this, nameof(ThirdModalPage));
	}

	private async void OnPopBackClicked(object? sender, EventArgs e)
	{
		await Navigation.PopAsync();
	}
}

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