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

    string player1Symbol;
    string player2Symbol;
    string computerSymbol;

    [ObservableProperty]
    string currentTurn = "cross.png";

    [ObservableProperty]
    ObservableCollection<CellViewModel> board = new();

    public ICommand CellClickCommand { get; private set; }
    public ICommand LogoutCommand { get; private set; }

    public string GetUserName => Preferences.Default.Get("tic-tac-toe-user", "User");

    public GameViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
        CellClickCommand = new Command(async (cell) => await CellClick((CellViewModel)cell));
        LogoutCommand = new Command(async () => await Logout());
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
        
        if (GameSettings.Turn == "X")
        {
            player1Symbol = _cross;
            player2Symbol = _circle;
            computerSymbol = _circle;
            _isCross = true;
            CurrentTurn = _cross;
        }
        else
        {
            player1Symbol = _circle;
            player2Symbol = _cross;
            computerSymbol = _cross;
            _isCross = true;
            CurrentTurn = _cross;

            if (GameSettings.GameMode == "Vs Computer")
            {
                ComputerMove();
            }
        }
    }

    partial void OnGameSettingsChanged(GameSettings value)
    {
        if (value != null)
        {
            InitializeBoard();
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

        if (GameSettings.GameMode == "Vs Computer")
        {
            await ComputerMove();
        }
    }

    async Task ComputerMove()
    {
        await Task.Delay(500);
        var emptyCells = Board.Where(c => string.IsNullOrEmpty(c.CellValue)).ToList();
        if (emptyCells.Count > 0)
        {
            var randomCell = emptyCells[new Random().Next(emptyCells.Count)];
            randomCell.CellValue = computerSymbol;

            if (await CheckGameOver())
                return;

            _isCross = !_isCross;
            CurrentTurn = _isCross ? _cross : _circle;
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
            await ShowWinner(winner == player1Symbol ? GetUserName : GameSettings.GameMode == "Two Players" ? "Player 2" : "Computer");
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

