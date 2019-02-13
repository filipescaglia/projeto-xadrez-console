namespace Chessboard
{
    abstract class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int QtMovements { get; protected set; }
        public Board Board { get; protected set; }

        public Piece(Board board, Color color)
        {
            Position = null;
            Board = board;
            Color = color;
            QtMovements = 0;
        }

        public void IncreaseNumMovements()
        {
            QtMovements++;
        }

        public void DecreaseNumMovements()
        {
            QtMovements--;
        }

        public bool ExistPossibleMovements()
        {
            bool[,] mat = PossibleMovements();
            for (int i = 0; i < Board.Lines; i++)
            {
                for (int j = 0; j < Board.Columns; j++)
                {
                    if (mat[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool PossibleMovement(Position pos)
        {
            return PossibleMovements()[pos.Line, pos.Column];
        }
         
        public abstract bool[,] PossibleMovements();
    }
}
