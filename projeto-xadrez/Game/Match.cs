using System;
using Chessboard;

namespace Game
{
    class Match
    {
        public Board Board { get; private set; }
        private int Shift;
        private Color CurrentPlayer;
        public bool Finished { get; private set; }

        public Match()
        {
            Board = new Board(8, 8);
            Shift = 1;
            CurrentPlayer = Color.White;
            Finished = false;
            PlacePieces();
        }

        public void PerformMovement(Position origin, Position destiny)
        {
            Piece p = Board.RemovePiece(origin);
            p.IncreaseNumMovements();
            Piece CapturedPiece = Board.RemovePiece(destiny);
            Board.InsertPiece(p, destiny);
        }

        private void PlacePieces()
        {
            Board.InsertPiece(new Tower(Board, Color.White), new ChessPosition('c', 1).ToPosition());
            Board.InsertPiece(new Tower(Board, Color.White), new ChessPosition('c', 2).ToPosition());
            Board.InsertPiece(new Tower(Board, Color.White), new ChessPosition('d', 2).ToPosition());
            Board.InsertPiece(new Tower(Board, Color.White), new ChessPosition('e', 2).ToPosition());
            Board.InsertPiece(new Tower(Board, Color.White), new ChessPosition('e', 1).ToPosition());
            Board.InsertPiece(new King(Board, Color.White), new ChessPosition('d', 1).ToPosition());

            Board.InsertPiece(new Tower(Board, Color.Black), new ChessPosition('c', 7).ToPosition());
            Board.InsertPiece(new Tower(Board, Color.Black), new ChessPosition('c', 8).ToPosition());
            Board.InsertPiece(new Tower(Board, Color.Black), new ChessPosition('d', 7).ToPosition());
            Board.InsertPiece(new Tower(Board, Color.Black), new ChessPosition('e', 7).ToPosition());
            Board.InsertPiece(new Tower(Board, Color.Black), new ChessPosition('e', 8).ToPosition());
            Board.InsertPiece(new King(Board, Color.Black), new ChessPosition('d', 8).ToPosition());
        }

    }
}
