using System;

namespace TSC.Game.Other
{

    /// <summary>
    /// An object containing information on a level that can be played within the game.
    /// </summary>
    [Serializable]
    public class Level
    {

        /// <summary>
        /// The target amount of money that the player must reach to win the level.
        /// </summary>
        public float moneyGoal;

        /// <summary>
        /// The index of this level, indexed from 1.
        /// </summary>
        public int level;

    }

}