namespace Tic_Tac_Toe.Services;

public class NavigationService : INavigationService
{
    public async Task InitializeAsync()
    {
        await NavigateToAsync("//MainPage");
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
