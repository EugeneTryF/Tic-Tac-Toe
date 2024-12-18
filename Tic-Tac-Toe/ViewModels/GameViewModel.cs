using System.Windows.Input;

namespace Tic_Tac_Toe.ViewModels;

public partial class GameViewModel : BaseViewModel
{
    INavigationService _navigationService;

    bool _isCross = true;
    string _circle = "circle.png";
    string _cross = "cross.png";

    [ObservableProperty]
    string currentTurn = "cross.png";

    [ObservableProperty]
    string[,] boardView = new string[3, 3];

    [ObservableProperty]
    string cell00, cell01, cell02, cell10, cell11, cell12, cell20, cell21, cell22;

    [ObservableProperty]
    string selectedGameMode;

    public List<string> GameModes { get; } = new List<string> { "Two Players", "Vs Computer" };

    public ICommand CellClickCommand { get; }
    public ICommand LogoutCommand { get; private set; }

    public string GetUserName => Preferences.Default.Get("tic-tac-toe-user", "User");

    public GameViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
        CellClickCommand = new Command(async (value) => await CellClick((string)value));
        LogoutCommand = new Command(async () => await Logout());
        SelectedGameMode = GameModes[0];
    }

    async Task CellClick(string coordinates)
    {
        if (IsCellOccupied(coordinates))
            return;

        int row = (int)(coordinates[0] - '0');
        int column = (int)(coordinates[1] - '0');

        BoardView[row, column] = _isCross ? _cross : _circle;

        await UpdateBoardState(coordinates);

        if (CheckGameOver(BoardView))
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
        var emptyCells = GetEmptyCells();
        if (emptyCells.Count > 0)
        {
            var randomCell = emptyCells[new Random().Next(emptyCells.Count)];
            int row = randomCell.Item1;
            int column = randomCell.Item2;

            BoardView[row, column] = _circle;

            await UpdateBoardState($"{row}{column}");

            if (CheckGameOver(BoardView))
                return;

            _isCross = true;
            CurrentTurn = _cross;
        }
    }

    bool IsCellOccupied(string coordinates)
    {
        int row = (int)(coordinates[0] - '0');
        int column = (int)(coordinates[1] - '0');
        return !string.IsNullOrEmpty(BoardView[row, column]);
    }

    List<(int, int)> GetEmptyCells()
    {
        var emptyCells = new List<(int, int)>();
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (string.IsNullOrEmpty(BoardView[i, j]))
                {
                    emptyCells.Add((i, j));
                }
            }
        }
        return emptyCells;
    }

    string CheckWinner(string[,] board)
    {
        for (int i = 0; i < 3; i++)
        {
            if (!string.IsNullOrEmpty(board[i, 0]) &&
                board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2])
            {
                return board[i, 0];
            }

            if (!string.IsNullOrEmpty(board[0, i]) &&
                board[0, i] == board[1, i] && board[1, i] == board[2, i])
            {
                return board[0, i];
            }
        }

        if (!string.IsNullOrEmpty(board[0, 0]) &&
            board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
        {
            return board[0, 0];
        }

        if (!string.IsNullOrEmpty(board[0, 2]) &&
            board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0])
        {
            return board[0, 2];
        }

        return null;
    }

    bool CheckGameOver(string[,] board)
    {
        var winner = CheckWinner(board);
        if (winner != null)
        {
            ShowWinner(winner == _cross ? "Player 1" : SelectedGameMode == "Two Players" ? "Player 2" : "Computer");
            return true;
        }

        if (GetEmptyCells().Count == 0)
        {
            ShowWinner("Nobody");
            return true;
        }

        return false;
    }

    public async Task ShowWinner(string winner)
    {
        await App.Current.MainPage.DisplayAlert("Game Over", $"{winner} wins!", "OK");
    }

    async Task UpdateBoardState(string coordinates)
    {
        switch (coordinates)
        {
            case "00": Cell00 = BoardView[0, 0]; break;
            case "01": Cell01 = BoardView[0, 1]; break;
            case "02": Cell02 = BoardView[0, 2]; break;

            case "10": Cell10 = BoardView[1, 0]; break;
            case "11": Cell11 = BoardView[1, 1]; break;
            case "12": Cell12 = BoardView[1, 2]; break;

            case "20": Cell20 = BoardView[2, 0]; break;
            case "21": Cell21 = BoardView[2, 1]; break;
            case "22": Cell22 = BoardView[2, 2]; break;
        }
    }

    async Task Logout()
    {
        Preferences.Default.Remove("tic-tac-toe-user");
        await RedirectToStartingPoint();
    }

    async Task RedirectToStartingPoint()
    {
        await _navigationService.InitializeAsync();
    }
}

