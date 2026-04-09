namespace MauiTestApp;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
	}

	protected override Window CreateWindow(IActivationState? activationState)
	{
		var shell = new Shell();
        var tabBar = new TabBar { Route = "root0" };
        var tab = new Tab();

        tab.Items.Add(new ShellContent()
        {
            ContentTemplate =
                new DataTemplate(() => new MainPage())
        });
        tabBar.Items.Add(tab);
        shell.Items.Add(tabBar);

        return new Window(shell);
	}
}