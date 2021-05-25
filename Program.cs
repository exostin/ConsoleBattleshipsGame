using System;

namespace StatkiRewrite
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Player player = new Player();
            Enemy enemy = new Enemy();
            Random rand = new Random();

            Board playerBoard = new Board();
            Board enemyBoard = new Board();

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
                    enemyBoard.GenerateBoard(new int[] { 1, 2, 3, 4, 5 });
                    // Creating a grid for the player according to his chosen configuration scheme
                    playerBoard.GenerateBoard(player.ChooseShips(configurationIndex));
                    Console.WriteLine("");

                    while (true)
                    {
                        UI.PrintTurnCounter(turnCount);
                        UI.PrintPlayerGrid(playerBoard.LastHorizontalGridPos, playerBoard.LastVerticalGridPos, playerBoard.GeneratedBoard);
                        UI.PrintEnemyGrid(enemyBoard.LastHorizontalGridPos, enemyBoard.LastVerticalGridPos, enemyBoard.GeneratedBoard);

                        UI.PrintPlayerAttackPrompt();
                        string playerInput = Console.ReadLine();
                        if (playerInput.Contains(','))
                        {
                            string[] splitPlayerAttackCoords = playerInput.Split(',');
                            var verticalAttackParseSuccess = int.TryParse(splitPlayerAttackCoords[0], out int playerVerticalAttackCoord);
                            var horizontalAttackParseSuccess = int.TryParse(splitPlayerAttackCoords[1], out int playerHorizontalAttackCoord);

                            if ((verticalAttackParseSuccess && horizontalAttackParseSuccess) &&
                                // Checking if player input is in bounds of grid array in both dimensions
                                (playerVerticalAttackCoord >= playerBoard.firstGridPos) &&
                                (playerVerticalAttackCoord <= playerBoard.LastVerticalGridPos - 1) &&
                                (playerHorizontalAttackCoord >= playerBoard.firstGridPos) &&
                                (playerHorizontalAttackCoord <= playerBoard.LastHorizontalGridPos - 1))
                            {
                                if (enemyBoard.GeneratedBoard[playerVerticalAttackCoord, playerHorizontalAttackCoord] == 2 ||
                                    enemyBoard.GeneratedBoard[playerVerticalAttackCoord, playerHorizontalAttackCoord] == 3)
                                {
                                    Console.WriteLine("You've already fired at that location!");
                                }
                                else
                                {
                                    if (enemyBoard.LaunchAttack(playerVerticalAttackCoord, playerHorizontalAttackCoord))
                                    {
                                        Console.WriteLine("Hit scored!");
                                        if (enemyBoard.CheckIfDefeated())
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

                                    // Executing enemy move and checking if he hit any ship
                                    while (EnemyMove())
                                    {
                                        Console.WriteLine("The enemy has hit your ship!");
                                        if (playerBoard.CheckIfDefeated())
                                        {
                                            break;
                                        }
                                    }

                                    if (playerBoard.CheckIfDefeated())
                                    {
                                        Console.WriteLine("You lost!");
                                        break;
                                    }

                                    turnCount++;
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
                    }
                }
                else
                {
                    Console.WriteLine("Wrong input! Try again.");
                }
            }

            bool EnemyMove()
            {
                int enemyAttackRandomVerticalCoord = rand.Next(playerBoard.firstGridPos, playerBoard.LastVerticalGridPos + 1);
                int enemyAttackRandomHorizontalCoord = rand.Next(playerBoard.firstGridPos, playerBoard.LastHorizontalGridPos + 1);

                if (playerBoard.LaunchAttack(enemyAttackRandomVerticalCoord, enemyAttackRandomHorizontalCoord))
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
}