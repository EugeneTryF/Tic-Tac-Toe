namespace Tic_Tac_Toe.Views;

public partial class GameModePage : ContentPage
{
	public GameModePage()
	{
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//TurnPage");
    }
}