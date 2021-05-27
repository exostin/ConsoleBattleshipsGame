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

        public void ChooseAIIntelligenceLevel()
        {
            /*...*/
        }

        /// <summary>
        /// This AI intelligence level will: attack only purely random positions
        /// </summary>
        public void Easy()
        {
            /*...*/
        }

        /// <summary>
        /// This AI intelligence level will: attack positions that haven't been hit yet
        /// </summary>
        public void Normal()
        {
            /*...*/
        }

        /// <summary>
        /// This AI intelligence level will: attack positions that aren't near to other already hit positions (CheckNearby())
        /// </summary>
        public void Hard()
        {
            /*...*/
        }

        /// <summary>
        /// This AI intelligence level will: use strategy (CheckNearby()) and already know about some ship positions
        /// </summary>
        public void Extreme()
        {
            /*...*/
        }

        /// <summary>
        /// This AI intelligence level will: know about every ship and will proceed to kill 3 per each turn
        /// </summary>
        public void God()
        {
            /*...*/
        }
    }
}