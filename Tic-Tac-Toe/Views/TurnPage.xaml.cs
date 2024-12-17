namespace Tic_Tac_Toe.Views;

public partial class TurnPage : ContentPage
{
    public TurnPage(TurnViewModel turnViewModel)
    {
        InitializeComponent();
        BindingContext = turnViewModel;
    }
}