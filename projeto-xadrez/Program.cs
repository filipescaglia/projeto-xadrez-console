using System;
using Chessboard;
using Game;

namespace projeto_xadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            ChessPosition pos = new ChessPosition('c', 7);


            Console.WriteLine(pos);

            Console.WriteLine(pos.toPosition());
        }
    }
}
