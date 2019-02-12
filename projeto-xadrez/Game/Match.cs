using System;
using Chessboard;

namespace Game
{
    class Match
    {
        public Board Board { get; private set; }
        public int Shift { get; private set; }
        public Color CurrentPlayer { get; private set; }
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

        public void PerformPlay(Position origin, Position destiny)
        {
            PerformMovement(origin, destiny);
            Shift++;
            SwitchPlayer();
        }

        public void ValidateOriginPosition(Position pos)
        {
            if (Board.Piece(pos) == null)
            {
                throw new BoardException("The chosen position doesn't contain a piece!");
            }
            if (CurrentPlayer != Board.Piece(pos).Color)
            {
                throw new BoardException("The chosen piece isn't yours!");
            }
            if (!Board.Piece(pos).ExistPossibleMovements())
            {
                throw new BoardException("There is no possible movements for the chosen piece!");
            }
        }

        public void ValidateDestinyPosition(Position origin, Position destiny)
        {
            if (!Board.Piece(origin).CanMoveTo(destiny))
            {
                throw new BoardException("Invalid destiny position!");
            }
        }

        private void SwitchPlayer()
        {
            if (CurrentPlayer == Color.White)
            {
                CurrentPlayer = Color.Black;
            }
            else
            {
                CurrentPlayer = Color.White;
            }
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
