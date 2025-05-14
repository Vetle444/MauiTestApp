using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Handlers.Items2;

namespace MauiTestApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.ConfigureMauiHandlers(configure =>
		{
			configure.AddHandler<CollectionView, CollectionViewHandler2>();
		});
		
#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
