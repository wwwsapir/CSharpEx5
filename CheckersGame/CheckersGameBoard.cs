using System.Collections.Generic;
using System;
using System.ComponentModel;

namespace CheckersGame
{
    public class CheckersGameBoard
    {
        public delegate void CellChangedEventHandler(object sender, CellChangedEventArgs e);
      
        private readonly eBoardSize r_BoardSize;
        private readonly GameMatrix r_BoardMatrix;
        private readonly byte[] r_NumOfInitialPieces = { 8, 12, 16 }; // respectively to eBoardSize :small/medium/large
        
        public event CellChangedEventHandler CellChanged
        {
            add
            {
                this.r_BoardMatrix.CellChanged += value;
            }

            remove
            {
                 this.r_BoardMatrix.CellChanged -= value;
            }
        }

        public enum eBoardSize
        {
            Small = 6,
            Medium = 8,
            Large = 10
        }

        public enum eCellMode
        {
            Empty = 0,
            Player1Piece = 1,
            Player2Piece = 2,
            Player1King = 3,
            Player2King = 4
        }

        private static bool isEven(int i_NumToCheck)
        {
            return i_NumToCheck % 2 != 0;
        }

        public eCellMode this[int i_X, int i_Y]
        {
            get
            {
                return this.r_BoardMatrix[i_X, i_Y];
            }
        }

        public eBoardSize BoardSize
        {
            get { return r_BoardSize; }
        }

        public class GameMatrix
        {
            private readonly eCellMode[,] r_BoardMatrix;

            public event CellChangedEventHandler CellChanged;

            public GameMatrix(int i_Rows, int i_Cols)
            {
                r_BoardMatrix = new eCellMode[i_Rows, i_Cols];
            }

            public eCellMode this[int i_X, int i_Y]
            {
                get
                {
                    return this.r_BoardMatrix[i_X, i_Y];
                }

                set
                {
                    this.r_BoardMatrix[i_X, i_Y] = value;
                    OnCellChanged(new CellChangedEventArgs(new Position(i_X, i_Y), value));
                }
            }

            protected virtual void OnCellChanged(CellChangedEventArgs e)
            {
                // check if someone is listening
                if (CellChanged != null)
                {
                    // raise the event:
                    CellChanged.Invoke(this, e);
                }
            }
        }

        // Checks if the given number can be a checkers board size
        public static bool IsBoardSizeValid(int i_TempBoardSize)
        {
            bool inputBoardSizeIsValid = Enum.IsDefined(typeof(eBoardSize), i_TempBoardSize);

            return inputBoardSizeIsValid;
        }

        public CheckersGameBoard(eBoardSize i_BoardSize)
        {
            r_BoardSize = i_BoardSize;
            r_BoardMatrix = new GameMatrix((int)r_BoardSize, (int)r_BoardSize);           
            InitializePiecesOnBoard();
        }

        // Puts the pieces on their initial places on the game board
        public void InitializePiecesOnBoard()
        {
            clearBoard(); // In case the board is not already empty - clear it
            int halfBoardSize = (int)r_BoardSize / 2;

            // Put the players' pieces on the board in the initial positions:
            for (int row = 0; row < (int)r_BoardSize; ++row)
            {
                int col = isEven(row) ? 0 : 1;
                if (row < halfBoardSize - 1)
                {
                    for (; col < (int)r_BoardSize; col += 2)
                    {
                        r_BoardMatrix[row, col] = eCellMode.Player1Piece;
                    }
                }
                else if (row > halfBoardSize)
                {
                    for (; col < (int)r_BoardSize; col += 2)
                    {
                        r_BoardMatrix[row, col] = eCellMode.Player2Piece;
                    }
                }
            }
        }

        private void clearBoard()
        {
            for (int row = 0; row < (int)r_BoardSize; ++row)
            {
                for (int col = 0; col < (int)r_BoardSize; ++col)
                {
                    r_BoardMatrix[row, col] = eCellMode.Empty;
                }
            }
        }

