namespace CheckersGame
{
    public class PlayerInfo
    {
        private const string k_ComputerName = "Computer";
        private string m_PlayerName;
        private uint m_Score;
        private bool m_Human;
        private Checkers.ePlayerTag m_PlayerTag;

        public Checkers.ePlayerTag PlayerTag
        {
            get { return m_PlayerTag; }
            set { m_PlayerTag = value; }
        }
        
        public string Name
        {
            get { return m_PlayerName; }
            set { m_PlayerName = value; }
        }

        public bool Human
        {
            get { return m_Human; }
            set { m_Human = value; }
        }

        public uint Score
        {
            get { return m_Score; }
            set { m_Score = value; }
        }

        // Creates a human playerInfo
        public PlayerInfo(string i_PlayerName, bool i_Human, Checkers.ePlayerTag i_PlayerTag)
        {
            Name = i_PlayerName;
            Human = i_Human;
            Score = 0;
            PlayerTag = i_PlayerTag;
        }

        // Creates a machine playerInfo
        public PlayerInfo(bool i_Human, Checkers.ePlayerTag i_PlayerTag)
        {
            Name = k_ComputerName;
            Human = i_Human;
            Score = 0;
            PlayerTag = i_PlayerTag;
        }
    }
}
