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
        public ICollection<Bullet> Bullets { get; }

        /// <summary>
        /// Occurs when [player bullet collide event].
        /// </summary>
        public event EventHandler<CollisionEventArgs> PlayerBulletCollideEvent;

        /// <summary>
        /// Gets or sets a value indicating whether this instance has special ship.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has special ship; otherwise, <c>false</c>.
        /// </value>
        public bool HasSpecialShip { get; set; }

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
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Initializes the enemies.
        /// </summary>
        /// <param name="levelNumber">The current level to initialize</param>
        public void Initialize(int levelNumber)
        {
            this.enemies.Clear();
            this.Bullets.Clear();
            this.enemies.AddRange(EnemyBuilder.BuildLevel(this.canvas, Level.GetLevel(levelNumber)));
        }

        /// <summary>
        ///     Moves the enemies.
        /// </summary>
        public void MoveEnemies()
        {
            foreach (var aEnemy in new List<EnemyShip>(this.enemies))
            {
                aEnemy.Move();
            }
        }

        /// <summary>
        ///     Shoots the bullets of the enemies
        ///     Precondition: none
        ///     Postcondition: a bullet was shot or not. It is random.
        /// </summary>
        public void ShootBullets(GameObject player1, GameObject player2)
        {
            foreach (var aEnemy in this.enemies)
            {
                if (aEnemy is IShoot iShootEnemy && this.Bullets.Count < EnemiesBulletCap)
                {
                    Bullet bullet;
                    double[] location;
                    Random random = new Random();
                    if (random.Next(1, 101) < 50 || player2 == null)
                    {
                        location = new[] { player1.X, player1.Y };
                    }
                    else
                    {
                        location = new[] { player2.X, player2.Y };
                    }

                    switch (iShootEnemy)
                    { 
                        case PlanetShip planetShip:
                            planetShip.PlayerLocation = location;
                            bullet = ((PlanetShip)iShootEnemy).Shoot();
                            break;
                        case SpecialShip specialShip:
                            specialShip.PlayerLocation = location;
                            bullet = ((SpecialShip)iShootEnemy).Shoot();
                            break;
                        default:
                            bullet = iShootEnemy.Shoot();
                            break;
                    }
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
                aBullet.Move();
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
                this.spawnSpecialShip();
            }

        }


        private void spawnSpecialShip()
        {
            if (!HasSpecialShip && SpecialShip.Spawn())
            {
                SoundManager.Play(SoundEffectsEnum.SpecialShip);
                var specialShip = EnemyBuilder.BuildEnemy(typeof(SpecialShip), SpecialShip.SpecialShipStartingX, SpecialShip.SpecialShipStartingY);
                this.enemies.Add(specialShip);
                this.canvas.Children.Add(specialShip.Sprite);

                 this.HasSpecialShip = true;

                  ((SpecialShip)specialShip).LeavesScreenEvent += removeSpecialShipWhenOffScreen;
                
            }
        }

        private void removeSpecialShipWhenOffScreen(object sender, EventArgs e)
        {
            SoundManager.Stop(SoundEffectsEnum.SpecialShip);
            this.enemies.Remove((EnemyShip)sender);
            this.canvas.Children.Remove(((EnemyShip)sender).Sprite);
            this.HasSpecialShip = false;

        }
        #endregion
    }
}