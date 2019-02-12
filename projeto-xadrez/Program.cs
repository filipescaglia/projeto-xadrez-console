using System;
using Chessboard;
using Game;

namespace projeto_xadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Match match = new Match();

                while (!match.Finished)
                {
                    Console.Clear();
                    View.PrintChess(match.Board);
                    Console.WriteLine();

                    Console.Write("Origin: ");
                    Position origin = View.ReadChessPosition().ToPosition();

                    bool[,] PossiblePositions = match.Board.Piece(origin).PossibleMovements();

                    Console.Clear();
                    View.PrintChess(match.Board, PossiblePositions);

                    Console.WriteLine();
                    Console.Write("Destiny: ");
                    Position destiny = View.ReadChessPosition().ToPosition();

                    match.PerformMovement(origin, destiny);
                }                
                
            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
