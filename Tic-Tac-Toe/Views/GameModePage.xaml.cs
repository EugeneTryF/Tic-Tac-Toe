namespace Tic_Tac_Toe.Views;

public partial class GameModePage : ContentPage
{
    public GameModePage(GameModeViewModel gameModeViewModel)
    {
        InitializeComponent();
        BindingContext = gameModeViewModel;
    }
}