namespace Tic_Tac_Toe.Views;

public partial class TurnPage : ContentPage
{
	public TurnPage()
	{
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(GamePage), true, new Dictionary<string, object> { });
    }
}