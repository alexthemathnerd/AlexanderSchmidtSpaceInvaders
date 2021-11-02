using System;
using Windows.UI.Xaml.Controls;
using SpaceInvaders.View.Sprites;

namespace SpaceInvaders.Model.Enemies
{

    /// <summary>
    /// Represents the Mother Ship 
    /// </summary>
    /// <seealso cref="SpaceInvaders.Model.Enemies.EnemyShip" />
    class MotherShip : EnemyShip, IShoot
    {

        private const double ChanceToShoot = 0.1;

        /// <summary>
        /// Initializes a new instance of the <see cref="MotherShip"/> class.
        /// </summary>
        public MotherShip() : base(5, 0, 3)
        {
            this.Sprite = new MotherShipSprite();
        }

        /// <summary>
        /// Makes the MotherShip Shoot
        /// </summary>
        public Bullet Shoot()
        {
            Random rand = new Random();
            int doShoot = rand.Next(0, 100);
            if (doShoot < ChanceToShoot)
            { 
                return new Bullet(this);
            }

            return null;
        }
    }
}
