using System.Windows.Input;

namespace Tic_Tac_Toe.ViewModels;

[QueryProperty(nameof(GameSettings), "GameSettings")]
public partial class GameViewModel : BaseViewModel
{
    INavigationService _navigationService;

    [ObservableProperty]
    GameSettings gameSettings;

    bool _isCross = true;
    string _circle = "circle.png";
    string _cross = "cross.png";

    [ObservableProperty]
    string currentTurn = "cross.png";

    [ObservableProperty]
    string selectedGameMode;

    [ObservableProperty]
    string selectedPlayerChoice = "Player 1 (X)";

    [ObservableProperty]
    string player1Symbol;

    [ObservableProperty]
    string player2Symbol;

    [ObservableProperty]
    string computerSymbol;

    [ObservableProperty]
    ObservableCollection<CellViewModel> board = new();

    public List<string> GameModes { get; } = new List<string> { "Two Players", "Vs Computer" };
    public List<string> PlayerChoices { get; } = new List<string> { "Player 1 (X)", "Player 2 (O)" };

    public ICommand CellClickCommand { get; private set; }
    public ICommand LogoutCommand { get; private set; }

    public string GetUserName => Preferences.Default.Get("tic-tac-toe-user", "User");

    public GameViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
        CellClickCommand = new Command(async (cell) => await CellClick((CellViewModel)cell));
        LogoutCommand = new Command(async () => await Logout());
        SelectedGameMode = GameModes[0];

        InitializeBoard();
    }

    void InitializeBoard()
    {
        Board.Clear();
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Board.Add(new CellViewModel(i, j));
            }
        }
        
        if (SelectedPlayerChoice == "Player 1 (X)")
        {
            Player1Symbol = _cross;
            Player2Symbol = _circle;
            ComputerSymbol = _circle;
            _isCross = true;
            CurrentTurn = _cross;
        }
        else
        {
            Player1Symbol = _circle;
            Player2Symbol = _cross;
            ComputerSymbol = _cross;
            _isCross = true;
            CurrentTurn = _cross;

            if (SelectedGameMode == "Vs Computer")
            {
                ComputerMove();
            }
        }
    }

    async Task CellClick(CellViewModel cell)
    {
        if (!string.IsNullOrEmpty(cell.CellValue))
            return;

        cell.CellValue = _isCross ? _cross : _circle;

        if (await CheckGameOver())
            return;

        _isCross = !_isCross;
        CurrentTurn = _isCross ? _cross : _circle;

        if (SelectedGameMode == "Vs Computer" && !_isCross)
        {
            await ComputerMove();
        }
    }

    async Task ComputerMove()
    {
        var emptyCells = Board.Where(c => string.IsNullOrEmpty(c.CellValue)).ToList();
        if (emptyCells.Count > 0)
        {
            var randomCell = emptyCells[new Random().Next(emptyCells.Count)];
            randomCell.CellValue = ComputerSymbol;

            if (await CheckGameOver())
                return;

            _isCross = true;
            CurrentTurn = _cross;
        }
    }

    string? CheckWinner()
    {
        var grid = Board.GroupBy(c => c.X).Select(g => g.ToList()).ToList();

        for (int i = 0; i < 3; i++)
        {
            if (!string.IsNullOrEmpty(grid[i][0].CellValue) &&
                grid[i][0].CellValue == grid[i][1].CellValue &&
                grid[i][1].CellValue == grid[i][2].CellValue)
            {
                return grid[i][0].CellValue;
            }

            if (!string.IsNullOrEmpty(grid[0][i].CellValue) &&
                grid[0][i].CellValue == grid[1][i].CellValue &&
                grid[1][i].CellValue == grid[2][i].CellValue)
            {
                return grid[0][i].CellValue;
            }
        }

        if (!string.IsNullOrEmpty(grid[0][0].CellValue) &&
            grid[0][0].CellValue == grid[1][1].CellValue &&
            grid[1][1].CellValue == grid[2][2].CellValue)
        {
            return grid[0][0].CellValue;
        }

        if (!string.IsNullOrEmpty(grid[0][2].CellValue) &&
            grid[0][2].CellValue == grid[1][1].CellValue &&
            grid[1][1].CellValue == grid[2][0].CellValue)
        {
            return grid[0][2].CellValue;
        }

        return null;
    }

    async Task<bool> CheckGameOver()
    {
        var winner = CheckWinner();
        if (winner != null)
        {
            await ShowWinner(winner == Player1Symbol ? "Player 1" : SelectedGameMode == "Two Players" ? "Player 2" : "Computer");
            return true;
        }

        if (Board.All(c => !string.IsNullOrEmpty(c.CellValue)))
        {
            await ShowWinner("Nobody");
            return true;
        }

        return false;
    }

    public async Task ShowWinner(string winner)
    {
        await App.Current.MainPage.DisplayAlert("Game Over", $"{winner} wins!", "OK");
        InitializeBoard();
    }

    async Task Logout()
    {
        Preferences.Default.Remove("tic-tac-toe-user");
        InitializeBoard();
        await RedirectToStartingPoint();
    }

    async Task RedirectToStartingPoint()
    {
        await _navigationService.InitializeAsync();
    }
}

