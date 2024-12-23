using Microsoft.Extensions.Logging;

namespace Tic_Tac_Toe;

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

        builder.Services.AddSingleton<INavigationService, NavigationService>();

        builder.Services.AddTransient<LoginViewModel>();
        builder.Services.AddTransient<GameModeViewModel>();
        builder.Services.AddTransient<TurnViewModel>();
        builder.Services.AddTransient<GameViewModel>();

        

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
