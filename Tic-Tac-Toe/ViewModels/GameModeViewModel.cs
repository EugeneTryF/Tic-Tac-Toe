using System.Windows.Input;

namespace Tic_Tac_Toe.ViewModels;

public partial class GameModeViewModel : BaseViewModel
{
    INavigationService _navigationService;
    public ICommand ChooseGameModeCommand { get; private set; }

    public string GetUserName => Preferences.Default.Get("tic-tac-toe-user", "User");

    public GameModeViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
        ChooseGameModeCommand = new Command(async () => await ChooseGameMode());
    }

    async Task ChooseGameMode()
    {
        await RedirectToNextPage();
    }

    async Task RedirectToNextPage()
    {
        await _navigationService.NavigateToAsync(nameof(TurnPage));
    }
}
