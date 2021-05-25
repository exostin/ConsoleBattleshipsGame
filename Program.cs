using System;

namespace BattleshipsGame
{
    class Program
    {
        private static void Main(string[] args)
        {
            Player player = new Player();
            Enemy enemy = new Enemy();
            Random rand = new Random();

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
                    enemy.board.GenerateBoard(new int[] { 1, 2, 3, 4, 5 });
                    // Creating a grid for the player according to his chosen configuration scheme
                    player.board.GenerateBoard(player.ChooseShips(configurationIndex));
                    Console.WriteLine("");

                    while (true)
                    {
                        UI.PrintTurnCounter(turnCount);
                        UI.PrintPlayerGrid(player.board.LastHorizontalGridPos, player.board.LastVerticalGridPos, player.board.generatedBoard);
                        UI.PrintEnemyGrid(enemy.board.LastHorizontalGridPos, enemy.board.LastVerticalGridPos, enemy.board.generatedBoard);

                        UI.PrintPlayerAttackPrompt();
                        string playerInput = Console.ReadLine();
                        if (playerInput.Contains(','))
                        {
                            string[] splitPlayerAttackCoords = playerInput.Split(',');
                            var verticalAttackParseSuccess = int.TryParse(splitPlayerAttackCoords[0], out int playerVerticalAttackCoord);
                            var horizontalAttackParseSuccess = int.TryParse(splitPlayerAttackCoords[1], out int playerHorizontalAttackCoord);

                            if ((verticalAttackParseSuccess && horizontalAttackParseSuccess) &&
                                // Checking if player input is in bounds of grid array in both dimensions
                                (playerVerticalAttackCoord >= player.board.firstGridPos) &&
                                (playerVerticalAttackCoord <= player.board.LastVerticalGridPos - 1) &&
                                (playerHorizontalAttackCoord >= player.board.firstGridPos) &&
                                (playerHorizontalAttackCoord <= player.board.LastHorizontalGridPos - 1))
                            {
                                if (enemy.board.generatedBoard[playerVerticalAttackCoord, playerHorizontalAttackCoord] == 2 ||
                                    enemy.board.generatedBoard[playerVerticalAttackCoord, playerHorizontalAttackCoord] == 3)
                                {
                                    Console.WriteLine("You've already fired at that location!");
                                }
                                else
                                {
                                    if (enemy.board.LaunchAttack(playerVerticalAttackCoord, playerHorizontalAttackCoord))
                                    {
                                        Console.WriteLine("Hit scored!");
                                        if (enemy.board.CheckIfDefeated())
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
                                    while (enemy.Move())
                                    {
                                        Console.WriteLine("The enemy has hit your ship!");
                                        if (player.board.CheckIfDefeated())
                                        {
                                            break;
                                        }
                                    }

                                    if (player.board.CheckIfDefeated())
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

            
        }
    }
}