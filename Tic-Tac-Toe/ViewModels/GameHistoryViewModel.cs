namespace Tic_Tac_Toe.ViewModels
{
    public class GameHistoryViewModel
    {
        public string Description { get; set; }
        public List<string> BoardState { get; set; } = new();
    }
}
