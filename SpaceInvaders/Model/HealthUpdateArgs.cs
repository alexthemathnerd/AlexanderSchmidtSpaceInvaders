using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.Model
{

    /// <summary>
    /// Args for the Health Update Event
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class HealthUpdateArgs : EventArgs
    {

        /// <summary>
        /// Creates new health.
        /// </summary>
        /// <value>
        /// The new health.
        /// </value>
        public int NewHealth { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="HealthUpdateArgs"/> class.
        /// </summary>
        /// <param name="newHealth">The new health.</param>
        public HealthUpdateArgs(int newHealth)
        {
            this.NewHealth = newHealth;
        }

    }
}
