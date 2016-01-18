using System;

namespace CheckersGame
{
    public class CellChangedEventArgs : EventArgs
    {
        public Position CellPosition { get; set; }

        public CheckersGameBoard.eCellMode NewCellMode { get; set; }

        public CellChangedEventArgs(Position i_CellPosition, CheckersGameBoard.eCellMode i_CellMode)
        {
            if (!Enum.IsDefined(typeof(CheckersGameBoard.eCellMode), i_CellMode))
            {
                throw new ArgumentOutOfRangeException("i_CellMode");
            }

            CellPosition = i_CellPosition;
            NewCellMode = i_CellMode;
        }
    }
}
