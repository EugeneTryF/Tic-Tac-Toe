using System.Windows.Input;

namespace Tic_Tac_Toe.ViewModels;

public partial class GameModeViewModel : BaseViewModel
{
    INavigationService _navigationService;
    public ICommand ChoosePvEGameModeCommand { get; private set; }
    public ICommand ChoosePvPGameModeCommand { get; private set; }

    public string GetUserName => Preferences.Default.Get("tic-tac-toe-user", "User");

    public GameModeViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
        ChoosePvEGameModeCommand = new Command(async () => await ChoosePvEGameMode());
        ChoosePvPGameModeCommand = new Command(async () => await ChoosePvPGameMode());
    }

    async Task ChoosePvEGameMode()
    {
        await RedirectToNextPage(new GameSettings { GameMode = "Vs Computer" });
    }

    async Task ChoosePvPGameMode()
    {
        await RedirectToNextPage(new GameSettings { GameMode = "Two Players" });
    }

    async Task RedirectToNextPage(GameSettings gameSettings)
    {
        await _navigationService.NavigateToAsync(nameof(TurnPage), new Dictionary<string, object>
        {
            { "GameSettings", gameSettings }
        });
    }
}
