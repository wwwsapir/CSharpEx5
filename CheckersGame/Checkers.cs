using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;

namespace CheckersGame
{
    public class Checkers
    {
        private const string k_First = "first";
        private const string k_Second = "second";

        private static readonly byte sr_MaxStrLength = 20;
        private CheckersGameBoard m_CheckersBoard;
        private List<Piece> m_Player1PiecesList;
        private List<Piece> m_Player2PiecesList;
        private PlayerInfo m_Player1;
        private PlayerInfo m_Player2;
        private Piece m_LastMovingPiece;

        public void InitializeBoard(byte i_BoardSize)
        {
            if (!CheckersGameBoard.IsBoardSizeValid(i_BoardSize))
            {
                throw new ArgumentException();
            }
            else
            {
                m_CheckersBoard = new CheckersGameBoard((CheckersGameBoard.eBoardSize)i_BoardSize);
            }
        }

        // Initializes the game - players and board size
        public void InitializeGameLogic(byte i_BoardSize, 
            string i_Player1Name,
            string i_Player2Name,
            bool i_IsPlayer2Human)
        {
            const bool v_Human = true;

            InitializeBoard(i_BoardSize);
            m_Player1 = new PlayerInfo(i_Player1Name, v_Human, ePlayerTag.First);
            m_Player2 = new PlayerInfo(i_Player2Name, i_IsPlayer2Human, ePlayerTag.Second);

            buildPlayersPiecesList();
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
                    moveValid &= i_CheckersGameBoard.BoardMatrix[i_PositionToExamine.Row, i_PositionToExamine.Col] == CheckersGameBoard.eCellMode.Empty;
                    Position? possibleCapturePosition = GetPossibleCapturePositionIfExists(i_PositionToExamine);

                    // If the new position is 2 cells far from the current one
                    if(possibleCapturePosition != null)
                    {
                        int middleRow = possibleCapturePosition.Value.Row;
                        int middleCol = possibleCapturePosition.Value.Col;

                        if (r_Owner == ePlayerTag.First)
                        {
                            moveValid &= i_CheckersGameBoard.BoardMatrix[middleRow, middleCol]
                                         == CheckersGameBoard.eCellMode.Player2Piece
                                         || i_CheckersGameBoard.BoardMatrix[middleRow, middleCol]
                                         == CheckersGameBoard.eCellMode.Player2King;
                        }
                        else
                        {
                            // if(r_Owner == ePlayerTag.Second)
                            moveValid &= i_CheckersGameBoard.BoardMatrix[middleRow, middleCol]
                                         == CheckersGameBoard.eCellMode.Player1Piece
                                         || i_CheckersGameBoard.BoardMatrix[middleRow, middleCol]
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

        // The game's main loop - handles the turns and end of game
        /*public void StartGame()
        {
            handlePlayersRounds();
            while (r_UserInterface.PlayersWantAnotherGameRound())
            {
                startANewGame();
            }           
        }*/
        
        private void startANewGame()
        {
            resetGame();
            handlePlayersRounds();       
        }

        private void resetGame()
        {
            m_CheckersBoard.InitializePiecesOnBoard();
            buildPlayersPiecesList();
        }

        private void handlePlayersRounds()
        {
            const bool v_FirstMoveThisTurn = true;    // Sending False in the first move, every turn
            bool gameContinue = true;

            while (gameContinue)
            {
                gameContinue = PlayRound(m_Player1, v_FirstMoveThisTurn);
                if (gameContinue)
                {
                    gameContinue = PlayRound(m_Player2, v_FirstMoveThisTurn);
                }
            }
        }   

        // One round in the game - Player1 then player2 turns
        public bool PlayRound(PlayerInfo i_CurrPlayerTurn, bool i_FirstMoveThisTurn)
        {
            bool v_OnlyCaptureMoves = true;
            Move? playerRequestMove;
            eAction nextAction = eAction.Continue;
            LinkedList<Move> legalMovesList;

            // if a capture move available, accept only capture move
            if (areLegalMovesExistForPlayer(i_CurrPlayerTurn, out legalMovesList, v_OnlyCaptureMoves, i_FirstMoveThisTurn))
            {   
                nextAction = getNextActionFromPlayer(i_CurrPlayerTurn, out playerRequestMove, legalMovesList);               
            }
            else if (i_FirstMoveThisTurn)
            {   
                // check if there are legal-non-capture moves, if so, get moves from player
                if (areLegalMovesExistForPlayer(i_CurrPlayerTurn, out legalMovesList, !v_OnlyCaptureMoves, i_FirstMoveThisTurn))
                {
                    nextAction = getNextActionFromPlayer(i_CurrPlayerTurn, out playerRequestMove, legalMovesList);
                }
            }

            nextAction = gameOver() ? eAction.GameOver : nextAction;
            bool willQuitGame = handleNextAction(i_CurrPlayerTurn, nextAction);

            return willQuitGame;
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
            const bool v_FirstMove = true;
            LinkedList<Move> legalMoves;
            o_Player1LegalMovesEmpty = !areLegalMovesExistForPlayer(m_Player1, out legalMoves, !v_OnlyCaptureMoves, v_FirstMove);
            o_Player2LegalMovesEmpty = !areLegalMovesExistForPlayer(m_Player2, out legalMoves, !v_OnlyCaptureMoves, v_FirstMove);
        }

        private eAction getNextActionFromPlayer(PlayerInfo i_CurrPlayerTurn, out Move? i_PlayerRequestMove, LinkedList<Move> i_LegalMovesList)
        {
            // get next action from player
            eAction returnedAction = i_CurrPlayerTurn.Human
                             ? getNextHumanAction(i_CurrPlayerTurn, out i_PlayerRequestMove, i_LegalMovesList)
                             : getMachineAction(out i_PlayerRequestMove, i_LegalMovesList);

            // apply movement if game is not over
            returnedAction = returnedAction != eAction.GameOver && returnedAction != eAction.Quit ?
                             applyMove(i_CurrPlayerTurn, i_PlayerRequestMove)
                             : returnedAction;

            return returnedAction;
        }

        private bool handleNextAction(PlayerInfo i_CurrPlayerTurn, eAction i_NextAction)
        {
            const bool v_FirstMove = true;
            bool continueGame;

            switch (i_NextAction)
            {
                case eAction.Capture:
                    // Recursive call to another move of the same player, this time only for the last played piece
                    continueGame = PlayRound(i_CurrPlayerTurn, !v_FirstMove);
                    break;
                case eAction.GameOver:
                    handleGameOver();
                    continueGame = false;
                    break;
                case eAction.Quit:
                    handleQuit(i_CurrPlayerTurn);
                    continueGame = false;
                    break;
                case eAction.Continue:
                    // Continue to next other player's turn
                    continueGame = true;
                    break;
                default:
                    throw new InvalidEnumArgumentException();
            }

            return continueGame;
        }

        // player pressed quit loses  the game and all his soldiers, here we update the score, and show game info
        private void handleQuit(PlayerInfo i_CurrPlayerTurn)
        {
            PlayerInfo winner = enemyPlayer(i_CurrPlayerTurn);

            // Player who quits -loses all his Pieces
            List<Piece> quittingPlayerPiecesList = getPlayerPiecesList(i_CurrPlayerTurn);
            quittingPlayerPiecesList.Clear();          
            addScore(winner);
            //r_UserInterface.ShowWinningState(winner, i_CurrPlayerTurn);                
        }

        // handle tie/ wiining situation
        private void handleGameOver()
        {
            bool player1LegalMovesEmpty, player2LegalMovesEmpty;
            checkIfPlayersHaveMoves(out player1LegalMovesEmpty, out player2LegalMovesEmpty);                      

            if (player1LegalMovesEmpty && player2LegalMovesEmpty)
            {   // Tie state
                PlayerInfo strongerPlayer = whichPlayerHaveMoreSoldiers();
                addScore(strongerPlayer);
                //r_UserInterface.ShowTieState(m_Player1, m_Player2);
            }
            else
            {   // Win state
                PlayerInfo winner = player1LegalMovesEmpty ? m_Player2 : m_Player1;
                addScore(winner);
                //r_UserInterface.ShowWinningState(winner, enemyPlayer(winner));                
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

        // randomly choosing a move
        private eAction getMachineAction(out Move? i_PlayerRequestMove, LinkedList<Move> i_LegalMovesList)
        {                     
            i_PlayerRequestMove = null;

            eAction nextAction = eAction.Continue;
            int listLength = i_LegalMovesList.Count;
            if (listLength != 0)
            {
                int randomListIndex = new Random().Next(0, listLength);
                i_PlayerRequestMove = i_LegalMovesList.ElementAt(randomListIndex);
            }

            return nextAction;
        }

        private eAction getNextHumanAction(PlayerInfo i_CurrPlayerTurn, out Move? i_PlayerRequestMove, LinkedList<Move> i_LegalMovesList)
        {/*
            // Continue loop until legal move given, or player wants to quit
            eAction nextAction = r_UserInterface.GetNextMove(i_CurrPlayerTurn, out i_PlayerRequestMove);
            while (nextAction != eAction.Quit && !i_LegalMovesList.Contains(i_PlayerRequestMove.Value))
            {
                r_UserInterface.ShowBadInputMessage();
                nextAction = r_UserInterface.GetNextMove(i_CurrPlayerTurn, out i_PlayerRequestMove);
            }           
            //TODO: Put the loop that gets the move in GUI class
            //TODO: GUI class should use a "isInputValid" function of Checkers to see if the loop continues
          * return nextAction;*/
            i_PlayerRequestMove = null;     //Test EXAMPLEEEEE!!!@#!@$#%#
            return eAction.Capture;     //Test EXAMPLEEEEE!!!@#!@$#%#
        }

        private bool areLegalMovesExistForPlayer(
            PlayerInfo i_CurrPlayerTurn,
            out LinkedList<Move> o_LegalMovesList, 
            bool i_OnlyCaptureMoves,
            bool i_IsFirstMove)
        {
            o_LegalMovesList = getLegalMovesList(i_CurrPlayerTurn, i_OnlyCaptureMoves, i_IsFirstMove);

            return o_LegalMovesList.Any();
        }

        // applying move,  returning whether a capture move happend
        private eAction applyMove(PlayerInfo i_CurrPlayerTurn, Move? i_Move)
        {
            eAction nextAction = eAction.Continue;
            Piece playingPiece = getPieceByPosition(i_CurrPlayerTurn, i_Move.Value.Source);
            Position capturedPiecePosition;

            // check if move is a capture move, output parameter get the captured piece position
            if (captureMove(playingPiece, i_Move.Value.Destination, out capturedPiecePosition))
            {
                PlayerInfo enemyPlayer = this.enemyPlayer(i_CurrPlayerTurn);
                removePieceFromPosition(enemyPlayer, capturedPiecePosition);
                m_LastMovingPiece = playingPiece;
                nextAction = eAction.Capture;
            }

            // updating movement
            playingPiece.MovePiece(i_Move.Value.Destination, m_CheckersBoard);
            //TODO: Move function in GUI that tells Checkers to move the piece
         
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
        private Piece getPieceByPosition(PlayerInfo i_CurrPlayerTurn, Position i_Position)
        {
            List<Piece> playerPieceList = getPlayerPiecesList(i_CurrPlayerTurn);
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
        private LinkedList<Move> getLegalMovesList(PlayerInfo i_CurrPlayerTurn, bool i_OnlyCaptureMoves, bool i_IsFirstMove)
        {
            LinkedList<Move> playerLegalMovesList = new LinkedList<Move>();
            List<Piece> currList = getPlayerPiecesList(i_CurrPlayerTurn);
            if (i_IsFirstMove)
            {
                foreach (var piece in currList)
                {
                    piece.AddLegalMovesToList(playerLegalMovesList, m_CheckersBoard, i_OnlyCaptureMoves);
                }
            }
            else
            {
                m_LastMovingPiece.AddLegalMovesToList(playerLegalMovesList, m_CheckersBoard, i_OnlyCaptureMoves);
            }

            return playerLegalMovesList;
        }
        
        // Returns the enemy playerInfo of a current player
        private PlayerInfo enemyPlayer(PlayerInfo i_CurrPlayerTurn)
        {
            return i_CurrPlayerTurn == m_Player1 ? m_Player2 : m_Player1;
        }

        private List<Piece> getPlayerPiecesList(PlayerInfo i_Player)
        {
            return i_Player == m_Player1 ? m_Player1PiecesList : m_Player2PiecesList;
        }
    }
}
