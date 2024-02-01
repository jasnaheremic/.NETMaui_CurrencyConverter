using CommunityToolkit.Maui;
using KonverterValuta.Services;
using KonverterValuta.View;
using KonverterValuta.ViewModel;
using Microsoft.Extensions.Logging;

namespace KonverterValuta;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

        builder.Services.AddScoped<MainPage>();
        builder.Services.AddScoped<MainPageViewModel>();
        builder.Services.AddScoped<ICurrencyService, CurrencyService>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
