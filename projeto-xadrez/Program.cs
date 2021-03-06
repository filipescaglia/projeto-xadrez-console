﻿using System;
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
                    try
                    {
                        Console.Clear();
                        View.PrintMatch(match);
                        Console.WriteLine();

                        Console.Write("Origin: ");
                        Position origin = View.ReadChessPosition().ToPosition();
                        match.ValidateOriginPosition(origin);

                        bool[,] PossiblePositions = match.Board.Piece(origin).PossibleMovements();

                        Console.Clear();
                        View.PrintChess(match.Board, PossiblePositions);

                        Console.WriteLine();
                        Console.Write("Destiny: ");
                        Position destiny = View.ReadChessPosition().ToPosition();
                        match.ValidateDestinyPosition(origin, destiny);

                        match.PerformPlay(origin, destiny);
                    }
                    catch (BoardException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }
                Console.Clear();
                View.PrintMatch(match);
            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
