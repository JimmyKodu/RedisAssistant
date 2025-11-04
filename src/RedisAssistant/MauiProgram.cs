using Microsoft.Extensions.Logging;
using RedisAssistant.Services;
using RedisAssistant.ViewModels;
using RedisAssistant.Views;

namespace RedisAssistant;

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

		// Services
		builder.Services.AddSingleton<IRedisService, RedisService>();

		// ViewModels
		builder.Services.AddSingleton<ConnectionsViewModel>();
		builder.Services.AddSingleton<KeysViewModel>();
		builder.Services.AddSingleton<MonitorViewModel>();

		// Views
		builder.Services.AddSingleton<ConnectionsPage>();
		builder.Services.AddSingleton<KeysPage>();
		builder.Services.AddSingleton<MonitorPage>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
