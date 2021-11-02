using System;
using Windows.UI.Xaml.Controls;
using SpaceInvaders.View.Sprites;

namespace SpaceInvaders.Model.Enemies
{

    /// <summary>
    /// Represents the Mother Ship 
    /// </summary>
    /// <seealso cref="SpaceInvaders.Model.Enemies.EnemyShip" />
    public class MotherShip : EnemyShip, IShoot
    {

        private const double ChanceToShoot = 1;

        /// <summary>
        /// Initializes a new instance of the <see cref="MotherShip"/> class.
        /// </summary>
        /// <param name="score">the score of the mother ship</param>
        public MotherShip() : base(5, 0)
        {
            this.Score = 5;
            this.Sprite = new MotherShipSprite();
        }

        /// <summary>
        /// Makes the MotherShip Shoot
        /// </summary>
        public Bullet Shoot()
        {
            Random rand = new Random();
            int doShoot = rand.Next(100);
            if (doShoot < ChanceToShoot)
            { 
                return new Bullet(this);
            }

            return null;
        }
    }
}
