namespace Uzdevums1.Enums
{
    /// <summary>
    /// Contains the list of actions user can take
    /// </summary>
    public enum ActionEnum
    {
        /// <summary>
        /// Indicates lack of action
        /// </summary>
        Continue = 0,
        /// <summary>
        /// Indicates need to exit the process
        /// </summary>
        Exit = 1,
        /// <summary>
        /// Indicates need to set a new point
        /// </summary>
        AddNextPoint = 2,
        /// <summary>
        /// DEFUNCT - Indicates need to display the points on a field
        /// </summary>
        ShowPoints = 3,
        /// <summary>
        /// Indicates need to restart the algorithm
        /// </summary>
        Restart = 4
    }
}
