using System;
using SpaceInvaders.Model.UserComparer;

namespace SpaceInvaders.Model
{

    /// <summary>
    /// Model for a User on the Leader board
    /// </summary>
    public class User : IComparable<User>
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public String Name { get; set; }

        /// <summary>
        /// Gets or sets the score.
        /// </summary>
        /// <value>
        /// The score.
        /// </value>
        public int Score { get; set; }

        //
        public int CompletedLevel { get; set; }

        public int CompareTo(User other)
        {
            return new ScoreNameLevelComparer().Compare(this, other);
        }
    }
}
