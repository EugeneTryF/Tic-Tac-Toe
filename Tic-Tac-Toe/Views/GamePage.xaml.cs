namespace Tic_Tac_Toe.Views;

public partial class GamePage : ContentPage
{
    public GamePage(GameViewModel gameViewModel)
    {
        InitializeComponent();
        BindingContext = gameViewModel;
    }

    private void OnGameModeChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        var selectedMode = (string)picker.SelectedItem;

        if (BindingContext is GameViewModel viewModel)
        {
            viewModel.SelectedGameMode = selectedMode;
        }
    }
}