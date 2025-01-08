namespace Tic_Tac_Toe.ViewModels;

public partial class CellViewModel : BaseViewModel
{
    public int X { get; }
    public int Y { get; }

    public Color TopBorderColor { get; set; }
    public Color BottomBorderColor { get; set; }
    public Color LeftBorderColor { get; set; }
    public Color RightBorderColor { get; set; }

    [ObservableProperty]
    string cellValue;

    public CellViewModel(int x, int y)
    {
        X = x;
        Y = y;
    }
}
