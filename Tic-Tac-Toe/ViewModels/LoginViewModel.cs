using System.Windows.Input;

namespace Tic_Tac_Toe.ViewModels;

public partial class LoginViewModel : BaseViewModel
{
    INavigationService _navigationService;
    public ICommand RegisterCommand { get; private set; }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(UserNameIsNotEmpty))]
    string userName = string.Empty;

    public bool UserNameIsNotEmpty => UserName.Length > 0;

    public LoginViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
        RegisterCommand = new Command(async () => await RegisterUser());
    }

    async Task RegisterUser()
    {
        Preferences.Default.Set("tic-tac-toe-user", UserName);
        UserName = string.Empty;
        await RedirectToNextPage();
    }

    async Task RedirectToNextPage()
    {
        await _navigationService.NavigateToAsync(nameof(GameModePage));
    }
}
