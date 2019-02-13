using System;
using System.Collections.Generic;
using Chessboard;

namespace Game
{
    class Match
    {
        public Board Board { get; private set; }
        public int Shift { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool Finished { get; private set; }
        private HashSet<Piece> Pieces;
        private HashSet<Piece> Captured;
        public bool Check { get; private set; }

        public Match()
        {
            Board = new Board(8, 8);
            Shift = 1;
            CurrentPlayer = Color.White;
            Finished = false;
            Check = false;
            Pieces = new HashSet<Piece>();
            Captured = new HashSet<Piece>();
            PlacePieces();
        }

        public Piece PerformMovement(Position origin, Position destiny)
        {
            Piece p = Board.RemovePiece(origin);
            p.IncreaseNumMovements();
            Piece capturedPiece = Board.RemovePiece(destiny);
            Board.InsertPiece(p, destiny);
            if (capturedPiece != null)
            {
                Captured.Add(capturedPiece);
            }
            return capturedPiece;
        }

        public void UndoMovement(Position origin, Position destiny, Piece capturedPiece)
        {
            Piece p = Board.RemovePiece(destiny);
            p.DecreaseNumMovements();
            if (capturedPiece != null)
            {
                Board.InsertPiece(capturedPiece, destiny);
                Captured.Remove(capturedPiece);
            }
            Board.InsertPiece(p, origin);
        }

        public void PerformPlay(Position origin, Position destiny)
        {
            Piece capturedPiece = PerformMovement(origin, destiny);

            if (IsInCheck(CurrentPlayer))
            {
                UndoMovement(origin, destiny, capturedPiece);
                throw new BoardException("You can't put yourself in check!");
            }
            if (IsInCheck(Opponent(CurrentPlayer)))
            {
                Check = true;
            }
            else
            {
                Check = false;
            }

            if (TestCheckmate(Opponent(CurrentPlayer)))
            {
                Finished = true;
            }
            else
            {
                Shift++;
                SwitchPlayer();
            }            
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
            if (!Board.Piece(origin).PossibleMovement(destiny))
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

        public HashSet<Piece> CapturedPieces(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in Captured)
            {
                if (x.Color == color) 
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Piece> InGamePieces(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in Pieces)
            {
                if (x.Color == color)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(CapturedPieces(color));
            return aux;
        }

        private Color Opponent(Color color)
        {
            if (color == Color.White)
            {
                return Color.Black;
            }
            else
            {
                return Color.White;
            }
        }

        private Piece King(Color color)
        {
            foreach (Piece x in InGamePieces(color))
            {
                if (x is King)
                {
                    return x;
                }
            }
            return null;
        }

        public bool IsInCheck(Color color)
        {
            Piece k = King(color);
            if (k == null)
            {
                throw new BoardException("There is no " + color + " king in the game!");
            }

            foreach (Piece x in InGamePieces(Opponent(color)))
            {
                bool[,] mat = x.PossibleMovements();
                if (mat[k.Position.Line, k.Position.Column])
                {
                    return true;
                }
            }
            return false;
        }

        public bool TestCheckmate(Color color)
        {
            if (!IsInCheck(color))
            {
                return false;
            }
            foreach (Piece x in InGamePieces(color))
            {
                bool[,] mat = x.PossibleMovements();
                for (int i = 0; i < Board.Lines; i++)
                {
                    for (int j = 0; j < Board.Columns; j++)
                    {
                        if (mat[i, j])
                        {
                            Position origin = x.Position;
                            Position destiny = new Position(i, j);
                            Piece CapturedPiece = PerformMovement(origin, destiny);
                            bool testCheck = IsInCheck(color);
                            UndoMovement(origin, destiny, CapturedPiece);
                            if (!testCheck)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void PlaceNewPiece(char column, int line, Piece piece)
        {
            Board.InsertPiece(piece, new ChessPosition(column, line).ToPosition());
            Pieces.Add(piece);
        }

        private void PlacePieces()
        {
            /*PlaceNewPiece('c', 1, new Tower(Board, Color.White));
            PlaceNewPiece('c', 2, new Tower(Board, Color.White));
            PlaceNewPiece('d', 2, new Tower(Board, Color.White));
            PlaceNewPiece('e', 2, new Tower(Board, Color.White));
            PlaceNewPiece('e', 1, new Tower(Board, Color.White));
            PlaceNewPiece('d', 1, new King(Board, Color.White));

            PlaceNewPiece('c', 7, new Tower(Board, Color.Black));
            PlaceNewPiece('c', 8, new Tower(Board, Color.Black));
            PlaceNewPiece('d', 7, new Tower(Board, Color.Black));
            PlaceNewPiece('e', 7, new Tower(Board, Color.Black));
            PlaceNewPiece('e', 8, new Tower(Board, Color.Black));
            PlaceNewPiece('d', 8, new King(Board, Color.Black));*/

            PlaceNewPiece('c', 1, new Tower(Board, Color.White));
            PlaceNewPiece('d', 1, new King(Board, Color.White));
            PlaceNewPiece('h', 7, new Tower(Board, Color.White));

            PlaceNewPiece('a', 8, new King(Board, Color.Black));
            PlaceNewPiece('b', 8, new Tower(Board, Color.Black));
        }

    }
}
