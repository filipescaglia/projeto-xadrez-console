﻿namespace Chessboard
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
         
        public abstract bool[,] PossibleMovements();
    }
}
