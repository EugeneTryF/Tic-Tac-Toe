﻿using System.Windows.Input;

namespace Tic_Tac_Toe.ViewModels;

[QueryProperty(nameof(GameSettings), "GameSettings")]
public partial class GameViewModel : BaseViewModel
{
    INavigationService _navigationService;

    [ObservableProperty]
    GameSettings gameSettings;

    bool _isCross = true;

    public string Circle {  get; set; }
    public string Cross {  get; set; }

    string player1Symbol;
    string player2Symbol;
    string computerSymbol;

    [ObservableProperty]
    string currentTurn;

    [ObservableProperty]
    GameHistoryViewModel? selectedHistoryEntry;

    [ObservableProperty]
    ObservableCollection<CellViewModel> board = new();

    [ObservableProperty]
    ObservableCollection<GameHistoryViewModel> history = new();

    public ICommand CellClickCommand { get; private set; }
    public ICommand LogoutCommand { get; private set; }
    public ICommand NavigateToHistoryCommand { get; private set; }

    public string GetUserName => Preferences.Default.Get("tic-tac-toe-user", "User");

    public GameViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
        CellClickCommand = new Command(async (cell) => await CellClick((CellViewModel)cell));
        LogoutCommand = new Command(async () => await Logout());
        NavigateToHistoryCommand = new Command<GameHistoryViewModel>(async (entry) => await NavigateToHistory(entry));

        Application.Current.RequestedThemeChanged += OnThemeChanged;
    }

    private void OnThemeChanged(object sender, AppThemeChangedEventArgs e)
    {
        Circle = GetThemedImage("circle_light.png", "circle.png");
        Cross = GetThemedImage("cross_light.png", "cross.png");

        foreach (var cell in Board)
        {
            if (cell.CellValue == "circle.png" || cell.CellValue == "circle_light.png")
            {
                cell.CellValue = Circle;
            }
            else if (cell.CellValue == "cross.png" || cell.CellValue == "cross_light.png")
            {
                cell.CellValue = Cross;
            }
        }

        CurrentTurn = _isCross ? Cross : Circle;

        InitializeCellBorders();
    }

    void InitializeBoard()
    {
        Board.Clear();
        History.Clear();

        Circle = GetThemedImage("circle_light.png", "circle.png");
        Cross = GetThemedImage("cross_light.png", "cross.png");

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Board.Add(new CellViewModel(i, j));
            }
        }

        InitializeCellBorders();

        if (GameSettings.Turn == "X")
        {
            player1Symbol = Cross;
            player2Symbol = Circle;
            computerSymbol = Circle;
            _isCross = true;
            CurrentTurn = Cross;
        }
        else
        {
            player1Symbol = Circle;
            player2Symbol = Cross;
            computerSymbol = Cross;
            _isCross = true;
            CurrentTurn = Cross;

            if (GameSettings.GameMode == "Vs Computer")
            {
                ComputerMove();
            }
        }

        AddHistoryEntry("Game Start");
    }

    private string GetThemedImage(string lightImage, string darkImage)
    {
        return Application.Current.RequestedTheme == AppTheme.Dark ? lightImage : darkImage;
    }

    void InitializeCellBorders()
    {
        var grid = Board.GroupBy(c => c.X).Select(g => g.ToList()).ToList();

        for (int row = 0; row < grid.Count; row++)
        {
            for (int col = 0; col < grid[row].Count; col++)
            {
                var cell = grid[row][col];

                cell.TopBorderColor = row == 0 ? Colors.Transparent : GetOpositeThemeColor();
                cell.BottomBorderColor = row == grid.Count - 1 ? Colors.Transparent : GetOpositeThemeColor();
                cell.LeftBorderColor = col == 0 ? Colors.Transparent : GetOpositeThemeColor();
                cell.RightBorderColor = col == grid[row].Count - 1 ? Colors.Transparent : GetOpositeThemeColor();
            }
        }
    }

    private Color GetOpositeThemeColor()
    {
        return Application.Current.RequestedTheme == AppTheme.Dark ? Colors.White : Colors.Black;
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

        cell.CellValue = _isCross ? Cross : Circle;

        AddHistoryEntry($"Turn {History.Count} - {(player1Symbol == CurrentTurn ? GetUserName : GameSettings.GameMode == "Two Players" ? "Player 2" : "Computer")}");

        if (await CheckGameOver())
            return;

        _isCross = !_isCross;
        CurrentTurn = _isCross ? Cross : Circle;

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

            AddHistoryEntry($"Turn {History.Count} - Computer");

            if (await CheckGameOver())
                return;

            _isCross = !_isCross;
            CurrentTurn = _isCross ? Cross : Circle;
        }
    }

    void AddHistoryEntry(string description)
    {
        var boardState = Board.Select(cell => cell.CellValue).ToList();
        History.Add(new GameHistoryViewModel
        {
            Description = description,
            BoardState = boardState
        });
    }

    async Task NavigateToHistory(GameHistoryViewModel selectedEntry)
    {
        if (selectedEntry == null) return;

        int targetIndex = History.IndexOf(selectedEntry);

        for (int i = History.Count - 1; i > targetIndex; i--)
        {
            History.RemoveAt(i);
        }

        string[] newBoardState = new string[9];

        selectedEntry.BoardState.CopyTo(newBoardState);

        SelectedHistoryEntry = null;

        for (int i = 0; i < Board.Count; i++)
        {
            Board[i].CellValue = newBoardState[i];
        }

        _isCross = targetIndex % 2 == 0;
        CurrentTurn = _isCross ? Cross : Circle;

        if (GameSettings.GameMode == "Vs Computer" && CurrentTurn == computerSymbol)
        {
            await ComputerMove();
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

