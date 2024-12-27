using System.Windows.Input;

namespace Tic_Tac_Toe.ViewModels;

[QueryProperty(nameof(GameSettings), "GameSettings")]
public partial class TurnViewModel : BaseViewModel
{
    INavigationService _navigationService;

    [ObservableProperty]
    GameSettings gameSettings;

    public ICommand ChooseTurnXCommand { get; private set; }
    public ICommand ChooseTurnOCommand { get; private set; }

    public string GetUserName => Preferences.Default.Get("tic-tac-toe-user", "User");

    public TurnViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
        ChooseTurnXCommand = new Command(async () => await ChooseTurnX());
        ChooseTurnOCommand = new Command(async () => await ChooseTurnO());
    }

    async Task ChooseTurnX()
    {
        GameSettings.Turn = "X";
        await RedirectToNextPage();
    }

    async Task ChooseTurnO()
    {
        GameSettings.Turn = "O";
        await RedirectToNextPage();
    }

    async Task RedirectToNextPage()
    {
        await _navigationService.NavigateToAsync(nameof(GamePage), new Dictionary<string, object>
        {
            { "GameSettings", GameSettings }
        });
    }
}
