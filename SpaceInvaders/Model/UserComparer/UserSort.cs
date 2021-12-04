using System;
using System.Collections.Generic;

namespace SpaceInvaders.Model.UserComparer
{
    /// <summary>
    /// Describes a Specific Sort with a name; 
    /// </summary>
    public struct UserSort
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public String Name { get; set; }

        /// <summary>
        /// Gets or sets the sort.
        /// </summary>
        /// <value>
        /// The sort.
        /// </value>
        public IComparer<User> Sort { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
