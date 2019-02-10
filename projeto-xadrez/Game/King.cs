﻿using Chessboard;

namespace Game
{
    class King : Piece
    {
        public King(Board board, Color color) : base(board, color) { }

        public override string ToString()
        {
            return "K";
        }
    }
}