using System;

namespace BattleshipsGame
{
    class Program
    {
        private static void Main(string[] args)
        {
            Player player = new Player();
            Enemy enemy = new Enemy();

            int turnCount = 1;

            while (true)
            {
                UI.PrintConfigurationPrompt();
                var configurationParseSuccess = int.TryParse(Console.ReadLine(), out var configurationIndex);

                if (configurationParseSuccess && (configurationIndex >= 1 && configurationIndex <= 5))
                {
                    enemy.board.PopulateBoard(player.ChooseShips(configurationIndex));
                    player.board.PopulateBoard(player.ChooseShips(configurationIndex));

                    while (true)
                    {
                        UI.PrintTurnCounter(turnCount);
                        UI.PrintPlayerGrid(player.board.LastHorizontalGridPos, player.board.LastVerticalGridPos, player.board.GeneratedBoard);
                        UI.PrintEnemyGrid(enemy.board.LastHorizontalGridPos, enemy.board.LastVerticalGridPos, enemy.board.GeneratedBoard);

                        UI.PrintPlayerAttackPrompt();
                        string playerInput = Console.ReadLine();
                        if (playerInput.Contains(','))
                        {
                            string[] splitPlayerAttackCoords = playerInput.Split(',');
                            var verticalAttackParseSuccess = int.TryParse(splitPlayerAttackCoords[0], out int playerVerticalAttackCoord);
                            var horizontalAttackParseSuccess = int.TryParse(splitPlayerAttackCoords[1], out int playerHorizontalAttackCoord);

                            if ((verticalAttackParseSuccess && horizontalAttackParseSuccess) &&
                                // Checking if player input is in bounds of grid array in both dimensions
                                (playerVerticalAttackCoord >= player.board.FirstGridPos) &&
                                (playerVerticalAttackCoord <= player.board.LastVerticalGridPos - 1) &&
                                (playerHorizontalAttackCoord >= player.board.FirstGridPos) &&
                                (playerHorizontalAttackCoord <= player.board.LastHorizontalGridPos - 1))
                            {
                                if (enemy.board.GeneratedBoard[playerVerticalAttackCoord, playerHorizontalAttackCoord] == 2 ||
                                    enemy.board.GeneratedBoard[playerVerticalAttackCoord, playerHorizontalAttackCoord] == 3)
                                {
                                    UI.PrintPositionAlreadyHit();
                                }
                                else
                                {
                                    if (enemy.board.LaunchAttack(playerVerticalAttackCoord, playerHorizontalAttackCoord))
                                    {
                                        UI.PrintPlayerHitMessage();
                                        if (enemy.board.CheckIfDefeated())
                                        {
                                            UI.PrintVictoryMessage();
                                            break;
                                        }
                                        else
                                        {
                                            continue;
                                        }
                                    }
                                    else
                                    {
                                        UI.PrintMissMessage();
                                    }

                                    // Executing enemy move and checking if he hit any ship
                                    while (player.board.LaunchAttack(enemy.GetRandomVerticalCoord(), enemy.GetRandomHorizontalCoord()))
                                    {
                                        UI.PrintEnemyHitOnPlayer();
                                        if (player.board.CheckIfDefeated())
                                        {
                                            break;
                                        }
                                    }

                                    if (player.board.CheckIfDefeated())
                                    {
                                        UI.PrintPlayerDefeat();
                                        break;
                                    }

                                    turnCount++;
                                }
                            }
                            else
                            {
                                UI.PrintWrongInput(1);
                            }
                        }
                        else
                        {
                            UI.PrintWrongInput();
                        }
                    }
                }
                else
                {
                    UI.PrintWrongInput(2);
                }
            }
        }
    }
}