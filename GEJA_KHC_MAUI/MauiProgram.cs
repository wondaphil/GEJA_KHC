using CommunityToolkit.Maui;

namespace GEJA_KHC_MAUI;

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
                fonts.AddFont("nyala.ttf", "Nyala");
                fonts.AddFont("EthiopicAbay-Light.ttf", "Abay");
                fonts.AddFont("EthiopicLessan-Light.ttf", "Lessan");
                fonts.AddFont("EthiopicSadiss-Light.ttf", "Sadiss");
            });

		return builder.Build();
	}
}
