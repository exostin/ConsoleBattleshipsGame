using System;

namespace BattleshipsGame
{
    class Enemy : Player
    {
        public Random rand = new Random();
       
        public int GetRandomVerticalCoord()
        {
            return rand.Next(board.firstGridPos, board.LastVerticalGridPos + 1);
        }
        public int GetRandomHorizontalCoord()
        {
            return rand.Next(board.firstGridPos, board.LastHorizontalGridPos + 1);
        }
    }
}