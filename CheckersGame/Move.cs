namespace CheckersGame
{
    public struct Move
    {
        private Position m_Source; //Coordinates
        private Position m_Destination; //Coordinates

        public Position Source
        {
            get { return m_Source; }
            set { m_Source = value; }
        }

        public Position Destination
        {
            get { return m_Destination; }
            set { m_Destination = value; }
        }

        public Move(Position i_Source, Position i_Destination)
        {
            m_Source = i_Source;
            m_Destination = i_Destination;
        }

        public static Move ParseAlphabetMove(string i_SrcPosition, string i_DstPosition)
        {
            return new Move(Position.ParseAlphabetPosition(i_SrcPosition),
                Position.ParseAlphabetPosition(i_DstPosition));
        }
    }
}
