using SpaceInvaders.View.Sprites;
using System;
using Windows.UI.Xaml.Controls;

namespace SpaceInvaders.Model
{

    /// <summary>
    /// Represents a Bullet
    /// </summary>
    /// <seealso cref="SpaceInvaders.Model.GameObject" />
    public class Bullet : GameObject
    {

        /// <summary>
        /// Gets the owner.
        /// </summary>
        /// <value>
        /// The owner.
        /// </value>
        public GameObject Owner { get; }
        public Direction Direction { get; set; } 

        /// <summary>
        /// Initializes a new instance of the <see cref="Bullet"/> class.
        /// </summary>
        /// <param name="owner">the owner of the bullet</param>
        public Bullet(GameObject owner)
        {
            this.Owner = owner;
            this.SetSpeed(0, 5);
            this.Sprite = new BulletSprite();
            this.X = this.Owner.X + this.Owner.Width / 2.0 - this.Width / 2;
            this.Y = this.Owner.Y + this.Height / 2;
        }

        public Bullet(GameObject owner, double[] playerlocation)
        {
            this.Owner = owner;
            this.Sprite = new BulletSprite();
            var trajectoryToTarget = this.trajectoryToTarget(playerlocation);
            this.SetSpeed(trajectoryToTarget[0], trajectoryToTarget[1]);
            this.X = this.Owner.X + this.Owner.Width / 2.0 - this.Width / 2;
            this.Y = this.Owner.Y + this.Height / 2;
        }

        private int[] trajectoryToTarget(double[] playerlocation)
        {
            int speedX = (int)Math.Abs((playerlocation[0] - Owner.X) / 100);
            int speedY = (int)((playerlocation[1] - Owner.Y) / 100);

            if (Owner.X > ((Canvas)this.Owner.Sprite.Parent).Width/2)
            {
                this.Direction = Direction.Left;
            }
            else
            {
                this.Direction = Direction.Right;
            }

            return new int[] {speedX,speedY};
        }

        public void Move()
        {
          
            if(this.Direction == Direction.Left)
            {
                this.MoveLeft();
            }
            else
            {
                this.MoveRight();
            }
            this.MoveDown();
        }
    }
}
