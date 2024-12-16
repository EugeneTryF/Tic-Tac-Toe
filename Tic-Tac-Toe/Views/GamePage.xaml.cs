namespace Tic_Tac_Toe.Views;

public partial class GamePage : ContentPage
{
	public GamePage()
	{
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MainPage");
    }
}