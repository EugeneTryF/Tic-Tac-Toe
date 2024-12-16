namespace Tic_Tac_Toe.Views;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(GameModePage), true, new Dictionary<string, object> { });
    }
}
