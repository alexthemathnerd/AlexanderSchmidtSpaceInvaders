using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

namespace SpaceInvaders.Model
{
    /// <summary>
    /// Handles the Player
    /// </summary>
    public class PlayerManager
    {

        private const double PlayerShipBottomOffset = 30;

        private Canvas canvas;
        private PlayerShip player;

        /// <summary>
        /// Gets or sets the player bullet.
        /// </summary>
        /// <value>
        /// The player bullet.
        /// </value>
        public Bullet PlayerBullet { get; set; }

        /// <summary>
        /// Gets the bullets.
        /// </summary>
        /// <value>
        /// The bullets.
        /// </value>
        public List<Bullet> Bullets { get; private set; }

        /// <summary>
        /// Occurs when [enemy bullet collide event].
        /// </summary>
        public event EventHandler<CollisionEventArgs> EnemyBulletCollideEvent;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerManager"/> class.
        /// </summary>
        /// <param name="canvas">The canvas.</param>
        public PlayerManager(Canvas canvas)
        {
            this.canvas = canvas;
            this.PlayerBullet = null;
            this.Bullets = new List<Bullet>();
            this.initialize(canvas);
        }

        private void initialize(Canvas canvas)
        {
            this.createAndPlacePlayerShip();
        }

        private void createAndPlacePlayerShip()
        {
            this.player = new PlayerShip();
            this.canvas.Children.Add(this.player.Sprite);
            this.placePlayerShipNearBottomOfBackgroundCentered();
        }

        private void placePlayerShipNearBottomOfBackgroundCentered()
        {
            this.player.X = this.canvas.Width / 2 - this.player.Width / 2.0;
            this.player.Y = this.canvas.Height - this.player.Height - PlayerShipBottomOffset;
        }

        /// <summary>
        /// Checks the collision of the player and the given bullet
        /// </summary>
        /// <param name="bullet">The bullet.</param>
        public void CheckCollision(Bullet bullet)
        {
            var x1 = bullet.X + bullet.Width / 2;
            var y1 = bullet.Y + bullet.Height / 2;
            var x2 = this.player.X + this.player.Width / 2;
            var y2 = this.player.Y + this.player.Height / 2;
            var distance = DistanceCalculator.CalculateDistance(x1, y1, x2, y2);
            if (distance < (bullet.Width + this.player.Width) / 2)
            {
                this.Bullets.Remove(this.PlayerBullet);
                this.EnemyBulletCollideEvent?.Invoke(this.player, new CollisionEventArgs(bullet));
                
            }
        }

        /// <summary>
        /// Shoots the player bullet.
        /// </summary>
        public void ShotBullet()
        {
            if (this.PlayerBullet == null)
            {
                this.PlayerBullet = new Bullet(this.player);
                this.Bullets.Add(this.PlayerBullet);
                this.canvas.Children.Add(this.PlayerBullet.Sprite);
            }
        }

        /// <summary>
        /// Moves the player bullet.
        /// </summary>
        public void MoveBullet()
        {
            this.PlayerBullet?.MoveUp();
            if (this.PlayerBullet?.Y < 0)
            {
                this.Bullets.Remove(this.PlayerBullet);
                this.canvas.Children.Remove(this.PlayerBullet.Sprite);
                this.PlayerBullet = null;
            }
        }

        /// <summary>
        /// Moves the player.
        /// </summary>
        /// <param name="direction">The direction to Move the player</param>
        public void MovePlayer(Direction direction)
        {
            if (direction.Equals(Direction.Left))
            {
                this.movePlayerToLeft();
            }
            else
            {
               this.movePlayerToRight();
            }
        }

        private void movePlayerToLeft()
        {
            if (this.player.X >= 0)
            {
                this.player.MoveLeft();
            }
        }

        private void movePlayerToRight()
        {
            if (this.player.X <= this.canvas.Width - this.player.Width)
            {
                this.player.MoveRight();
            }
        }

    }
}
