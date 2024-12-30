using Microsoft.Extensions.Logging;
using Tic_Tac_Toe.Elements;
using Tic_Tac_Toe.Handlers;

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
            })
            .ConfigureMauiHandlers(handlers =>
            {
                handlers.AddHandler<BorderedEntry, BorderedEntryHandler>();
            });

        builder.Services.AddSingleton<INavigationService, NavigationService>();
        builder.Services.AddSingleton<AppShell>();

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
