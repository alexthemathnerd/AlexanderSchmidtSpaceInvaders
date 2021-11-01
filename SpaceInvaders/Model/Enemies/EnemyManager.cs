using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI;
using Windows.UI.Xaml.Controls;

namespace SpaceInvaders.Model.Enemies
{

    /// <summary>
    /// Handles Enemies
    /// </summary>
    public class EnemyManager
    {

        private const int AlienShipCount = 4;
        private const int MotherShipCount = 4;
        private const int EnemyGap = 25;

        private readonly IList<EnemyShip> enemies;
        private readonly double backgroundHeight;
        private readonly double backgroundWidth;

        /// <summary>
        /// Gets a value indicating whether this instance has more enemies.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has more enemies; otherwise, <c>false</c>.
        /// </value>
        public bool HasMoreEnemies => this.enemies.Count != 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnemyManager"/> class.
        /// </summary>
        /// <param name="backgroundHeight">Height of the background.</param>
        /// <param name="backgroundWidth">Width of the background.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// backgroundHeight less than or equal to 0
        /// or
        /// backgroundWidth less than or equal to 0
        /// </exception>
        public EnemyManager(double backgroundHeight, double backgroundWidth)
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
            this.enemies = new List<EnemyShip>();
        }

        /// <summary>
        /// Initializes the enemies.
        /// </summary>
        /// <param name="background">The background.</param>
        public void InitializeEnemies(Canvas background)
        {
            this.createAndPlaceAlienShips(background, EnemyGap, Color.FromArgb(255, 0, 255, 0), false);
            this.createAndPlaceAlienShips(background, EnemyGap + 64, Color.FromArgb(255, 0, 255,255), true);
            this.createAndPlaceMotherShips(background,EnemyGap * 3 + 64 * 2);
        }

        private void createAndPlaceAlienShips(Canvas background, int yPos, Color color, bool isAdvance)
        {
            int prevX = (int) ((this.backgroundWidth - (64 * AlienShipCount + EnemyGap * (AlienShipCount - 1))) / 2);
            for (int i = 0; i < AlienShipCount; i++)
            {
                EnemyShip enemy = new AlienShip(color, isAdvance);
                this.enemies.Add(enemy);
                background.Children.Add(enemy.Sprite);
                enemy.X = prevX;
                enemy.Y = yPos;
                prevX += 64 + EnemyGap;
            }
        }

        private void createAndPlaceMotherShips(Canvas background, int yPos)
        {
            int prevX = (int)((this.backgroundWidth - (64 * MotherShipCount + EnemyGap * (MotherShipCount - 1))) / 2);
            for (int i = 0; i < MotherShipCount; i++)
            {
                EnemyShip enemy = new MotherShip();
                this.enemies.Add(enemy);
                background.Children.Add(enemy.Sprite);
                enemy.X = prevX;
                enemy.Y = yPos;
                prevX += 64 + EnemyGap;
            }
        }

        /// <summary>
        /// Moves the enemies.
        /// </summary>
        public void MoveEnemies()
        {
            foreach (var aEnemy in this.enemies)
            {
                if (aEnemy.ShouldGoLeft)
                {
                    aEnemy.MoveLeft();
                }
                else
                {
                    aEnemy.MoveRight();
                }
            }
        }

        /// <summary>
        /// Moves the mother ships shots.
        /// </summary>
        /// <param name="gameManager">The game manager.</param>
        /// <param name="canvas">The canvas.</param>
        public void MoveMotherShipsShots(GameManager gameManager, Canvas canvas)
        {
            foreach (var aEnemy in this.enemies)
            {
                if (aEnemy is MotherShip ship)
                {
                    ship.MoveShots(gameManager, canvas);
                }
            }
        }

        /// <summary>
        /// Checks and resolve collisions.
        /// </summary>
        /// <param name="bullet">The bullet.</param>
        /// <param name="canvas">The canvas.</param>
        /// <param name="prevScore">The previous score.</param>
        /// <param name="score">The score.</param>
        /// <returns>true if a collision occurred</returns>
        public bool CheckAndResolveCollisions(Bullet bullet, Canvas canvas, int prevScore, out int score)
        {
            try
            {
                var collidedShip = this.enemies.First((enemy) => checkIndividualCollision(enemy, bullet));
                if (collidedShip is MotherShip motherShip)
                {
                    motherShip.DestroyBullet(canvas);
                    score = prevScore + 3;
                } else if (collidedShip is AlienShip alienShip)
                {
                    if (!alienShip.IsAdvance)
                    {
                        score = prevScore + 2;
                    }
                    else
                    {
                        score = prevScore + 1;
                    }
                    
                }
                else
                {
                    score = prevScore;
                }
                this.enemies.Remove(collidedShip);
                canvas.Children.Remove(collidedShip.Sprite);
                return true;
            }
            catch (InvalidOperationException)
            {
                score = prevScore;
                return false;
            }
        }

        /// <summary>
        /// Makes the mother ships shoot.
        /// </summary>
        /// <param name="canvas">The canvas.</param>
        public void MakeMotherShipsShoot(Canvas canvas)
        {
            foreach (var aShip in this.enemies)
            {
                if (aShip is MotherShip ship)
                {
                    ship.Shoot(canvas);
                }
            }
        }
        
        private bool checkIndividualCollision(EnemyShip enemy, Bullet bullet)
        {
            var x1 = bullet.X + bullet.Width / 2;
            var y1 = bullet.Y + bullet.Height / 2;
            var x2 = enemy.X + enemy.Width / 2;
            var y2 = enemy.Y + enemy.Height / 2;
            var dist = calculateDistance(x1, y1, x2, y2);
            return dist < (bullet.Width + enemy.Width)/ 2 ;
        }

        private double calculateDistance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
        }
    }
}
