using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckersGame
{
    public class CheckersGameGui
    {
        private readonly FormGameSettings r_FormGameSettings = new FormGameSettings();
        private FormGameBoard m_FormGameBoard;
        private Checkers m_CheckersLogic = new Checkers();

        public void StartGame()
        {
            r_FormGameSettings.ShowDialog();
            m_FormGameBoard = new FormGameBoard(r_FormGameSettings.BoardSize);
            m_CheckersLogic.InitializeGameLogic(r_FormGameSettings.BoardSize,
                r_FormGameSettings.Player1Name,
                r_FormGameSettings.Player2Name,
                r_FormGameSettings.IsPlayer2Human);

            m_FormGameBoard.HandleInputMove += BoardForm_InputReceived;
            m_FormGameBoard.ShowDialog();
        }

        private FormGameBoard.MoveInfo BoardForm_InputReceived(Button i_SrcButton, Button i_DstButton)
        {
            FormGameBoard.MoveInfo moveInfo = new FormGameBoard.MoveInfo();
            Position srcPosition = Position.ParseAlphabetPosition(i_SrcButton.Tag.ToString());
            Position dstPosition = Position.ParseAlphabetPosition(i_DstButton.Tag.ToString());
            Position? capturedPosition = null;
            moveInfo.MoveValid = Checkers.CheckIfMoveValid(srcPosition, dstPosition, out capturedPosition);
            moveInfo.CapturedCell = capturedPosition.Value.ToAlphabetString();

            return moveInfo;
        }
    }
}
