using System;
using System.Diagnostics;
using Windows.UI.Xaml.Controls;
using SpaceInvaders.Model.Enemies;

namespace SpaceInvaders.Model
{
    /// <summary>
    ///     Manages the entire game.
    /// </summary>
    public class GameManager
    {
        #region Data members

        private const double PlayerShipBottomOffset = 30;
        private readonly double backgroundHeight;
        private readonly double backgroundWidth;

        private PlayerShip playerShip;
        private Bullet playerBullet;

        /// <summary>
        /// Gets the score.
        /// </summary>
        /// <value>
        /// The score.
        /// </value>
        public int Score { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance is game over.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is game over; otherwise, <c>false</c>.
        /// </value>
        public bool IsGameOver { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="GameManager" /> class.
        ///     Precondition: backgroundHeight > 0 AND backgroundWidth > 0
        /// </summary>
        /// <param name="backgroundHeight">The backgroundHeight of the game play window.</param>
        /// <param name="backgroundWidth">The backgroundWidth of the game play window.</param>
        public GameManager(double backgroundHeight, double backgroundWidth)
        {
            if (backgroundHeight <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(backgroundHeight));
            }

            if (backgroundWidth <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(backgroundWidth));
            }

            this.backgroundHeight = backgroundHeight;
            this.backgroundWidth = backgroundWidth;
            this.playerBullet = null;
            this.Score = 0;
            this.IsGameOver = false;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Initializes the game placing player ship and enemy ship in the game.
        ///     Precondition: background != null
        ///     Postcondition: Game is initialized and ready for play.
        /// </summary>
        /// <param name="background">The background canvas.</param>
        public void InitializeGame(Canvas background)
        {
            if (background == null)
            {
                throw new ArgumentNullException(nameof(background));
            }

            this.createAndPlacePlayerShip(background);
        }

        private void createAndPlacePlayerShip(Canvas background)
        {
            this.playerShip = new PlayerShip();
            background.Children.Add(this.playerShip.Sprite);

            this.placePlayerShipNearBottomOfBackgroundCentered();
        }

        private void placePlayerShipNearBottomOfBackgroundCentered()
        {
            this.playerShip.X = this.backgroundWidth / 2 - this.playerShip.Width / 2.0;
            this.playerShip.Y = this.backgroundHeight - this.playerShip.Height - PlayerShipBottomOffset;
        }

        /// <summary>
        /// Shoots the player bullet.
        /// </summary>
        /// <param name="canvas">The canvas.</param>
        public void ShootPlayerBullet(Canvas canvas)
        {
            if (this.playerBullet == null)
            {
                Debug.WriteLine("I Shoot");
                this.playerBullet = new Bullet(this.playerShip.X + this.playerShip.Width / 2.0, this.playerShip.Y);
                canvas.Children.Add(this.playerBullet.Sprite);
            }
        }

        /// <summary>
        /// Moves the player bullet.
        /// </summary>
        /// <param name="enemyManager">The enemy manager.</param>
        /// <param name="canvas">The canvas.</param>
        public void MovePlayerBullet(EnemyManager enemyManager, Canvas canvas)
        {
            if (this.playerBullet != null)
            {
                this.playerBullet.MoveUp();
                int score;
                if (enemyManager.CheckAndResolveCollisions(this.playerBullet, canvas, this.Score, out score) || this.playerBullet.Y < 0)
                {
                    canvas.Children.Remove(this.playerBullet.Sprite);
                    this.playerBullet = null;
                    this.Score = score;
                }
            }
        }

        /// <summary>
        /// Checks and resolve collisions.
        /// </summary>
        /// <param name="bullet">The bullet.</param>
        /// <param name="canvas">The canvas.</param>
        /// <returns>true if a collision occurred</returns>
        public bool CheckAndResolveCollisions(Bullet bullet, Canvas canvas)
        {
            var shouldCollide = checkIndividualCollision(this.playerShip, bullet);

            if (shouldCollide)
            {
                this.IsGameOver = true;
                canvas.Children.Remove(this.playerShip.Sprite);
            }
            return shouldCollide;
        }

        private bool checkIndividualCollision(PlayerShip player, Bullet bullet)
        {
            var x1 = bullet.X + bullet.Width / 2;
            var y1 = bullet.Y + bullet.Height / 2;
            var x2 = player.X + player.Width / 2;
            var y2 = player.Y + player.Height / 2;
            var dist = calculateDistance(x1, y1, x2, y2);
            return dist < (bullet.Width + player.Width) / 2;
        }

        private double calculateDistance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
        }

        /// <summary>
        ///     Moves the player ship to the left.
        ///     Precondition: none
        ///     Postcondition: The player ship has moved left.
        /// </summary>
        public void MovePlayerShipLeft()
        {
            if (this.playerShip.X >= 0)
            {
                this.playerShip.MoveLeft();
            }
        }

        /// <summary>
        ///     Moves the player ship to the right.
        ///     Precondition: none
        ///     Postcondition: The player ship has moved right.
        /// </summary>
        public void MovePlayerShipRight()
        {
            if (this.playerShip.X <= this.backgroundWidth - this.playerShip.Width)
            {
                this.playerShip.MoveRight();
            }
        }

        #endregion


    }
}
