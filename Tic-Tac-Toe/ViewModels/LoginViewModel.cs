using System.Windows.Input;

namespace Tic_Tac_Toe.ViewModels;

public partial class LoginViewModel : BaseViewModel
{
    INavigationService _navigationService;
    public ICommand RegisterCommand { get; private set; }

    [ObservableProperty]
    string userName;

    public LoginViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
        RegisterCommand = new Command(async () => await RegisterUser());
    }

    async Task RegisterUser()
    {
        await _navigationService.NavigateToAsync(nameof(GameModePage));
    }
}
