using Tic_Tac_Toe.Enums;

namespace Tic_Tac_Toe.Services;

public class GameService
{
    bool _playerTwoIsComputer;
    bool _isPlayerOneTurn;
    GameSymbols _playerOneSymbol = GameSymbols.None;
    GameSymbols _playerTwoSymbol = GameSymbols.None;
    Random _random = new Random();

    public GameSymbols[,] _board = new GameSymbols[3, 3];

    public GameService()
    {
        
    }

    public void PlayerMove(int row, int column)
    {
        _board[row, column] = _playerTwoSymbol;

        _isPlayerOneTurn = true;
    }

    public (int Row, int Column)? ComputerMove()
    {
        var emptyCells = GetEmptyCells();

        if (emptyCells.Count == 0)
            return null;

        var randomMove = emptyCells[_random.Next(emptyCells.Count)];
        _board[randomMove.Row, randomMove.Column] = _playerTwoSymbol;

        _isPlayerOneTurn = true;
        return randomMove;
    }

    private List<(int Row, int Column)> GetEmptyCells()
    {
        var emptyCells = new List<(int Row, int Column)>();

        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                if (_board[i, j] == GameSymbols.None)
                    emptyCells.Add((i, j));

        return emptyCells;
    }

    public string CheckWinner(string[,] board)
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
}
