namespace MauiTestApp;

public static class HandlerLog
{
	public static List<string> Entries { get; } = [];

	public static void Add(string message)
	{
		Entries.Add(message);
		System.Diagnostics.Debug.WriteLine(message);
	}

	public static void Clear()
	{
		Entries.Clear();
	}
}
