namespace CheckersGame
{
    using System.Windows.Forms;

    public class Program
    {
        public static void Main()
        {
            FormGameBoard currFormGameBoard = new FormGameBoard();
            Application.EnableVisualStyles();
            currFormGameBoard.StartGame();
        }
    }
}
