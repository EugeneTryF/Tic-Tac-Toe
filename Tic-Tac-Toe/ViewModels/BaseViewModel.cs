namespace Tic_Tac_Toe.ViewModels;

public partial class BaseViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(NotIsBusy))]
    bool isBusy;

    [ObservableProperty]
    string title = "";

    public bool NotIsBusy => !IsBusy;
}
