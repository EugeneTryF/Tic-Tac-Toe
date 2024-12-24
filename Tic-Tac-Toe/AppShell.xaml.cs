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

        Loaded += AppShell_Loaded;
    }

    private async void AppShell_Loaded(object sender, EventArgs e)
    {
        await NavigateBasedOnPreferences();
    }

    private async Task NavigateBasedOnPreferences()
    {
        var nickname = Preferences.Get("tic-tac-toe-user", string.Empty);

        if (string.IsNullOrEmpty(nickname))
        {
            await Shell.Current.GoToAsync("//MainPage");
        }
        else
        {
            await Shell.Current.GoToAsync("//GameModePage");
        }
    }
}
