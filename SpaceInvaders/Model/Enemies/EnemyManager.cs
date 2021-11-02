using System;
using System.Collections.Generic;
using Windows.UI;
using Windows.UI.Xaml.Controls;

namespace SpaceInvaders.Model.Enemies
{
    /// <summary>
    ///     Handles Enemies
    /// </summary>
    public class EnemyManager
    {
        #region Data members

        private const int AlienShipCount = 4;
        private const int MotherShipCount = 4;
        private const int EnemyGap = 25;
        private const int EnemyWidth = 64;

        private readonly IList<EnemyShip> enemies;
        private readonly Canvas canvas;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets a value indicating whether this instance has more enemies.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance has more enemies; otherwise, <c>false</c>.
        /// </value>
        public bool HasMoreEnemies => this.enemies.Count != 0;

        /// <summary>
        /// The bullets in play.
        /// </summary>
        public List<Bullet> Bullets { get; }

        /// <summary>
        /// Occurs when [player bullet collide event].
        /// </summary>
        public event EventHandler<CollisionEventArgs> PlayerBulletCollideEvent;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="EnemyManager" /> class.
        /// </summary>
        /// <param name="canvas">the canvas the enemies will be drawn on</param>
        /// <exception cref="System.ArgumentOutOfRangeException">
        ///     backgroundHeight less than or equal to 0
        ///     or
        ///     backgroundWidth less than or equal to 0
        /// </exception>
        public EnemyManager(Canvas canvas)
        {
            this.canvas = canvas;
            this.enemies = new List<EnemyShip>();
            this.Bullets = new List<Bullet>();
            this.initializeEnemies(canvas);
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Initializes the enemies.
        /// </summary>
        /// <param name="background">The background.</param>
        private void initializeEnemies(Canvas background)
        {
            this.createAndPlaceMotherShips(background, EnemyGap);
            this.createAndPlaceAlienShips(background, EnemyGap + EnemyWidth, Color.FromArgb(255, 0, 255, 255), true, 2);
            this.createAndPlaceAlienShips(background, EnemyGap * 3 + EnemyWidth * 2, Color.FromArgb(255, 0, 255, 0),
                false, 1);
        }

        private void createAndPlaceAlienShips(Canvas background, int yPos, Color color, bool isAdvance, int score)
        {
            var prevX = (int)((this.canvas.Width - (EnemyWidth * AlienShipCount + EnemyGap * (AlienShipCount - 1))) /
                              2);
            for (var i = 0; i < AlienShipCount; i++)
            {
                EnemyShip enemy = new AlienShip(color, isAdvance, score);
                this.enemies.Add(enemy);
                background.Children.Add(enemy.Sprite);
                enemy.X = prevX;
                enemy.Y = yPos;
                prevX += EnemyWidth + EnemyGap;
            }
        }

        private void createAndPlaceMotherShips(Canvas background, int yPos)
        {
            var prevX = (int)((this.canvas.Width - (EnemyWidth * MotherShipCount + EnemyGap * (MotherShipCount - 1))) /
                              2);
            for (var i = 0; i < MotherShipCount; i++)
            {
                EnemyShip enemy = new MotherShip();
                this.enemies.Add(enemy);
                background.Children.Add(enemy.Sprite);
                enemy.X = prevX;
                enemy.Y = yPos;
                prevX += EnemyWidth + EnemyGap;
            }
        }

        /// <summary>
        ///     Moves the enemies.
        /// </summary>
        public void MoveEnemies()
        {
            foreach (var aEnemy in this.enemies)
            {
                aEnemy.Move();
            }
        }

        /// <summary>
        ///     Shoots the bullets of the enemies
        ///     Precondition: none
        ///     Postcondition: a bullet was shot or not. It is random.
        /// </summary>
        public void ShootBullets()
        {
            foreach (var aEnemy in this.enemies)
            {
                if (aEnemy is IShoot iShootEnemy)
                {
                    var bullet = iShootEnemy.Shoot();
                    if (bullet != null)
                    {
                        this.Bullets.Add(bullet);
                        this.canvas.Children.Add(bullet.Sprite);
                    }
                }
            }
        }

        /// <summary>
        ///     Move the the bullets
        ///     Precondition: none
        ///     Postcondition: all bullets move down
        /// </summary>
        public void MoveBullets()
        {
            foreach (var aBullet in this.Bullets)
            {
                aBullet.MoveDown();
            }
        }

        /// <summary>
        /// Checks if the given bullet collides and raises a collision event.
        /// </summary>
        /// <param name="bullet">the bullet to check collision with enemies</param>
        public void CheckCollision(Bullet bullet)
        {
            EnemyShip shipToRemove = null;
            foreach (var aEnemy in this.enemies)
            {
                var x1 = bullet.X + bullet.Width / 2;
                var y1 = bullet.Y + bullet.Height / 2;
                var x2 = aEnemy.X + aEnemy.Width / 2;
                var y2 = aEnemy.Y + aEnemy.Height / 2;
                var distance = DistanceCalculator.CalculateDistance(x1, y1, x2, y2);
                if (distance < (bullet.Width + aEnemy.Width) / 2)
                {
                    shipToRemove = aEnemy;
                    this.PlayerBulletCollideEvent?.Invoke(aEnemy, new CollisionEventArgs(bullet));
                    break;
                }
            }

            if (shipToRemove != null)
            {
                this.enemies.Remove(shipToRemove);
                this.Bullets.Remove(bullet);
            }
        }

        #endregion
    }
}