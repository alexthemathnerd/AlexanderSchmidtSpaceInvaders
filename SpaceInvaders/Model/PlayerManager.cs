using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Windows.UI.Xaml.Controls;

namespace SpaceInvaders.Model
{
    /// <summary>
    ///     Handles the Player
    /// </summary>
    public class PlayerManager
    {
        #region Data members

        private const double PlayerShipBottomOffset = 30;
        private const double PlayerShipMultiplayerGap = 50;
        private const long ShootCooldown = 500;
        private const int PowerUpLength = 5;
        private readonly Canvas canvas;
        private DateTime lastShotFiredPlayer1;
        private DateTime lastShotFiredPlayer2;
        private int maxBulletsFireable = 6;
        private DateTime powerupStart;
        private int numberOfPlayers;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the player 1.
        /// </summary>
        /// <value>
        ///     The player.
        /// </value>
        public PlayerShip Player1 { get; private set; }

        /// <summary>
        ///     Gets the player 2.
        /// </summary>
        /// <value>
        ///     The player.
        /// </value>
        public PlayerShip Player2 { get; private set; }

        /// <summary>
        ///     Gets the bullets of Player1.
        /// </summary>
        /// <value>
        ///     The bullets.
        /// </value>
        public List<Bullet> BulletsPlayer1 { get; }

        /// <summary>
        ///     Gets the bullets of Player2.
        /// </summary>
        /// <value>
        ///     The bullets.
        /// </value>
        public List<Bullet> BulletsPlayer2 { get; }

        /// <summary>
        ///     Gets the player 1 health.
        /// </summary>
        /// <value>
        ///     The player health.
        /// </value>
        public int Player1Health { get; private set; }

        /// <summary>
        ///     Gets the player 2 health.
        /// </summary>
        /// <value>
        ///     The player health.
        /// </value>
        public int Player2Health { get; private set; }

