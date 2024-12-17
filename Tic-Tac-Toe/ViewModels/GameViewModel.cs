using System.Windows.Input;

namespace Tic_Tac_Toe.ViewModels;

public partial class GameViewModel : BaseViewModel
{
    INavigationService _navigationService;
    public ICommand LogoutCommand { get; private set; }

    public GameViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
        LogoutCommand = new Command(async () => await Logout());
    }

    async Task Logout()
    {
        await _navigationService.InitializeAsync();
    }
}
