using System.Windows.Input;

namespace Tic_Tac_Toe.ViewModels;

public partial class TurnViewModel : BaseViewModel
{
    INavigationService _navigationService;
    public ICommand ChooseTurnCommand { get; private set; }

    public TurnViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
        ChooseTurnCommand = new Command(async () => await ChooseTurn());
    }

    async Task ChooseTurn()
    {
        await _navigationService.NavigateToAsync(nameof(GamePage));
    }
}
