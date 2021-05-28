using System;

namespace BattleshipsGame
{
    class Enemy : Player
    {
        private Random _rand = new Random();

        public int[,] PlayerBoardGrid { get; set; }
        public int CurrentDifficulty { get; set; } = 0;
        int VerticalCoord { get; set; }
        int HorizontalCoord { get; set; }
        int[] AttackCoords { get; set; }

        public void GetPlayerBoard(ref Board playerBoard)
        {
            PlayerBoardGrid = playerBoard.GeneratedBoard;
        }

        public void GenerateCoordinates()
        {
            VerticalCoord = _rand.Next(board.FirstGridPos, board.LastVerticalGridPos);
            HorizontalCoord = _rand.Next(board.FirstGridPos, board.LastHorizontalGridPos);
        }

        public int[] Attack()
        {
            //while (true)
            //{
            //    // Easy
            //    if (CurrentDifficulty >= 1)
            //    {
            //        GenerateCoordinates();
            //        // Normal
            //        if (CurrentDifficulty >= 2)
            //        {
            //            if (PlayerBoardGrid[VerticalCoord, HorizontalCoord] == 2 ||
            //                PlayerBoardGrid[VerticalCoord, HorizontalCoord] == 3)
            //            {
            //                GenerateCoordinates();
            //            }
            //        }
            //    }
            //    break;
            //}



            switch (CurrentDifficulty)
            {
                case 0:
                    // Easy: This AI intelligence level will: attack random positions and has a 33% chance to purosefully miss if it hit something
                    GenerateCoordinates();
                    break;

                case 1:
                    // Normal: This AI intelligence level will: attack positions that haven't been hit yet
                    GenerateCoordsWhichHaventBeenUsed();
                    break;

                case 2:
                    // Hard: This AI intelligence level will: attack positions that aren't near to other already hit positions
                    GenerateStrategicCoordsWithUnpopulatedNeighbours();
                    break;

                case 3:
                    // Extreme: This AI intelligence level will: kill a ship every turn and then fire a random shot
                    GeneratePopulatedCoords();
                    break;

                default:
                    // Easy
                    break;
            }

            AttackCoords = new int[] { VerticalCoord, HorizontalCoord };
            return AttackCoords;
        }

        public void GenerateCoordsWhichHaventBeenUsed()
        {
            GenerateCoordinates();
            while (true)
            {
                if (PlayerBoardGrid[VerticalCoord, HorizontalCoord] == 2 || PlayerBoardGrid[VerticalCoord, HorizontalCoord] == 3)
                {
                    GenerateCoordinates();
                }
                else
                {
                    break;
                }
            }
            
        }

        public void GenerateStrategicCoordsWithUnpopulatedNeighbours()
        {
            GenerateCoordsWhichHaventBeenUsed();
            while (CheckIfThereAreNeighbours())
            {
                GenerateCoordsWhichHaventBeenUsed();
            }
        }

        // Hit one and then purposefully miss
        public void GeneratePopulatedCoords()
        {
            while(PlayerBoardGrid[VerticalCoord, HorizontalCoord] != 1)
            {
                GenerateStrategicCoordsWithUnpopulatedNeighbours();
            }
        }

        public bool CheckIfThereAreNeighbours()
        {
            if ((PlayerBoardGrid[VerticalCoord + 1, HorizontalCoord] != 3) &&
                (PlayerBoardGrid[VerticalCoord - 1, HorizontalCoord] != 3) &&
                (PlayerBoardGrid[VerticalCoord, HorizontalCoord + 1] != 3) &&
                (PlayerBoardGrid[VerticalCoord, HorizontalCoord - 1] != 3) &&
                (PlayerBoardGrid[VerticalCoord - 1, HorizontalCoord + 1] != 3) &&
                (PlayerBoardGrid[VerticalCoord - 1, HorizontalCoord - 1] != 3) &&
                (PlayerBoardGrid[VerticalCoord + 1, HorizontalCoord + 1] != 3) &&
                (PlayerBoardGrid[VerticalCoord + 1, HorizontalCoord - 1] != 3))
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