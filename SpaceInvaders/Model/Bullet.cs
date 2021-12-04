using System;
using Windows.UI.Xaml.Controls;
using SpaceInvaders.View.Sprites;

namespace SpaceInvaders.Model
{
    /// <summary>
    ///     Represents a Bullet
    /// </summary>
    /// <seealso cref="SpaceInvaders.Model.GameObject" />
    public class Bullet : GameObject
    {
        #region Properties

        /// <summary>
        ///     Gets the owner.
        /// </summary>
        /// <value>
        ///     The owner.
        /// </value>
        public GameObject Owner { get; }

        public Direction Direction { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Bullet" /> class.
        /// </summary>
        /// <param name="owner">the owner of the bullet</param>
        public Bullet(GameObject owner)
        {
            this.Owner = owner;
            SetSpeed(0, 5);
            Sprite = new BulletSprite();
            X = this.Owner.X + this.Owner.Width / 2.0 - Width / 2;
            Y = this.Owner.Y + Height / 2;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Bullet"/> class.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="playerLocation">The player location.</param>
        public Bullet(GameObject owner, double[] playerLocation)
        {
            this.Owner = owner;
            Sprite = new BulletSprite();
            var trajectoryToTarget = this.trajectoryToTarget(playerLocation);
            SetSpeed(trajectoryToTarget[0], trajectoryToTarget[1]);
            X = this.Owner.X + this.Owner.Width / 2.0 - Width / 2;
            Y = this.Owner.Y + Height / 2;
        }

        #endregion

        #region Methods

        private int[] trajectoryToTarget(double[] playerlocation)
        {
            var speedX = (int)Math.Abs((playerlocation[0] - this.Owner.X) / 100);
            var speedY = (int)((playerlocation[1] - this.Owner.Y) / 100);

            if (this.Owner.X > ((Canvas)this.Owner.Sprite.Parent).Width / 2)
            {
                this.Direction = Direction.Left;
            }
            else
            {
                this.Direction = Direction.Right;
            }

            return new[] { speedX, speedY };
        }

        /// <summary>
        /// Moves this instance.
        /// </summary>
        public void Move()
        {
            if (this.Direction == Direction.Left)
            {
                MoveLeft();
            }
            else
            {
                MoveRight();
            }

            MoveDown();
        }

        #endregion
    }
}