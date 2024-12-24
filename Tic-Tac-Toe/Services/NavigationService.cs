namespace Tic_Tac_Toe.Services;

public class NavigationService : INavigationService
{
    public async Task InitializeAsync()
    {
        var nickname = Preferences.Get("tic-tac-toe-user", string.Empty);

        if (string.IsNullOrEmpty(nickname))
        {
            await NavigateToAsync("//MainPage");
        }
        else
        {
            await NavigateToAsync("//GameModePage");
        }
    }

    public async Task NavigateToAsync(string route, IDictionary<string, object> routeParameters = null)
    {
        if (routeParameters != null)
        {
            await Shell.Current.GoToAsync(route, routeParameters);
        }
        else
        {
            await Shell.Current.GoToAsync(route);
        }
    }
}
