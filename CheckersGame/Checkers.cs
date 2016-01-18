using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckersGame
{
    public delegate void InvalidMoveEventHandler(object sender, EventArgs e);

    public delegate void GameOverEventHandler(object sender, GameOverEventArgs e);

    public class Checkers
    {
        private static readonly byte sr_MaxStrLength = 20;
        private CheckersGameBoard m_CheckersBoard;
        private List<Piece> m_Player1PiecesList;
        private List<Piece> m_Player2PiecesList;
        private PlayerInfo m_Player1;
        private PlayerInfo m_Player2;
        private Piece m_LastMovingPiece;
        private PlayerInfo m_CurrPlayerTurn;
        private bool m_FirstMoveThisTurn = true;
        private LinkedList<Move> r_CurrentlyLegalMoves = new LinkedList<Move>();

        // Events:
        public event InvalidMoveEventHandler InvalidMoveGiven;

        public event GameOverEventHandler GameOver;

        public event CheckersGameBoard.CellChangedEventHandler CellChanged
        {
            add { this.m_CheckersBoard.CellChanged += value; }
            remove { this.m_CheckersBoard.CellChanged -= value; }
        }

        public void InitializeBoard(CheckersGameBoard.eBoardSize i_BoardSize)
        {
            if (!CheckersGameBoard.IsBoardSizeValid((byte)i_BoardSize))
            {
                throw new ArgumentException();
            }

            m_CheckersBoard = new CheckersGameBoard(i_BoardSize);
        }

        // Initializes the game - players and board size
        public void InitializeGameLogic(
            CheckersGameBoard.eBoardSize i_BoardSize, 
            string i_Player1Name,
            string i_Player2Name,
            bool i_IsPlayer2Human)
        {
            const bool v_Human = true;

            InitializeBoard(i_BoardSize);
            m_Player1 = new PlayerInfo(i_Player1Name, v_Human, ePlayerTag.First);
            m_Player2 = new PlayerInfo(i_Player2Name, i_IsPlayer2Human, ePlayerTag.Second);
            m_CurrPlayerTurn = m_Player1;
            buildPlayersPiecesList();
        }

        public CheckersGameBoard.eBoardSize BoardSize
        {
            get
            {
                return this.m_CheckersBoard.BoardSize;
            }
        }

        public enum ePlayerTag
        {
            First = 0,
            Second = 1
        }

        public enum eAction
        {
            Quit,
            Continue,
            Capture,
            GameOver
        }

        public string Player1Name
        {
            get { return m_Player1.Name; }
        }

        public string Player2Name
        {
            get { return m_Player2.Name; }
        }

        public static byte MaxStrLength
        {
            get
            {
                return sr_MaxStrLength;
            }
        }

        private void buildPlayersPiecesList()
        {
            if (m_Player1PiecesList != null && m_Player2PiecesList != null)
            {
                m_Player1PiecesList.Clear();
                m_Player2PiecesList.Clear();
            }

            m_Player1PiecesList = m_CheckersBoard.BuildPlayerPiecesList(ePlayerTag.First);
            m_Player2PiecesList = m_CheckersBoard.BuildPlayerPiecesList(ePlayerTag.Second);
        }

        public class Piece
        {
            private const int k_ManNumberOfPossibleMoves = 4;
            private const int k_KingNumberOfPossibleMoves = 8;
            private const int k_MaxNumberOfPossibleMoves = 8;
            private const uint k_KingPieceValue = 4;
            private const uint k_ManPieceValue = 1;
            private readonly ePlayerTag r_Owner;    // the number of the player that owns the piece

            private readonly Position[] r_PossibleMoves = new Position[k_MaxNumberOfPossibleMoves];
            private Position m_CurrPosition;        
            private bool m_King;        // True if the piece is on king mode     

            // Initializes the possible moves of every piece (addings to the currPosition)
            public Piece(ePlayerTag i_Owner)
            {
                r_Owner = i_Owner;
                m_King = false;

                // The first 4 position changes options are for men and kings, the last 4 are only for kings (walking backwards)
                // For player1 - men can only move down, for player2 - men can only move up.
                r_PossibleMoves[0] = (i_Owner == ePlayerTag.First) ? new Position(1, 1) : new Position(-1, 1);
                r_PossibleMoves[1] = (i_Owner == ePlayerTag.First) ? new Position(1, -1) : new Position(-1, -1);
                r_PossibleMoves[2] = (i_Owner == ePlayerTag.First) ? new Position(2, 2) : new Position(-2, 2);
                r_PossibleMoves[3] = (i_Owner == ePlayerTag.First) ? new Position(2, -2) : new Position(-2, -2);
                r_PossibleMoves[4] = (i_Owner == ePlayerTag.First) ? new Position(-1, 1) : new Position(1, 1);
                r_PossibleMoves[5] = (i_Owner == ePlayerTag.First) ? new Position(-1, -1) : new Position(1, -1);
                r_PossibleMoves[6] = (i_Owner == ePlayerTag.First) ? new Position(-2, 2) : new Position(2, 2);
                r_PossibleMoves[7] = (i_Owner == ePlayerTag.First) ? new Position(-2, -2) : new Position(2, -2);
            }

           public Position CurrPosition
            {
                get { return m_CurrPosition; }
                set { m_CurrPosition = value; }
            }

            public ePlayerTag Owner
            {
                get { return r_Owner; }
            }

            public uint PieceValue()
            {
                return m_King ? k_KingPieceValue : k_ManPieceValue;
            }

            // Goes over possible moves of a piece and adds it into the list if it's a valid move for this part of the round
            public void AddLegalMovesToList(
                LinkedList<Move> i_LegalMovesList,
                CheckersGameBoard i_CheckersGameBoard,
                bool i_OnlyCaptureMoves)
            {
                int maxIndexOfPossibleMove = m_King ? k_KingNumberOfPossibleMoves : k_ManNumberOfPossibleMoves;
                Position newPossiblePosition;
                bool captureMove;
                for (int i = 0; i < maxIndexOfPossibleMove; ++i)
                {
                    newPossiblePosition = CurrPosition.AddPosition(r_PossibleMoves[i]);
                    if (isMoveValid(newPossiblePosition, i_CheckersGameBoard, out captureMove)
                        && ((i_OnlyCaptureMoves && captureMove) || !i_OnlyCaptureMoves))                       
                    {                      
                            // Create a linkedList node and add it to the legal moves list - Source: currPosition, Dst: newLegalPosition
                            Move tempMove = new Move(m_CurrPosition, newPossiblePosition);
                            LinkedListNode<Move> tempNode = new LinkedListNode<Move>(tempMove);
                           
                            // If we only need capture moves and it's a capture move; or if we don't nessecarily need a capture move - add to list:
                            i_LegalMovesList.AddLast(tempNode);                         
                    }
                }
            }

            // Piece checks if a suggested move is valid
            private bool isMoveValid(Position i_PositionToExamine, CheckersGameBoard i_CheckersGameBoard, out bool o_IsMoveCapture)
            {
                o_IsMoveCapture = false;
                bool moveValid = !i_CheckersGameBoard.IsPositionOutOfBounds(i_PositionToExamine);
                if (moveValid)
                {
                    moveValid &= i_CheckersGameBoard[i_PositionToExamine.Row, i_PositionToExamine.Col] == CheckersGameBoard.eCellMode.Empty;
                    Position? possibleCapturePosition = GetPossibleCapturePositionIfExists(i_PositionToExamine);

                    // If the new position is 2 cells far from the current one
                    if(possibleCapturePosition != null)
                    {
                        int middleRow = possibleCapturePosition.Value.Row;
                        int middleCol = possibleCapturePosition.Value.Col;

                        if (r_Owner == ePlayerTag.First)
                        {
                            moveValid &= i_CheckersGameBoard[middleRow, middleCol]
                                         == CheckersGameBoard.eCellMode.Player2Piece
                                         || i_CheckersGameBoard[middleRow, middleCol]
                                         == CheckersGameBoard.eCellMode.Player2King;
                        }
                        else
                        {
                            // if(r_Owner == ePlayerTag.Second)
                            moveValid &= i_CheckersGameBoard[middleRow, middleCol]
                                         == CheckersGameBoard.eCellMode.Player1Piece
                                         || i_CheckersGameBoard[middleRow, middleCol]
                                         == CheckersGameBoard.eCellMode.Player1King;
                        }

                        // If the move is valid and distance is 2 cells - it's a capture move
                        o_IsMoveCapture = moveValid;
                    }
                }

                return moveValid;
            }

            public Position? GetPossibleCapturePositionIfExists(Position i_NextPosition)
            {
                Position? returnedValue = null;
                if (Math.Abs(m_CurrPosition.Row - i_NextPosition.Row) == 2)
                {
                    returnedValue = new Position(
                        (m_CurrPosition.Row + i_NextPosition.Row) / 2,
                        (m_CurrPosition.Col + i_NextPosition.Col) / 2);
                }

                return returnedValue;
            }

            public void MovePiece(Position i_DstPosition, CheckersGameBoard i_CheckersBoard)
            {
                i_CheckersBoard.MovePieceOnBoard(m_CurrPosition, i_DstPosition);
                CurrPosition = i_DstPosition;
                if (i_CheckersBoard.IsPieceOnEdgeOfBoard(m_CurrPosition) && !m_King)
                {
                    turnManToKing(i_CheckersBoard);
                }
            }

            private void turnManToKing(CheckersGameBoard i_CheckersBoard)
            {
                m_King = true;
                i_CheckersBoard.TurnManToKingOnBoard(m_CurrPosition);
            }

            public void RemovePiece(CheckersGameBoard i_CheckersBoard)
            {
                i_CheckersBoard.ErasePieceFromBoard(m_CurrPosition);
            }
        }

        public void ResetGame()
        {
            m_CheckersBoard.InitializePiecesOnBoard();
            buildPlayersPiecesList();
        }

        // Check if the game is over
        private bool gameOver()
        {
            bool player1LegalMovesEmpty, player2LegalMovesEmpty;
            checkIfPlayersHaveMoves(out player1LegalMovesEmpty, out player2LegalMovesEmpty);
            return player1LegalMovesEmpty || player2LegalMovesEmpty;
        }

        private void checkIfPlayersHaveMoves(out bool o_Player1LegalMovesEmpty, out bool o_Player2LegalMovesEmpty)
        {
            const bool v_OnlyCaptureMoves = true;
            o_Player1LegalMovesEmpty = !areLegalMovesExistForPlayer(m_Player1, !v_OnlyCaptureMoves);
            o_Player2LegalMovesEmpty = !areLegalMovesExistForPlayer(m_Player2, !v_OnlyCaptureMoves);
        }

        // handle tie/ winning situation
        private void handleGameOver()
        {
            const bool v_Tie = true;
            PlayerInfo strongerPlayer;
            bool player1LegalMovesEmpty, player2LegalMovesEmpty;
            checkIfPlayersHaveMoves(out player1LegalMovesEmpty, out player2LegalMovesEmpty);                      

            if (player1LegalMovesEmpty && player2LegalMovesEmpty)
            {   // Tie state
                strongerPlayer = whichPlayerHaveMoreSoldiers();
                addScore(strongerPlayer);
                OnGameOverOccured(new GameOverEventArgs(strongerPlayer, enemyPlayer(strongerPlayer), v_Tie));
            }
            else
            {   // Win state
                strongerPlayer = player1LegalMovesEmpty ? m_Player2 : m_Player1;
                addScore(strongerPlayer);
                OnGameOverOccured(new GameOverEventArgs(strongerPlayer, enemyPlayer(strongerPlayer), !v_Tie));               
            }
        }

        // considering a king equals more soldiers(4)
        private PlayerInfo whichPlayerHaveMoreSoldiers()
        {
            return (numOfSoldiers(m_Player1) - numOfSoldiers(m_Player2)) >= 0 ? m_Player1 : m_Player2;
        }

        private void addScore(PlayerInfo i_Player)
        {
            i_Player.Score += numOfSoldiers(i_Player) - numOfSoldiers(enemyPlayer(i_Player));
        }

        // considering king more soldiers
        private uint numOfSoldiers(PlayerInfo i_Player)
        {
            uint soldiersNumber = 0;
            List<Piece> pieceList = getPlayerPiecesList(i_Player);

            foreach (Piece piece in pieceList)
            {
                soldiersNumber += piece.PieceValue();
            }

            return soldiersNumber;
        }
       
        private bool areLegalMovesExistForPlayer(
            PlayerInfo i_PlayerToCheckMoves,
            bool i_OnlyCaptureMoves)
        {
            fillLegalMovesList(i_PlayerToCheckMoves, i_OnlyCaptureMoves);

            return r_CurrentlyLegalMoves.Any();
        }

        public void PlayRound(Move i_MoveToApply)
        {
	        if (isMoveValid(i_MoveToApply))
	        {
	            const bool v_OnlyCaptureMoves = true;
	            eAction lastAction = ApplyMove(i_MoveToApply);
                r_CurrentlyLegalMoves.Clear();
                m_LastMovingPiece.AddLegalMovesToList(r_CurrentlyLegalMoves, m_CheckersBoard, v_OnlyCaptureMoves);
	            if (lastAction == eAction.Capture && r_CurrentlyLegalMoves.Any()) 
	            {
                    // the player keeping the turn, but we update that it will not be the first move, as in series of capture moves
	                m_FirstMoveThisTurn = false;
	            }
	            else
	            {
                    // turn goes to the next player
	                m_CurrPlayerTurn = enemyPlayer(m_CurrPlayerTurn);
	                m_FirstMoveThisTurn = true;
	                if (!m_CurrPlayerTurn.Human)
	                {
	                    makeMachineMove();
	                }
	            }

	            if(gameOver())
		        {
			        handleGameOver();
		        }
	        }
	        else
	        {
	            OnInvalidMoveGiven(new EventArgs()); // show prompt dialog "Move is not Valid"
	        }
        }

        private bool isMoveValid(Move i_MoveToCheck)
        {
            const bool v_OnlyCaptureMoves = true;
	        if(!m_FirstMoveThisTurn)
            {	// in middle of a series of captures moves
                r_CurrentlyLegalMoves.Clear();
		    	m_LastMovingPiece.AddLegalMovesToList(r_CurrentlyLegalMoves, m_CheckersBoard, v_OnlyCaptureMoves);
	        }
	        else
	        {		
		        fillLegalMovesList(m_CurrPlayerTurn, v_OnlyCaptureMoves);   // Try to find only capture moves
	            if (!r_CurrentlyLegalMoves.Any())
	            {
	                fillLegalMovesList(m_CurrPlayerTurn, !v_OnlyCaptureMoves); // get all possible moves
	            }
	        }

            return r_CurrentlyLegalMoves.Contains(i_MoveToCheck);
        }

        private void makeMachineMove()
        {
            const bool v_OnlyCaptureMoves = true;
            if (m_FirstMoveThisTurn)
            {
                // int the middle of capture moves series
                fillLegalMovesList(m_CurrPlayerTurn, v_OnlyCaptureMoves);
                if (!r_CurrentlyLegalMoves.Any())
                {
                    fillLegalMovesList(m_CurrPlayerTurn, !v_OnlyCaptureMoves);
                }
            }
            else
            {
                m_LastMovingPiece.AddLegalMovesToList(r_CurrentlyLegalMoves, m_CheckersBoard, v_OnlyCaptureMoves);
            }

            if (r_CurrentlyLegalMoves.Any())
            {
                int randomListIndex = new Random().Next(0, r_CurrentlyLegalMoves.Count);
                Move machineCurrMove = r_CurrentlyLegalMoves.ElementAt(randomListIndex);
                eAction lastAction = ApplyMove(machineCurrMove);
                r_CurrentlyLegalMoves.Clear();
                m_LastMovingPiece.AddLegalMovesToList(r_CurrentlyLegalMoves, m_CheckersBoard, v_OnlyCaptureMoves);
                if (lastAction == eAction.Capture && r_CurrentlyLegalMoves.Any())
                {
                    m_FirstMoveThisTurn = false;
                    makeMachineMove();
                }
                else
                {
                    m_FirstMoveThisTurn = true;
                    m_CurrPlayerTurn = enemyPlayer(m_CurrPlayerTurn);
                }
            }
        }

        // applying move,  returning whether a capture move happend
        public eAction ApplyMove(Move? i_Move)
        {
            eAction nextAction = eAction.Continue;
            m_LastMovingPiece = getPieceByPosition(m_CurrPlayerTurn, i_Move.Value.Source);
            Position capturedPiecePosition;

            // check if move is a capture move, output parameter get the captured piece position
            if (captureMove(m_LastMovingPiece, i_Move.Value.Destination, out capturedPiecePosition))
            {
                PlayerInfo enemyPlayer = this.enemyPlayer(m_CurrPlayerTurn);
                removePieceFromPosition(enemyPlayer, capturedPiecePosition);
                nextAction = eAction.Capture;
            }

            // updating movement
            m_LastMovingPiece.MovePiece(i_Move.Value.Destination, m_CheckersBoard);
         
            return nextAction;
        }

        // check if for detenation position represent a capture move, if so- the output parameter update the captured piece Position
        private bool captureMove(Piece i_SrcPiece, Position i_LegalDstPosition, out Position i_CapturePosition)
        {
            i_CapturePosition = new Position(                           // Position will be relevant only if i_Capture_Position is true
            (i_SrcPiece.CurrPosition.Row + i_LegalDstPosition.Row) / 2,
            (i_SrcPiece.CurrPosition.Col + i_LegalDstPosition.Col) / 2);

            return Math.Abs(i_SrcPiece.CurrPosition.Row - i_LegalDstPosition.Row) == 2;
        }

        // Removes the piece from the current game
        private void removePieceFromPosition(PlayerInfo i_CurrPlayerTurn, Position i_PiecePosition)
        {
            List<Piece> playerPieceList = getPlayerPiecesList(i_CurrPlayerTurn);
            Piece pieceToRemove = getPieceByPosition(i_CurrPlayerTurn, i_PiecePosition);
            pieceToRemove.RemovePiece(m_CheckersBoard);
            playerPieceList.Remove(pieceToRemove);
        }

        // Finds the piece in the list using given position coordinates
        private Piece getPieceByPosition(PlayerInfo i_Player, Position i_Position)
        {
            List<Piece> playerPieceList = getPlayerPiecesList(i_Player);
            Piece wantedPiece = null;
            foreach (Piece currentPiece in playerPieceList)
            {
                if (currentPiece.CurrPosition.Equals(i_Position))
                {
                    wantedPiece = currentPiece;
                    break;
                }
            }
            
            return wantedPiece;
        }

        // Goes through the pieces list and inserts all legal moves of every piece into a list (altogether)
        private void fillLegalMovesList(PlayerInfo i_PlayerForLegalMoves, bool i_OnlyCaptureMoves)
        {
            r_CurrentlyLegalMoves.Clear();
            List<Piece> currList = getPlayerPiecesList(i_PlayerForLegalMoves);
            if (m_FirstMoveThisTurn)
            {
                foreach (Piece piece in currList)
                {
                    piece.AddLegalMovesToList(r_CurrentlyLegalMoves, m_CheckersBoard, i_OnlyCaptureMoves);
                }
            }
            else
            {
                m_LastMovingPiece.AddLegalMovesToList(r_CurrentlyLegalMoves, m_CheckersBoard, i_OnlyCaptureMoves);
            }
        }
        
        // Returns the enemy playerInfo of a current player
        private PlayerInfo enemyPlayer(PlayerInfo i_Player)
        {
            return i_Player == m_Player1 ? m_Player2 : m_Player1;
        }

        private List<Piece> getPlayerPiecesList(PlayerInfo i_Player)
        {
            return i_Player == m_Player1 ? m_Player1PiecesList : m_Player2PiecesList;
        }

        protected void OnInvalidMoveGiven(EventArgs e)
        {
            if (InvalidMoveGiven != null)
            {
                InvalidMoveGiven.Invoke(this, e);
            }
        }

        protected void OnGameOverOccured(GameOverEventArgs e)
        {
            if (GameOver != null)
            {
                GameOver.Invoke(this, e);
            }
        }
    }
}
