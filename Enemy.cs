using System;

namespace BattleshipsGame
{
    class Enemy : Player
    {
        public Random rand = new Random();
       
       // replace move with just a method to get random coords and pass them into LaunchAttack 

        public bool Move()
        {
            int enemyAttackRandomVerticalCoord = rand.Next(board.firstGridPos, board.LastVerticalGridPos + 1);
            int enemyAttackRandomHorizontalCoord = rand.Next(board.firstGridPos, board.LastHorizontalGridPos + 1);

            if (board.LaunchAttack(enemyAttackRandomVerticalCoord, enemyAttackRandomHorizontalCoord))
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