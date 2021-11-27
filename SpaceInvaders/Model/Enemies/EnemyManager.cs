using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

namespace SpaceInvaders.Model.Enemies
{
    /// <summary>
    ///     Handles Enemies
    /// </summary>
    public class EnemyManager
    {
        #region Data members

        private const int AlienShipCount = 8;
        private const int AdvancedAlienShipCount = 6;
        private const int MotherShipCount = 4;
        private const int PlanetShipCount = 2;

        private const int EnemiesBulletCap = 3;

        private readonly List<EnemyShip> enemies;
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
            EnemyBuilder.BuildRow<PlanetShip>(background, PlanetShipCount, 1, this.enemies);
            EnemyBuilder.BuildRow<MotherShip>(background, MotherShipCount, 2, this.enemies);
            EnemyBuilder.BuildRow<AdvancedAlienShip>(background, AdvancedAlienShipCount, 3, this.enemies);
            EnemyBuilder.BuildRow<AlienShip>(background, AlienShipCount, 4, this.enemies);
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
                if (aEnemy is IShoot iShootEnemy && this.Bullets.Count < EnemiesBulletCap)
                {
                    var bullet = iShootEnemy.Shoot();
                    if (bullet != null)
                    {
                        SoundManager.Play(SoundEffectsEnum.EnemyFire);
                        this.Bullets.Add(bullet);
                        this.canvas.Children.Add(bullet.Sprite);
                        
                    }
                }
            }
        }

        /// <summary>
        /// Animates the enemies.
        /// </summary>
        public void AnimateEnemies()
        {
            foreach (var aEnemy in this.enemies)
            {
                if (aEnemy is IAnimate iAnimateEnemy)
                {
                    iAnimateEnemy.ChangeState();
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
            foreach (var aBullet in new List<Bullet>(this.Bullets))
            {
                aBullet.MoveDown();
                if (aBullet.Y > this.canvas.Height)
                {
                    this.canvas.Children.Remove(aBullet.Sprite);
                    this.Bullets.Remove(aBullet);
                }
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
                SoundManager.Play(SoundEffectsEnum.EnemyDestroyed);
                this.enemies.Remove(shipToRemove);
            }
        }

        #endregion
    }
}