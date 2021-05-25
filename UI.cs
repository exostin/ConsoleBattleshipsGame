using System;

namespace BattleshipsGame
{
    internal static class UI
    {
        public static void PrintPlayerAttackPrompt()
        {
            Console.Write("Enter the attack coordinates (vert/hor 1-10 ex. 9,1): ");
        }

        public static void PrintEnemyGrid(int lastHorizontalGridPos, int lastVerticalGridPos, int[,] enemyGrid)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("|   ENEMY GRID   |");
            Console.ResetColor();
            for (int i = 1; i < lastHorizontalGridPos; i++)
            {
                for (int j = 1; j < lastVerticalGridPos; j++)
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

        public static void PrintPlayerGrid(int lastHorizontalGridPos, int lastVerticalGridPos, int[,] playerGrid)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("|   YOUR GRID   |");
            Console.ResetColor();
            for (int i = 1; i < lastHorizontalGridPos; i++)
            {
                for (int j = 1; j < lastVerticalGridPos; j++)
                {
                    Console.Write(string.Format("{0} ", playerGrid[i, j]));
                }
                Console.Write(Environment.NewLine);
            }
        }

        public static void PrintTurnCounter(int turnCount)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"#  Turn no. {turnCount}   #");
            Console.ResetColor();
        }
    }
}