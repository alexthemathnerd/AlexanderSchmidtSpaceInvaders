using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.Model
{

    /// <summary>
    /// Args for the Score Update Event
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class ScoreUpdateArgs : EventArgs
    {

        /// <summary>
        /// Creates new score.
        /// </summary>
        /// <value>
        /// The new score.
        /// </value>
        public int NewScore { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ScoreUpdateArgs"/> class.
        /// </summary>
        /// <param name="newScore">The new score.</param>
        public ScoreUpdateArgs(int newScore)
        {
            this.NewScore = newScore;
        }

    }
}
