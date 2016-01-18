namespace CheckersGame
{
    public struct Position
    {
        private int m_Row;  // Coordinate
        private int m_Col;  // Coordinate

        public Position(int i_Row, int i_Col)
        {
            m_Row = i_Row;
            m_Col = i_Col;
        }

        public int Row
        {
            get { return m_Row; }
            set { m_Row = value; }
        }

        public int Col
        {
            get { return m_Col; }
            set { m_Col = value; }
        }

        // Example: "Af" -> (0,5)
        public static Position ParseAlphabetPosition(string i_CheckersBoardPosition)
        {
            int x = char.ToUpper(i_CheckersBoardPosition[0]) - 'A';
            int y = char.ToUpper(i_CheckersBoardPosition[1]) - 'A';

            return new Position(x, y);
        }

        // Adds a position to this position
        public Position AddPosition(Position i_PositionToAdd)
        {
            Position sumPosition = new Position(m_Row, m_Col);
            sumPosition.Row += i_PositionToAdd.Row;
            sumPosition.Col += i_PositionToAdd.Col;

            return sumPosition;
        }

        public string ToAlphabetString()
        {
            char rowChar = (char)('A' + m_Row);
            char colChar = (char)('a' + m_Col);
            return rowChar.ToString() + colChar.ToString();
        }
    }
}
