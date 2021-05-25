using System;
using System.Linq;

namespace StatkiRewrite
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Board board = new Board();
            Player player = new Player();
            Enemy enemy = new Enemy();
            Random rand = new Random();

            int[,] enemyGrid;
            int[,] playerGrid;

            int turnCount = 1;

            string configurationOptions =
                "1) 5, 44, 333, 2222, 11111 - original\r\n" +
                "2) 55, 44, 333, 2222 - no 1s\r\n" +
                "3) 555, 44, 3333 - no 1s and 2s\r\n" +
                "4) 7x 5\r\n" +
                "5) 35x 1";

            while (true)
            {
                // Ships configuration
                Console.WriteLine(configurationOptions);
                Console.Write("Choose your prefered configuration: ");
                var configurationParseSuccess = int.TryParse(Console.ReadLine(), out var configurationIndex);

                if (configurationParseSuccess && (configurationIndex >= 1 && configurationIndex <= 5))
                {
                    // Generating enemy grid with the original configuration
                    enemyGrid = board.GenerateBoard(new int[] { 1, 2, 3, 4, 5 });
                    // Creating a grid for the player according to his chosen configuration scheme
                    playerGrid = board.GenerateBoard(player.ChooseShips(configurationIndex));
                    Console.WriteLine("");

                    while (true)
                    {
                        UIPrintTurn();
                        UIPrintPlayerGrid();
                        UIPrintEnemyGrid();
                        // Player attack prompt
                        Console.Write("Enter the attack coordinates (vert/hor 1-10 ex. 9,1): ");
                        string playerInput = Console.ReadLine();
                        if (playerInput.Contains(','))
                        {
                            string[] splitPlayerAttackCoords = playerInput.Split(',');
                            var verticalAttackParseSuccess = int.TryParse(splitPlayerAttackCoords[0], out int playerVerticalAttackCoord);
                            var horizontalAttackParseSuccess = int.TryParse(splitPlayerAttackCoords[1], out int playerHorizontalAttackCoord);

                            if ((verticalAttackParseSuccess && horizontalAttackParseSuccess) &&
                                // Checking if player input is in bounds of grid array in both dimensions
                                (playerVerticalAttackCoord >= board.firstGridPos) &&
                                (playerVerticalAttackCoord <= board.lastVerticalGridPos - 1) &&
                                (playerHorizontalAttackCoord >= board.firstGridPos) &&
                                (playerHorizontalAttackCoord <= board.lastHorizontalGridPos - 1))
                            {
                                if (enemyGrid[playerVerticalAttackCoord, playerHorizontalAttackCoord] == 2 ||
                                    enemyGrid[playerVerticalAttackCoord, playerHorizontalAttackCoord] == 3)
                                {
                                    Console.WriteLine("You've already fired at that location!");
                                }
                                else
                                {
                                    if (Fire(playerVerticalAttackCoord, playerHorizontalAttackCoord, enemyGrid))
                                    {
                                        Console.WriteLine("Hit scored!");
                                        if (CheckVictory(enemyGrid))
                                        {
                                            Console.WriteLine("Victory!!!");
                                            break;
                                        }
                                        else
                                        {
                                            continue;
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Miss!");
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("Coordinates out of bounds! Try again.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Wrong input! Try again.");
                        }

                        // Executing enemy turn and checking if he hit any ship
                        while (EnemyTurn())
                        {
                            Console.WriteLine("The enemy has hit your ship!");
                            if (CheckVictory(playerGrid))
                            {
                                break;
                            }
                        }

                        if (CheckVictory(playerGrid))
                        {
                            Console.WriteLine("You lost!");
                            break;
                        }

                        turnCount++;
                    }
                }
                else
                {
                    Console.WriteLine("Wrong input! Try again.");
                }
            }

            bool CheckVictory(int[,] gridToCheck)
            {
                if (!gridToCheck.Cast<int>().Contains(1))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            bool Fire(int verticalCoordinate, int horizontalCoordinate, int[,] gridToFireAt)
            {
                if (gridToFireAt[verticalCoordinate, horizontalCoordinate] == 1)
                {
                    // Mark that spot as hit
                    gridToFireAt[verticalCoordinate, horizontalCoordinate] = 3;
                    return true;
                }
                else
                {
                    // Mark that spot as a miss
                    gridToFireAt[verticalCoordinate, horizontalCoordinate] = 2;
                    return false;
                }
            }

            bool EnemyTurn()
            {
                int enemyAttackRandomVerticalCoord = rand.Next(board.firstGridPos, board.lastVerticalGridPos + 1);
                int enemyAttackRandomHorizontalCoord = rand.Next(board.firstGridPos, board.lastHorizontalGridPos + 1);

                if (Fire(enemyAttackRandomVerticalCoord, enemyAttackRandomHorizontalCoord, playerGrid))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            void UIPrintEnemyGrid()
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("|   ENEMY GRID   |");
                Console.ResetColor();
                for (int i = 1; i < board.lastHorizontalGridPos; i++)
                {
                    for (int j = 1; j < board.lastVerticalGridPos; j++)
                    {
                        if (enemyGrid[i, j] != 1)
                        {
                            Console.Write(string.Format("{0} ", enemyGrid[i, j]));
                        }
                        else
                        {
                            Console.Write("? ");
                        }
                    }
                    Console.Write(Environment.NewLine);
                }
            }
            void UIPrintPlayerGrid()
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("|   YOUR GRID   |");
                Console.ResetColor();
                for (int i = 1; i < board.lastHorizontalGridPos; i++)
                {
                    for (int j = 1; j < board.lastVerticalGridPos; j++)
                    {
                        Console.Write(string.Format("{0} ", playerGrid[i, j]));
                    }
                    Console.Write(Environment.NewLine);
                }
            }
            void UIPrintTurn()
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"#  Turn no. {turnCount}   #");
                Console.ResetColor();
            }

            // 0 - empty space, 1 - a ship, 2 - miss, 3 - hit | and in future 4 as a destroyed ship

            // TODO Metody do odpowiednich klas
        }
    }
}