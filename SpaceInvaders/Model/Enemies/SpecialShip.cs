using SpaceInvaders.View.Sprites;
using System;
using Windows.UI.Xaml.Controls;

namespace SpaceInvaders.Model.Enemies
{

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="SpaceInvaders.Model.Enemies.EnemyShip" />
    /// <seealso cref="SpaceInvaders.Model.IShoot" />
    public class SpecialShip : EnemyShip, IShoot
    {
        private const double ChanceToShoot = 1;
        public const int SpecialShipStartingY = 2;
        public const int SpecialShipStartingX = -100;

        /// <summary>
        /// Gets or sets the player location.
        /// </summary>
        /// <value>
        /// The player location.
        /// </value>
        public double[] PlayerLocation { get; set; }

        public event EventHandler LeavesScreenEvent;

        public SpecialShip() : base(2,0)
        {
            this.Sprite = new SpecialShipSprite();
            this.Score = 25;
        }

        public Bullet Shoot()
        {
            Random rand = new Random();
            int doShoot = rand.Next(100);
            if (doShoot < ChanceToShoot)
            {
                return new Bullet(this, this.PlayerLocation);
            }

            return null;
        }
        public static bool Spawn()
        {
            Random rand = new Random();
            if (rand.Next(0,100) > 90) 
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public override void Move()
        {
            this.MoveRight();

            if (this.X > (((Canvas)this.Sprite.Parent)).Width)
            {
                this.LeavesScreenEvent?.Invoke(this, EventArgs.Empty);              
            }
        }
    }
}
