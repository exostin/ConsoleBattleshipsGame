using System;

namespace BattleshipsGame
{
    class Enemy : Player
    {
        public Random rand = new Random();
       
        public int GetRandomVerticalCoord()
        {
            return rand.Next(board.FirstGridPos, board.LastVerticalGridPos + 1);
        }
        public int GetRandomHorizontalCoord()
        {
            return rand.Next(board.FirstGridPos, board.LastHorizontalGridPos + 1);
        }
    }
}