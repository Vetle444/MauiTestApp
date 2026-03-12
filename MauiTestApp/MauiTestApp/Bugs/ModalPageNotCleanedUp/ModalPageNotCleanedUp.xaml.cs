namespace MauiTestApp.Bugs.ModalPageNotCleanedUp;

public partial class ModalPageNotCleanedUp
{
    private List<(string Name, WeakReference Ref)>? _modalRefs;
    private List<(string Name, WeakReference Ref)>? _modalInNavRefs;

    public ModalPageNotCleanedUp()
    {
        InitializeComponent();
    }

    private static List<(string Name, WeakReference Ref)> CollectWeakRefs(Element root)
    {
        var refs = new List<(string Name, WeakReference Ref)>();
        CollectRecursive(root, refs);
        return refs;
    }

    private static void CollectRecursive(Element element, List<(string Name, WeakReference Ref)> refs)
    {
        var name = element.GetType().Name;
        if (element is IView view)
            name += string.IsNullOrEmpty(view.AutomationId) ? "" : $" ({view.AutomationId})";

        refs.Add((name, new WeakReference(element)));

        if (element.Handler?.PlatformView is { } platformView)
            refs.Add(($"  ↳ {platformView.GetType().Name} (platform)", new WeakReference(platformView)));

        foreach (var child in ((IVisualTreeElement)element).GetVisualChildren())
        {
            if (child is Element childElement)
                CollectRecursive(childElement, refs);
        }
    }

    private async void OnPushModal_Clicked(object? sender, EventArgs e)
    {
        var modalPage = new ModalPage();
        EventHandler handler = null!;
        handler = (_, _) =>
        {
            _modalRefs = CollectWeakRefs(modalPage);
            modalPage.Appearing -= handler;
        };
        modalPage.Appearing += handler;
        await Navigation.PushModalAsync(modalPage);
    }

    private async void OnPushModalInNavPage_Clicked(object? sender, EventArgs e)
    {
        var modalPage = new ModalPage();
        var navPage = new NavigationPage(modalPage);
        EventHandler handler = null!;
        handler = (_, _) =>
        {
            _modalInNavRefs = CollectWeakRefs(navPage);
            navPage.Appearing -= handler;
        };
        navPage.Appearing += handler;
        await Navigation.PushModalAsync(navPage);
    }

    private async void OnCheckCleanup_Clicked(object? sender, EventArgs e)
    {
        for (int i = 0; i < 10; i++)
        {
            await Task.Delay(10);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        var lines = new List<string>();

        if (_modalRefs != null)
        {
            lines.Add("── Modal Page ──");
            AppendLeakReport(_modalRefs, lines);
        }

        if (_modalInNavRefs != null)
        {
            lines.Add("");
            lines.Add("── Modal in NavigationPage ──");
            AppendLeakReport(_modalInNavRefs, lines);
        }

        if (lines.Count == 0)
            lines.Add("No modals have been pushed yet.");

        StatusLabel.Text = string.Join("\n", lines);
    }

    private static void AppendLeakReport(List<(string Name, WeakReference Ref)> refs, List<string> lines)
    {
        var leaked = refs.Where(r => r.Ref.IsAlive).ToList();
        var cleaned = refs.Where(r => !r.Ref.IsAlive).ToList();

        if (leaked.Count > 0)
        {
            lines.Add($"LEAKED ({leaked.Count}):");
            foreach (var item in leaked)
                lines.Add($"  • {item.Name}");
        }

        if (cleaned.Count > 0)
        {
            lines.Add($"Cleaned up ({cleaned.Count}):");
            foreach (var item in cleaned)
                lines.Add($"  • {item.Name}");
        }
    }
}
