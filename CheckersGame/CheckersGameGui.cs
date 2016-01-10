using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            m_FormGameBoard.ShowDialog();

            m_CheckersLogic.InitializeGameLogic(r_FormGameSettings.BoardSize,
                r_FormGameSettings.Player1Name,
                r_FormGameSettings.Player2Name,
                r_FormGameSettings.IsPlayer2Human);
        }


    }
}
