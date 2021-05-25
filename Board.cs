using System;
using System.Linq;

namespace BattleshipsGame
{
    class Board
    {
        private Random rand = new Random();

        public int firstGridPos = 1;

        public int LastVerticalGridPos { get; set; }
        public int LastHorizontalGridPos { get; set; }
        public int[,] generatedBoard= new int[12, 12];

        public Board()
        {
            generatedBoard = new int[12, 12];
        }
        public Board(int[,] board)
        {
            generatedBoard = board;
        }
        /// <summary>
        /// Generate a board with a defined number of ships of specified type
        /// </summary>
        /// <param name="shipsConfiguration"> {5s, 4s, 3s, 2s, 1s} - how many of which ship to place</param>
        public void GenerateBoard(int[] shipsConfiguration)
        {
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
            generatedBoard = new int[12, 12];

            CheckDimensions();

            // Loop generating 15 ships in random locations which cannot overlap or be next to eachother
            for (int i = 0; i < 15;)
            {
                while (true)
                {
                    var randomVerticalStartPoint = rand.Next(firstGridPos, LastVerticalGridPos);
                    var randomHorizontalStartPoint = rand.Next(firstGridPos, LastHorizontalGridPos);
                    if (!CheckIfTheresShipNearby(randomVerticalStartPoint, randomHorizontalStartPoint))
                    {
                        generatedBoard[randomVerticalStartPoint, randomHorizontalStartPoint] = 1;
                        i++;
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }

        /// <summary>
        /// Checks the horizontal and vertical dimensions of the generated board
        /// </summary>
        public void CheckDimensions()
        {
            LastVerticalGridPos = generatedBoard.GetLength(0) - 1;
            LastHorizontalGridPos = generatedBoard.GetLength(1) - 1;
        }

        /// <summary>
        /// Checks all neighbouring spots for existence of any other ship
        /// </summary>
        /// <param name="vertical">Vertical coordinate to check</param>
        /// <param name="horizontal">Vertical coordinate to check</param>
        /// <returns>true if there is a ship nearby, false if there are none</returns>
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

        // 0 - empty space, 1 - a ship, 2 - miss, 3 - hit
        /// <summary>
        /// Launches an attack at the generated board
        /// </summary>
        /// <param name="verticalCoordinate"></param>
        /// <param name="horizontalCoordinate"></param>
        /// <returns>true if something was hit, false if it was a miss</returns>
        public bool LaunchAttack(int verticalCoordinate, int horizontalCoordinate)
        {
            if (generatedBoard[verticalCoordinate, horizontalCoordinate] == 1)
            {
                // Mark that spot as hit
                generatedBoard[verticalCoordinate, horizontalCoordinate] = 3;
                return true;
            }
            else
            {
                // Mark that spot as a miss
                generatedBoard[verticalCoordinate, horizontalCoordinate] = 2;
                return false;
            }
        }

        /// <summary>
        /// Checks if the generated board contains any ship (a spot with a value of 1)
        /// </summary>
        public bool CheckIfDefeated()
        {
            if (!generatedBoard.Cast<int>().Contains(1))
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