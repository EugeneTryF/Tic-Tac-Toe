namespace Tic_Tac_Toe.ViewModels;

public partial class CellViewModel : BaseViewModel
{
    public int X { get; }
    public int Y { get; }

    [ObservableProperty]
    Color topBorderColor, bottomBorderColor, leftBorderColor, rightBorderColor;

    [ObservableProperty]
    string cellValue;

    public CellViewModel(int x, int y)
    {
        X = x;
        Y = y;
    }
}
