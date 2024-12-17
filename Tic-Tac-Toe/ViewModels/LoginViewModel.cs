using System.Windows.Input;

namespace Tic_Tac_Toe.ViewModels;

public partial class LoginViewModel : BaseViewModel
{
    INavigationService _navigationService;
    public ICommand RegisterCommand { get; private set; }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(UserNameIsNotEmpty))]
    string userName = "";

    public bool UserNameIsNotEmpty => UserName.Length > 0;

    public LoginViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
        RegisterCommand = new Command(async () => await RegisterUser());

        bool hasKey = Preferences.Default.ContainsKey("tic-tac-toe-user");
        if (hasKey)
            _navigationService.NavigateToAsync(nameof(GameModePage));
    }

    async Task RegisterUser()
    {
        Preferences.Default.Set("tic-tac-toe-user", UserName);
        UserName = "";
        await RedirectToNextPage();
    }

    async Task RedirectToNextPage()
    {
        await _navigationService.NavigateToAsync(nameof(GameModePage));
    }
}