        public void MovePieceOnBoard(Position i_Source, Position i_Destination)
        {
            r_BoardMatrix[i_Destination.Row, i_Destination.Col] = r_BoardMatrix[i_Source.Row, i_Source.Col];
            r_BoardMatrix[i_Source.Row, i_Source.Col] = eCellMode.Empty;
        }

        public void ErasePieceFromBoard(Position i_CellToErasePosition)
        {
            r_BoardMatrix[i_CellToErasePosition.Row, i_CellToErasePosition.Col] = eCellMode.Empty;
        }

        // Fills the pieces lists of each player by checking the pieces positions on the board, only used in the beginning of a match
        public List<Checkers.Piece> BuildPlayerPiecesList(Checkers.ePlayerTag i_CurrentPlayer)
        {
            List<Checkers.Piece> listOfPieces = new List<Checkers.Piece>(getNumberOfInitialPieces());
            int cellIndex = 0;
            for (int row = 0; row < (int)r_BoardSize; ++row)
            {
                for (int col = 0; col < (int)r_BoardSize; ++col)
                {
                    if (isPieceBelongsToPlayer(r_BoardMatrix[row, col], i_CurrentPlayer))
                    {
                        listOfPieces.Add(new Checkers.Piece(i_CurrentPlayer));
                        listOfPieces[cellIndex++].CurrPosition = new Position(row, col);
                    }
                }
            }

            return listOfPieces;
        }

        // Used to check if the player should become a king
        public bool IsPieceOnEdgeOfBoard(Position i_CurrPiecePosition)
        {
            return i_CurrPiecePosition.Row == (int)r_BoardSize - 1 || i_CurrPiecePosition.Row == 0;
        }

        public void TurnManToKingOnBoard(Position i_CurrPiecePosition)
        {
            eCellMode currentMode = r_BoardMatrix[i_CurrPiecePosition.Row, i_CurrPiecePosition.Col];
            if (currentMode == eCellMode.Player1Piece)
            {
                r_BoardMatrix[i_CurrPiecePosition.Row, i_CurrPiecePosition.Col] = eCellMode.Player1King;
            }
            else
            { // currentMode == eCellMode.Player2Piece                
                r_BoardMatrix[i_CurrPiecePosition.Row, i_CurrPiecePosition.Col] = eCellMode.Player2King;
            }
        }

        // Used by the function that builds the pieces lists according to the positions on the board
        private bool isPieceBelongsToPlayer(eCellMode i_Cell, Checkers.ePlayerTag i_Player)
        {
            bool pieceBelongToPlayer;
            switch (i_Player)
            {
                    case Checkers.ePlayerTag.First:
                    pieceBelongToPlayer = i_Cell == eCellMode.Player1Piece || i_Cell == eCellMode.Player1King;
                    break;
                    case Checkers.ePlayerTag.Second:
                    pieceBelongToPlayer = i_Cell == eCellMode.Player2Piece || i_Cell == eCellMode.Player2King;
                    break;                 
                default:
                    throw new InvalidEnumArgumentException();
            }

            return pieceBelongToPlayer;
        }

        // Number of initial pieces for each player according to the board size
        private int getNumberOfInitialPieces()
        {
            int numOfInitialPiecesIndex;

            switch (r_BoardSize)
            {
                case eBoardSize.Small:
                    numOfInitialPiecesIndex = 0;  // 8 pieces
                    break;
                case eBoardSize.Medium:
                    numOfInitialPiecesIndex = 1; // 12 pieces
                    break;
                case eBoardSize.Large:
                    numOfInitialPiecesIndex = 2; // 16 pieces
                    break;                 
                default:
                    throw new InvalidEnumArgumentException();
            }

            return r_NumOfInitialPieces[numOfInitialPiecesIndex]; // Get the suitable value from the readonly array
        }

        public bool IsPositionOutOfBounds(Position i_PositionToExamine )
        {
            return i_PositionToExamine.Row >= (int)r_BoardSize || i_PositionToExamine.Row < 0
                   || i_PositionToExamine.Col >= (int)r_BoardSize || i_PositionToExamine.Col < 0;
        }
    }
}
