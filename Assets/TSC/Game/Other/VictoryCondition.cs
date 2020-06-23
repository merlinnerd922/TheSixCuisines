namespace TSC.Game.Other
{
/// <summary>
/// An enumeration of the different types of ways to win a level.
/// </summary>
    public enum VictoryCondition
    {
        /// <summary>
        /// A victory condition met by having a certain amount of cash.
        /// </summary>
        CASH = 0,
        
        /// <summary>
        /// A victory condition that wins based on selling a certain amount of a certain dish.
        /// </summary>
        DISH = 1
    }

}