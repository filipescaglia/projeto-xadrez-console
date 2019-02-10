using System;
using Chessboard;

namespace projeto_xadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(8, 8);

            View.PrintChess(board);
        }
    }
}
