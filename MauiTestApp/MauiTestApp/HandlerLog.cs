namespace MauiTestApp;

public record TrackedElement(string Name, WeakReference<VisualElement> Ref);

public static class HandlerLog
{
	public static List<TrackedElement> TrackedElements { get; } = [];
}
