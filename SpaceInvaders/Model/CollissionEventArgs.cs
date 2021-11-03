using System;

namespace SpaceInvaders.Model
{

    /// <summary>
    /// Args for a CollisionEvent
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class CollisionEventArgs : EventArgs
    {

        /// <summary>
        /// Gets the bullet.
        /// </summary>
        /// <value>
        /// The bullet.
        /// </value>
        public Bullet Bullet { get;}

        /// <summary>
        /// Initializes a new instance of the <see cref="CollisionEventArgs"/> class.
        /// </summary>
        /// <param name="bullet">The bullet that cause the event.</param>
        public CollisionEventArgs(Bullet bullet)
        {
            this.Bullet = bullet;
        }

    }
}
