namespace Tic_Tac_Toe.Views;

public partial class MainPage : ContentPage
{
    public MainPage(LoginViewModel loginViewModel)
    {
        InitializeComponent();
        BindingContext = loginViewModel;
    }
}
