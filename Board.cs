using System;
using System.Linq;

namespace StatkiRewrite
{
    internal class Board
    {
        private Random rand = new Random();

        public int firstGridPos = 1;

        public int LastVerticalGridPos { get; set; }
        public int LastHorizontalGridPos { get; set; }
        public int[,] GeneratedBoard { get; set; }

        /// <summary>
        /// Generate a board with a defined number of ships of specified type
        /// </summary>
        /// <param name="shipsConfiguration"> {5s, 4s, 3s, 2s, 1s} - how many of which ship to place</param>
        public int[,] GenerateBoard(int[] shipsConfiguration)
        {
            GeneratedBoard = new int[12, 12];
            // Visual representation of this array
            // generatedBoard = new int[12, 12] { { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 0},
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
            for (int i = 0; i < 15;)
            {
                while (true)
                {
                    var randomVerticalStartPoint = rand.Next(firstGridPos, LastVerticalGridPos);
                    var randomHorizontalStartPoint = rand.Next(firstGridPos, LastHorizontalGridPos);
                    if (!CheckIfTheresShipNearby(randomVerticalStartPoint, randomHorizontalStartPoint))
                    {
                        GeneratedBoard[randomVerticalStartPoint, randomHorizontalStartPoint] = 1;
                        i++;
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            return GeneratedBoard;
        }

        /// <summary>
        /// Checks the horizontal and vertical dimensions of the generated board
        /// </summary>
        public void CheckDimensions()
        {
            LastVerticalGridPos = GeneratedBoard.GetLength(0) - 1;
            LastHorizontalGridPos = GeneratedBoard.GetLength(1) - 1;
        }

        /// <summary>
        /// Checks all neighbouring spots for existence of any other ship
        /// </summary>
        /// <param name="vertical">Vertical coordinate to check</param>
        /// <param name="horizontal">Vertical coordinate to check</param>
        /// <returns>true if there is a ship nearby, false if there are none</returns>
        public bool CheckIfTheresShipNearby(int vertical, int horizontal)
        {
            if (GeneratedBoard[vertical, horizontal] == 0 &&
                (GeneratedBoard[vertical + 1, horizontal] != 1) &&
                (GeneratedBoard[vertical - 1, horizontal] != 1) &&
                (GeneratedBoard[vertical, horizontal + 1] != 1) &&
                (GeneratedBoard[vertical, horizontal - 1] != 1) &&
                (GeneratedBoard[vertical - 1, horizontal + 1] != 1) &&
                (GeneratedBoard[vertical - 1, horizontal - 1] != 1) &&
                (GeneratedBoard[vertical + 1, horizontal + 1] != 1) &&
                (GeneratedBoard[vertical + 1, horizontal - 1] != 1))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        // 0 - empty space, 1 - a ship, 2 - miss, 3 - hit
        /// <summary>
        /// Launches an attack at the generated board
        /// </summary>
        /// <param name="verticalCoordinate"></param>
        /// <param name="horizontalCoordinate"></param>
        /// <returns>true if something was hit, false if it was a miss</returns>
        public bool LaunchAttack(int verticalCoordinate, int horizontalCoordinate)
        {
            if (GeneratedBoard[verticalCoordinate, horizontalCoordinate] == 1)
            {
                // Mark that spot as hit
                GeneratedBoard[verticalCoordinate, horizontalCoordinate] = 3;
                return true;
            }
            else
            {
                // Mark that spot as a miss
                GeneratedBoard[verticalCoordinate, horizontalCoordinate] = 2;
                return false;
            }
        }

        /// <summary>
        /// Checks if the generated board contains any ship (a spot with a value of 1)
        /// </summary>
        public bool CheckIfDefeated()
        {
            if (!GeneratedBoard.Cast<int>().Contains(1))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}