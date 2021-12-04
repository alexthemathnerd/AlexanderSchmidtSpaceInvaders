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
        private const long ShootCooldown = 500;
        private const int PowerUpLength = 5;
        private readonly Canvas canvas;
        private DateTime lastShotFired;
        private int maxBulletsFireable = 6;
        private DateTime powerupStart;

        /// <summary>
        /// Gets the player.
        /// </summary>
        /// <value>
        /// The player.
        /// </value>
        public PlayerShip Player { get; private set; }

        /// <summary>
        /// Gets the bullets.
        /// </summary>
        /// <value>
        /// The bullets.
        /// </value>
        public List<Bullet> Bullets { get; }

        /// <summary>
        /// Gets the player health.
        /// </summary>
        /// <value>
        /// The player health.
        /// </value>
        public int PlayerHealth { get; private set; }


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
            this.Bullets = new List<Bullet>();
            this.lastShotFired = DateTime.MinValue; 
            this.PlayerHealth = 3;
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public void Initialize()
        {
            this.Bullets.Clear();
            this.createAndPlacePlayerShip();
        }

        private void createAndPlacePlayerShip()
        {
            this.canvas.Children.Remove(this.Player?.Sprite);
            this.Player = new PlayerShip();
            this.canvas.Children.Add(this.Player.Sprite);
            this.placePlayerShipNearBottomOfBackgroundCentered();
        }

        private void placePlayerShipNearBottomOfBackgroundCentered()
        {
            this.Player.X = this.canvas.Width / 2 - this.Player.Width / 2.0;
            this.Player.Y = this.canvas.Height - this.Player.Height - PlayerShipBottomOffset;
        }

        /// <summary>
        /// Checks the collision of the player and the given bullet
        /// </summary>
        /// <param name="bullet">The bullet.</param>
        public void CheckCollision(Bullet bullet)
        {
            var x1 = bullet.X + bullet.Width / 2;
            var y1 = bullet.Y + bullet.Height / 2;
            var x2 = this.Player.X + this.Player.Width / 2;
            var y2 = this.Player.Y + this.Player.Height / 2;
            var distance = DistanceCalculator.CalculateDistance(x1, y1, x2, y2);
            if (distance < (bullet.Width + this.Player.Width) / 2)
            {
                SoundManager.Play(SoundEffectsEnum.PlayerDestroyed);
                this.PlayerHealth--;
                this.EnemyBulletCollideEvent?.Invoke(this.Player, new CollisionEventArgs(bullet));
            }
        }

        /// <summary>
        /// Shoots the player bullet.
        /// </summary>
        public void ShootBullet()
        {
            if (DateTime.Now.Subtract(this.powerupStart).Seconds > PowerUpLength)
            {
                this.maxBulletsFireable = 3;
            }

            if (this.Bullets.Count < this.maxBulletsFireable && DateTime.Now.Subtract(this.lastShotFired).Milliseconds > ShootCooldown)
            {
                SoundManager.Play(SoundEffectsEnum.PlayerFire);
                this.lastShotFired = DateTime.Now;
                var bullet = new Bullet(this.Player);
                this.Bullets.Add(bullet);
                this.canvas.Children.Add(bullet.Sprite);

            }
            
        }

        /// <summary>
        /// Moves the player bullet.
        /// </summary>
        public void MoveBullets()
        {
            foreach (var aBullet in new List<Bullet>(this.Bullets))
            {
                aBullet.MoveUp();
                if (aBullet.Y < 0)
                {
                    this.Bullets.Remove(aBullet);
                    this.canvas.Children.Remove(aBullet.Sprite);
                }
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
            if (this.Player.X >= 0)
            {
                this.Player.MoveLeft();
            }
        }

        private void movePlayerToRight()
        {
            if (this.Player.X <= this.canvas.Width - this.Player.Width)
            {
                this.Player.MoveRight();
            }
        }

        /// <summary>
        /// Powers up the player.
        /// </summary>
        public void PowerUp()
        {
            this.maxBulletsFireable *= 2;
            this.powerupStart = DateTime.Now;
        }

    }
}
