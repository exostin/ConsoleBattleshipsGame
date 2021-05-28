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

                UI.AskForDifficulty();
                var difficultyParseSuccess = int.TryParse(Console.ReadLine(), out var difficultyIndex);
                enemy.CurrentDifficulty = difficultyIndex;

                if (configurationParseSuccess && (configurationIndex >= 1 && configurationIndex <= 5) &&
                    difficultyParseSuccess && (difficultyIndex >= 1 && difficultyIndex <= 4))
                {
                    enemy.board.PopulateBoard(player.ChooseShips(configurationIndex));
                    player.board.PopulateBoard(player.ChooseShips(configurationIndex));
                    enemy.PlayerBoardGrid = player.board.GeneratedBoard;

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
                            var horizontalAttackParseSuccess = char.TryParse(splitPlayerAttackCoords[1], out char playerHorizontalAlphabeticAttackCoord);
                            int playerHorizontalConvertedToNumericalCoord = char.ToUpper(playerHorizontalAlphabeticAttackCoord) - 64;
                            if ((verticalAttackParseSuccess && horizontalAttackParseSuccess) &&
                                // Checking if player input is in bounds of grid array in both dimensions
                                (playerVerticalAttackCoord >= player.board.FirstGridPos) &&
                                (playerVerticalAttackCoord <= player.board.LastVerticalGridPos - 1) &&
                                (playerHorizontalConvertedToNumericalCoord >= player.board.FirstGridPos) &&
                                (playerHorizontalConvertedToNumericalCoord <= player.board.LastHorizontalGridPos - 1))
                            {
                                if (enemy.board.GeneratedBoard[playerVerticalAttackCoord, playerHorizontalConvertedToNumericalCoord] == 2 ||
                                    enemy.board.GeneratedBoard[playerVerticalAttackCoord, playerHorizontalConvertedToNumericalCoord] == 3)
                                {
                                    UI.PrintPositionAlreadyHit();
                                }
                                else
                                {
                                    if (enemy.board.LaunchAttack(playerVerticalAttackCoord, playerHorizontalConvertedToNumericalCoord))
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
                                    // TODO: This can definitely be done better... reference(?)
                                    //enemy.GetPlayerBoard(ref player.board);
                                    // Executing enemy move and checking if he hit any ship
                                    while (player.board.LaunchAttack(enemy.Attack()[0], enemy.Attack()[1]))
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
                                UI.PrintWrongInput(scenario: 1);
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
                    UI.PrintWrongInput(scenario: 2);
                }
            }
        }
    }
}