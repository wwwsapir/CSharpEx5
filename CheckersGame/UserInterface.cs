namespace CheckersGame
{
    using System;

    internal class UserInterface
    {
        private const char k_Delimiter = '>';
        private const byte k_FormatSizeOfMoveStr = 5; // "Af>Be": total should be 5 chars, after trimming
        private const string k_YesStr = "Y";
        private const string k_NoStr = "N";
        private const int k_MaxNumOfDigitsInInput = 10;
        private const char k_SeperatingCharSign = '=';
        private const char k_QuitChar = 'Q';

        private readonly char[] r_PiecesSymbols = { ' ', 'O', 'X', 'U', 'K' };

        internal bool IsSecondPlayerHuman()
        {
            return askUserBooleanQuestion("Is this a two-players game? ");
        }

        internal bool PlayersWantAnotherGameRound()
        {
            return askUserBooleanQuestion("Do you want to play another game round?");
        }

        // Getting the next move from user, and the next action: either quit, or make a move
        internal Checkers.eAction GetNextMove(PlayerInfo i_Playerinfo, out Move? o_NextMove)
        {
            char playerSymbol = r_PiecesSymbols[(int)i_Playerinfo.PlayerTag + 1]; // could be either O or X
            string userMessage = string.Format("{0}'s turn ({1}) : ", i_Playerinfo.Name, playerSymbol);
            string moveString = GetInputString(userMessage, k_FormatSizeOfMoveStr);
            o_NextMove = null;

            // Until move structure is legal or player wants to quit
            while (o_NextMove == null && !isPlayerPressedQuitButton(moveString))
            {
                o_NextMove = tryParseMoveStr(moveString);
                if (o_NextMove == null)
                {
                    moveString = GetInputString("Move syntax not valid, try again , e.g.: Af>Be ", k_FormatSizeOfMoveStr);
                }
            }

            return o_NextMove == null ? Checkers.eAction.Quit : Checkers.eAction.Continue;
        }

        private bool isPlayerPressedQuitButton(string i_InputString)
        {
            return i_InputString.Length == 1 && i_InputString.ToUpper()[0] == k_QuitChar;
        }

        // Will return null if the input string is not valid
        // Not checking "outOfBounds" scenario - as should only be checked/implemented in the checkers game
        private Move? tryParseMoveStr(string i_MoveString)
        {
            string[] splittedMove = i_MoveString.Split(k_Delimiter);
            Move? returnedPosition = null;

            if (splittedMove.Length == 2 && checkMoveRequest(splittedMove[0], splittedMove[1]))
            {
                returnedPosition = new Move(Position.ParseAlphabetPosition(splittedMove[0]), Position.ParseAlphabetPosition(splittedMove[1]));
            }

            return returnedPosition;
        }

        // Check of the move syntax is valid
        private bool checkMoveRequest(string i_SourcePosition, string i_DestinationPosition)
        {
            return i_DestinationPosition.Length == 2
                && i_SourcePosition.Length == 2
                && char.IsUpper(i_SourcePosition[0])
                && char.IsUpper(i_DestinationPosition[0])
                && char.IsLower(i_SourcePosition[1])
                && char.IsLower(i_DestinationPosition[1]);
        }

        internal void PrintBoard(CheckersGameBoard i_CheckersBoard)
        {
            byte checkersBoardSize = (byte)i_CheckersBoard.BoardSize;

            Ex02.ConsoleUtils.Screen.Clear();

            // printing upper case letteres representing the column index            
            printUpperAlphabetRow(checkersBoardSize);
            printSeperatingLine(checkersBoardSize);
            char currentLowerAlphabet = 'a';
            for (int i = 0; i < checkersBoardSize; i++)
            {
                // printing lower case letteres representing the row index
                Console.Write("{0}|", currentLowerAlphabet);
                currentLowerAlphabet++;

                // printing current cell
                for (int j = 0; j < checkersBoardSize; j++)
                {
                    printCell(i_CheckersBoard[i, j]);
                }

                Console.Write(Environment.NewLine);
                printSeperatingLine(checkersBoardSize);
            }
        }

        private void printCell(CheckersGameBoard.eCellMode i_CellMode)
        {
            // sr_PiecesSymbols is respectively placed to eCellMode!
            // {Empty, Player1Piece, Player2Piece, Player1King, Player2King} -> { ' ', 'O', 'X', 'U', 'K' }
            char cellStrToPrint = r_PiecesSymbols[(int)i_CellMode];
            Console.Write(" {0} |", cellStrToPrint);
        }

        // printing : ========... etc.
        private void printSeperatingLine(byte i_CheckersBoardSize)
        {
            byte lengthOfSeperatingLine = (byte)((i_CheckersBoardSize * 4) + 1);
            Console.Write(" ");
            for (int i = 0; i < lengthOfSeperatingLine; i++)
            {
                Console.Write("{0}", k_SeperatingCharSign);
            }

            Console.Write(Environment.NewLine);
        }

        // printing A  B  C  D ... etc. Columns identification.
        private void printUpperAlphabetRow(byte i_CheckersBoardSize)
        {
            char currentUpperAlphabet = 'A';
            for (int i = 0; i < i_CheckersBoardSize; i++)
            {
                Console.Write("   {0}", currentUpperAlphabet++);
            }

            Console.Write(Environment.NewLine);
        }

        internal string GetPlayerName(string i_PlayerOrder)
        {
            return GetInputString("Please enter a name for the " + i_PlayerOrder + " player and press Enter key.. ", Checkers.MaxStrLength);
        }

        internal void ShowTieState(PlayerInfo i_Player1, PlayerInfo i_Player2)
        {
            string message = string.Format(
@"It's a Tie!
First Player : {0} scored {1} points!
Second Player : {2} scored {3} points!",
                                       i_Player1.Name,
                                       i_Player1.Score,
                                       i_Player2.Name,
                                       i_Player2.Score);

            promptMessage(message);
        }

        internal void ShowWinningState(PlayerInfo i_Winner, PlayerInfo i_Loser)
        {
            string message = string.Format(
@" {0} is the Winner !
{0} scored {1} points!
{2} scored {3} points!",
                       i_Winner.Name,
                       i_Winner.Score,
                       i_Loser.Name,
                       i_Loser.Score);

            promptMessage(message);
        }

        internal int GetBoardSize()
        {
            int inputSize = getPosNumFromUser("Please Enter the board size (6,8,10)");
            int? boardSize = null;

            // repeat until board size is legal
            while (boardSize == null)
            {
                if (CheckersGameBoard.IsBoardSizeValid(inputSize))
                {
                    boardSize = inputSize;
                }
                else
                {
                    inputSize = getPosNumFromUser("Input is not Legal, try again (6,8,10)");
                }
            }

            return boardSize.Value;
        }

        internal void ShowLastMove(PlayerInfo i_LastMovePlayer, Move i_LastMove)
        {
            string moveString = convertMoveToUiString(i_LastMove);
            Console.WriteLine("{0}'s last move was: {1}", i_LastMovePlayer.Name, moveString);
        }

        // Example: (0,1) -> "Ab"
        private string convertMoveToUiString(Move i_MoveToConvert)
        {
            string moveUiString = string.Format(
                "{0}{1}>{2}{3}",
                convertToUpperLetter(i_MoveToConvert.Source.Col),
                char.ToLower(convertToUpperLetter(i_MoveToConvert.Source.Row)),
                convertToUpperLetter(i_MoveToConvert.Destination.Col),
                char.ToLower(convertToUpperLetter(i_MoveToConvert.Destination.Row)));

            return moveUiString;
        }

        private char convertToUpperLetter(int i_NumberToConvert)
        {
            return (char)('A' + i_NumberToConvert);
        }

        private void promptMessage(string i_Message)
        {
            Ex02.ConsoleUtils.Screen.Clear();
            Console.WriteLine(i_Message);
            Console.WriteLine("Press Enter key to continue");
            Console.ReadLine();
        }

        private bool askUserBooleanQuestion(string i_QuestionString)
        {
            bool? resultValue = null;

            Console.WriteLine("{0}, ({1}/{2})?", i_QuestionString, k_YesStr, k_NoStr);

            while (resultValue == null)
            {
                string userInputStr = Console.ReadLine().Trim().ToUpper();
                switch (userInputStr)
                {
                    case k_YesStr:
                        resultValue = true;
                        break;
                    case k_NoStr:
                        resultValue = false;
                        break;
                    default:
                        ShowBadInputMessage();
                        break;
                }
            }

            return resultValue.Value;
        }

        internal string GetInputString(string i_MessageToUser, int i_MaxNumOfAllowedChars)
        {
            Console.WriteLine(i_MessageToUser);
            string strnextAction = Console.ReadLine();
            strnextAction = strnextAction.Trim();

            while (string.IsNullOrEmpty(strnextAction) || strnextAction.Length > i_MaxNumOfAllowedChars)
            {
                ShowBadInputMessage();
                strnextAction = Console.ReadLine();
            }

            return strnextAction;
        }

        private int getPosNumFromUser(string i_MessageToUser)
        {
            int intParseRes;

            string strnextAction = GetInputString(i_MessageToUser, k_MaxNumOfDigitsInInput);
            bool strIsPosNumber = int.TryParse(strnextAction, out intParseRes) && intParseRes > 0;     // Checking if a positive valid number entered
            while (!strIsPosNumber)
            {
                ShowBadInputMessage();
                strnextAction = GetInputString(i_MessageToUser, k_MaxNumOfDigitsInInput);
                strIsPosNumber = int.TryParse(strnextAction, out intParseRes) && intParseRes > 0;     // Checking if a positive valid number entered
            }

            return intParseRes;
        }

        internal void ShowBadInputMessage()
        {
            Console.WriteLine("Input is not Legal, try again");
        }
    }
}
