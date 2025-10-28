namespace MauiTestApp;

public partial class MainPage 
{
	public MainPage()
	{
		InitializeComponent();
	}

	private void OnCollectionViewTestClicked(object? sender, EventArgs e)
	{
		Navigation.PushAsync(new CollectionViewTestPage());
	}

	private void OnCollectionViewRemoveTestClicked(object? sender, EventArgs e)
	{
		Navigation.PushAsync(new CollectionViewRemoveTestPage());
	}

	private void OnShowConnectedHandlers(object? sender, EventArgs e)
	{
		var labelsWithHandlers = HandlerTrackingLabel.GetLabelsWithConnectedHandlers();
		
		if (labelsWithHandlers.Count == 0)
		{
			ConnectedHandlersLabel.Text = "✓ No labels have connected handlers (all cleaned up!)";
			ConnectedHandlersLabel.TextColor = Colors.Green;
		}
		else
		{
			var details = string.Join("\n", labelsWithHandlers.Select(l => 
				$"Label #{l.InstanceId} - Text: '{l.Text}'"));
			
			ConnectedHandlersLabel.Text = $"⚠️ {labelsWithHandlers.Count} labels still have connected handlers:\n{details}";
			ConnectedHandlersLabel.TextColor = Colors.Red;
		}
	}

	private void OnForceCleanup(object? sender, EventArgs e)
	{
		var countBefore = HandlerTrackingLabel.GetLabelsWithConnectedHandlers().Count;
		HandlerTrackingLabel.CleanupAllConnectedHandlers();
		
		ConnectedHandlersLabel.Text = $"✓ Force disconnected {countBefore} handlers. Click 'Show' to verify.";
		ConnectedHandlersLabel.TextColor = Colors.Blue;
	}
}

