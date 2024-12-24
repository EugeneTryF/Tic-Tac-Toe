namespace Tic_Tac_Toe;

public partial class AppShell : Shell
{
    INavigationService _navigationService;

    public AppShell(INavigationService navigationService)
    {
        InitializeComponent();
        _navigationService = navigationService;

        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        Routing.RegisterRoute(nameof(GameModePage), typeof(GameModePage));
        Routing.RegisterRoute(nameof(TurnPage), typeof(TurnPage));
        Routing.RegisterRoute(nameof(GamePage), typeof(GamePage));

        Loaded += AppShell_Loaded;
    }

    private async void AppShell_Loaded(object sender, EventArgs e)
    {
        await _navigationService.InitializeAsync();
    }
}
