using System;

namespace SpaceInvaders.Model
{

    /// <summary>
    /// Event Args for LevelChange Event
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class LevelChangeEventArgs : EventArgs
    {

        /// <summary>
        /// Creates new level;.
        /// </summary>
        /// <value>
        /// The new level.
        /// </value>
        public int NewLevel { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LevelChangeEventArgs"/> class.
        /// </summary>
        /// <param name="newLevel">The new level.</param>
        public LevelChangeEventArgs(int newLevel)
        {
            this.NewLevel = newLevel;
        }

    }
}
