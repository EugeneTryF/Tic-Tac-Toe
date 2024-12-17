using System.Windows.Input;

namespace Tic_Tac_Toe.ViewModels;

public partial class GameViewModel : BaseViewModel
{
    INavigationService _navigationService;
    public ICommand LogoutCommand { get; private set; }

    public string GetUserName => Preferences.Default.Get("tic-tac-toe-user", "User");

    public GameViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
        LogoutCommand = new Command(async () => await Logout());
    }

    async Task Logout()
    {
        Preferences.Default.Remove("tic-tac-toe-user");
        await RedirectToStartingPoint();
    }

    async Task RedirectToStartingPoint()
    {
        await _navigationService.InitializeAsync();
    }
}
