using System;

namespace CheckersGame
{
    public class GameOverEventArgs : EventArgs
    {
        public PlayerInfo StrongerPlayer { get; set; }

        public PlayerInfo WeakerPlayer { get; set; }

        public bool Tie { get; set; }

        public GameOverEventArgs(PlayerInfo i_PlayerInfo1, PlayerInfo i_PlayerInfo2, bool i_Tie)
        {
            StrongerPlayer = i_PlayerInfo1;
            WeakerPlayer = i_PlayerInfo2;
            Tie = i_Tie;
        }
    }
}
