namespace Tic_Tac_Toe.ViewModels
{
    public partial class GameHistoryViewModel : BaseViewModel
    {
        [ObservableProperty]
        string description;

        [ObservableProperty]
        List<string> boardState;
    }
}
