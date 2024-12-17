namespace Tic_Tac_Toe;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        Routing.RegisterRoute(nameof(GameModePage), typeof(GameModePage));
        Routing.RegisterRoute(nameof(TurnPage), typeof(TurnPage));
        Routing.RegisterRoute(nameof(GamePage), typeof(GamePage));
    }
}
