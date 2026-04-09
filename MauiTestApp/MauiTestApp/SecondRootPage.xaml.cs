namespace MauiTestApp;

public partial class SecondRootPage
{
	public SecondRootPage()
	{
		InitializeComponent();
		LogLabel.Text = string.Join("\n", HandlerLog.Entries);
	}
}
