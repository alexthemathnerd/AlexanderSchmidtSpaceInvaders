using System;
using SpaceInvaders.View.Sprites;

namespace SpaceInvaders.Model.Enemies
{

    /// <summary>
    /// Represents the Mother Ship 
    /// </summary>
    /// <seealso cref="SpaceInvaders.Model.Enemies.EnemyShip" />
    public class MotherShip : EnemyShip, IShoot, IAnimate
    {

        private const int ExtendCap = 10;
        private int extendCount;
        private bool shouldExtend;
        protected const double ChanceToShoot = 1;

        /// <summary>
        /// Initializes a new instance of the <see cref="MotherShip"/> class.
        /// </summary>
        public MotherShip() : base(5, 0)
        {
            this.Score = 5;
            this.Sprite = new MotherShipSprite();
            this.shouldExtend = true;
            this.extendCount = 0;
        }

        /// <summary>
        /// Makes the MotherShip Shoot
        /// </summary>
        public virtual Bullet Shoot()
        {
            Random rand = new Random();
            int doShoot = rand.Next(100);
            if (doShoot < ChanceToShoot)
            { 
                return new Bullet(this);
            }

            return null;
        }

        /// <summary>
        /// Changes the state of the Sprite. Should be called on each tick
        /// </summary>
        public virtual void ChangeState()
        {
            var ship = (MotherShipSprite)this.Sprite;
            if (this.shouldExtend)
            {
                ship.MoveOut();
                this.extendCount++;
            }
            else
            {
                ship.MoveIn();
                this.extendCount--;
            }

            if (this.extendCount == ExtendCap)
            {
                this.shouldExtend = false;
            }

            if (this.extendCount == 0)
            {
                this.shouldExtend= true;
            }
        }
    }
}
