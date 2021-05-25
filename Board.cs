using System;

namespace StatkiRewrite
{
    internal class Board
    {
        private Random rnd = new Random();

        public int firstGridPos = 1;
        public int lastVerticalGridPos;
        public int lastHorizontalGridPos;

        public int[,] generatedBoard;

        // shipsConfiguration = {5s, 4s, 3s, 2s, 1s} - how many of which ship to place
        public int[,] GenerateBoard(int[] shipsConfiguration)
        {
            generatedBoard = new int[12, 12];
            //generatedBoard = new int[12, 12] { { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 0},
            //                                    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            //                                    { 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            //                                    { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            //                                    { 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            //                                    { 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            //                                    { 6, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            //                                    { 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            //                                    { 8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            //                                    { 9, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            //                                    { 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            //                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}};

            CheckDimensions();

            // Choose a random place on the board
            // and if the ship takes more than 1 place
            // Randomly choose the direction it will face
            // check if there's place for it (can't be near other ships)
            // start placing it (for loop, long as the ship)
            // continue placing until all ships are on their place

            // For: length of ShipsToBePlaced array? or something similar
            for (int i = 0; i < 15;)
            {
                while (true)
                {
                    var randomVerticalStartPoint = rnd.Next(firstGridPos, lastVerticalGridPos);
                    var randomHorizontalStartPoint = rnd.Next(firstGridPos, lastHorizontalGridPos);
                    if (!CheckIfTheresShipNearby(randomVerticalStartPoint, randomHorizontalStartPoint))
                    {
                        generatedBoard[randomVerticalStartPoint, randomHorizontalStartPoint] = 1;
                        i++;
                        break;
                        // Check the ship length
                        // If it is 1 block long then break;
                        // else:
                        // Determine direction the ship will face
                        // for loop:
                        // CheckIfTheresShipNearby
                        // place
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            return generatedBoard;
        }

        public void CheckDimensions()
        {
            lastVerticalGridPos = generatedBoard.GetLength(0) - 1;
            lastHorizontalGridPos = generatedBoard.GetLength(1) - 1;
        }

        public bool CheckIfTheresShipNearby(int vertical, int horizontal)
        {
            if (generatedBoard[vertical, horizontal] == 0 &&
                (generatedBoard[vertical + 1, horizontal] != 1) &&
                (generatedBoard[vertical - 1, horizontal] != 1) &&
                (generatedBoard[vertical, horizontal + 1] != 1) &&
                (generatedBoard[vertical, horizontal - 1] != 1) &&
                (generatedBoard[vertical - 1, horizontal + 1] != 1) &&
                (generatedBoard[vertical - 1, horizontal - 1] != 1) &&
                (generatedBoard[vertical + 1, horizontal + 1] != 1) &&
                (generatedBoard[vertical + 1, horizontal - 1] != 1))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}