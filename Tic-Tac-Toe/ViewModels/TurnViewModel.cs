using System.Windows.Input;

namespace Tic_Tac_Toe.ViewModels;

public partial class TurnViewModel : BaseViewModel
{
    INavigationService _navigationService;
    public ICommand ChooseTurnCommand { get; private set; }

    public string GetUserName => Preferences.Default.Get("tic-tac-toe-user", "User");

    public TurnViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
        ChooseTurnCommand = new Command(async () => await ChooseTurn());
    }

    async Task ChooseTurn()
    {
        await RedirectToNextPage();
    }

    async Task RedirectToNextPage()
    {
        await _navigationService.NavigateToAsync(nameof(GamePage));
    }
}
