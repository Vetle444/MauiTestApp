namespace MauiTestApp;

public record TrackedElement(string Name, VisualElement Element);

public static class HandlerLog
{
	public static List<TrackedElement> TrackedElements { get; } = [];
}