        public int TotalHealth => this.Player1Health + this.Player2Health;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PlayerManager" /> class.
        /// </summary>
        /// <param name="canvas">The canvas.</param>
        public PlayerManager(Canvas canvas)
        {
            this.canvas = canvas;
            this.BulletsPlayer1 = new List<Bullet>();
            this.BulletsPlayer2 = new List<Bullet>();
            this.lastShotFiredPlayer1 = DateTime.MinValue;
            this.lastShotFiredPlayer2 = DateTime.MinValue;
            this.Player1Health = 3;
            this.Player2Health = 0;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Occurs when [enemy bullet collide event].
        /// </summary>
        public event EventHandler<CollisionEventArgs> EnemyBulletCollideEvent;

        /// <summary>
        ///     Initializes this instance.
        /// </summary>
        public void Initialize(int players)
        {
            this.BulletsPlayer1.Clear();
            this.BulletsPlayer2.Clear();
            this.numberOfPlayers = players;
            if (this.numberOfPlayers == 2)
            {
                this.Player2Health = 3;
                this.createAndPlacePlayerShips();
            }
            else
            {
                this.createAndPlacePlayerShip();
            }
        }

        private void createAndPlacePlayerShips()
        {
            this.canvas.Children.Remove(this.Player1?.Sprite);
            this.canvas.Children.Remove(this.Player2?.Sprite);
            this.Player1 = new PlayerShip();
            this.Player2 = new PlayerShip();
            this.canvas.Children.Add(this.Player1.Sprite);
            this.canvas.Children.Add(this.Player2.Sprite);
            this.placePlayerShipsNearBottomOfBackgroundCentered();
        }

        private void createAndPlacePlayerShip()
        {
            this.canvas.Children.Remove(this.Player1?.Sprite);
            this.Player1 = new PlayerShip();
            this.canvas.Children.Add(this.Player1.Sprite);
            this.placePlayerShipNearBottomOfBackgroundCentered();
        }

        private void placePlayerShipNearBottomOfBackgroundCentered()
        {
            this.Player1.X = this.canvas.Width / 2 - this.Player1.Width / 2.0;
            this.Player1.Y = this.canvas.Height - this.Player1.Height - PlayerShipBottomOffset;
        }

        private void placePlayerShipsNearBottomOfBackgroundCentered()
        {
            this.Player1.X = this.canvas.Width / 2 - this.Player1.Width / 2.0 - PlayerShipMultiplayerGap;
            this.Player1.Y = this.canvas.Height - this.Player1.Height - PlayerShipBottomOffset;
            this.Player2.X = this.canvas.Width / 2 - this.Player2.Width / 2.0 + PlayerShipMultiplayerGap;
            this.Player2.Y = this.canvas.Height - this.Player2.Height - PlayerShipBottomOffset;
        }

        /// <summary>
        ///     Checks the collision of the player and the given bullet
        /// </summary>
        /// <param name="bullet">The bullet.</param>
        public void CheckCollision(Bullet bullet)
        {
            var x1 = bullet.X + bullet.Width / 2;
            var y1 = bullet.Y + bullet.Height / 2;
            var x2 = this.Player1.X + this.Player1.Width / 2;
            var y2 = this.Player1.Y + this.Player1.Height / 2;
            var distance = DistanceCalculator.CalculateDistance(x1, y1, x2, y2);

            if (this.Player1 != null && distance < (bullet.Width + this.Player1.Width) / 2)
            {
                SoundManager.Play(SoundEffectsEnum.PlayerDestroyed);
                this.Player1Health--;
                this.EnemyBulletCollideEvent?.Invoke(this.Player1, new CollisionEventArgs(bullet));
            }

            if (this.Player2 != null)
            {
                this.handlePlayer2Collision(bullet);
            }
        }

        private void handlePlayer2Collision(Bullet bullet)
        {
            var x1 = bullet.X + bullet.Width / 2;
            var y1 = bullet.Y + bullet.Height / 2;
            var x2 = this.Player2.X + this.Player2.Width / 2;
            var y2 = this.Player2.Y + this.Player2.Height / 2;
            var distance = DistanceCalculator.CalculateDistance(x1, y1, x2, y2);

            if (distance < (bullet.Width + this.Player2.Width) / 2)
            {
                Debug.WriteLine("PLAYER 2 DIE");
                SoundManager.Play(SoundEffectsEnum.PlayerDestroyed);
                this.Player2Health--;
                this.EnemyBulletCollideEvent?.Invoke(this.Player2, new CollisionEventArgs(bullet));
            }
        }

        /// <summary>
        ///     Shoots the player 1 bullet.
        /// </summary>
        public void ShootPlayer1Bullet()
        {
            if (DateTime.Now.Subtract(this.powerupStart).Seconds > PowerUpLength)
            {
                this.maxBulletsFireable = 3;
            }

            if (this.BulletsPlayer1.Count < this.maxBulletsFireable &&
                DateTime.Now.Subtract(this.lastShotFiredPlayer1).Milliseconds > ShootCooldown)
            {
                SoundManager.Play(SoundEffectsEnum.PlayerFire);
                this.lastShotFiredPlayer1 = DateTime.Now;
                var bullet = new Bullet(this.Player1);
                this.BulletsPlayer1.Add(bullet);
                this.canvas.Children.Add(bullet.Sprite);
            }
        }

        /// <summary>
        ///     Shoots the player 2 bullet.
        /// </summary>
        public void ShootPlayer2Bullet()
        {
            if (DateTime.Now.Subtract(this.powerupStart).Seconds > PowerUpLength)
            {
                this.maxBulletsFireable = 3;
            }

            if (this.BulletsPlayer2.Count < this.maxBulletsFireable &&
                DateTime.Now.Subtract(this.lastShotFiredPlayer2).Milliseconds > ShootCooldown)
            {
                SoundManager.Play(SoundEffectsEnum.PlayerFire);
                this.lastShotFiredPlayer2 = DateTime.Now;
                var bullet = new Bullet(this.Player2);
                this.BulletsPlayer2.Add(bullet);
                this.canvas.Children.Add(bullet.Sprite);
            }
        }

        /// <summary>
        ///     Moves the player bullet.
        /// </summary>
        public void MoveBullets()
        {
            foreach (var aBullet in new List<Bullet>(this.BulletsPlayer1.Union(this.BulletsPlayer2)))
            {
                aBullet.MoveUp();
                if (aBullet.Y < 0)
                {
                    if (aBullet.Owner.Equals(this.Player1))
                    {
                        this.BulletsPlayer1.Remove(aBullet);
                    }
                    else
                    {
                        this.BulletsPlayer2.Remove(aBullet);
                    }

                    this.canvas.Children.Remove(aBullet.Sprite);
                }
            }
        }

        /// <summary>
        ///     Moves the player 1.
        /// </summary>
        /// <param name="direction">The direction to Move the player</param>
        public void MovePlayer1(Direction direction)
        {
            if (direction.Equals(Direction.Left))
            {
                this.movePlayerToLeft(this.Player1);
            }
            else
            {
                this.movePlayerToRight(this.Player1);
            }
        }

        /// <summary>
        ///     Moves the player 2.
        /// </summary>
        /// <param name="direction">The direction to Move the player</param>
        public void MovePlayer2(Direction direction)
        {
            if (direction.Equals(Direction.Left))
            {
                this.movePlayerToLeft(this.Player2);
            }
            else
            {
                this.movePlayerToRight(this.Player2);
            }
        }

        private void movePlayerToLeft(PlayerShip player)
        {
            if (player.X >= 0)
            {
                player.MoveLeft();
            }
        }

        private void movePlayerToRight(PlayerShip player)
        {
            if (player.X <= this.canvas.Width - player.Width)
            {
                player.MoveRight();
            }
        }

        /// <summary>
        ///     Powers up the player.
        /// </summary>
        public void PowerUp()
        {
            this.maxBulletsFireable *= 2;
            this.powerupStart = DateTime.Now;
        }

        #endregion
    }
}