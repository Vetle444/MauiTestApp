namespace MauiTestApp;

public partial class MainPage
{
    private readonly List<WeakReference> _pageRefs = [];

    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnPushLeakPage(object? sender, EventArgs e)
    {
        var page = new LeakPage();
        _pageRefs.Add(new WeakReference(page));
        await Navigation.PushAsync(page);
    }

    private async void OnPushLeakPageModal(object? sender, EventArgs e)
    {
        var page = new LeakPage();
        _pageRefs.Add(new WeakReference(page));
        await Navigation.PushModalAsync(new NavigationPage(page));
    }

    private async void OnForceGC(object? sender, EventArgs e)
    {
        for (int i = 0; i < 10; i++)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            await Task.Delay(100);
        }

        int alive = 0;
        int collected = 0;
        foreach (var wr in _pageRefs)
        {
            if (wr.IsAlive)
                alive++;
            else
                collected++;
        }

        var msg = $"Pages created: {_pageRefs.Count}, Still alive: {alive}, Collected: {collected}";
        System.Diagnostics.Debug.WriteLine(msg);
        StatusLabel.Text = msg;
    }
}
