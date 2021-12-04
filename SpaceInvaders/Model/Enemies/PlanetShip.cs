using SpaceInvaders.View.Sprites;
using System;

namespace SpaceInvaders.Model.Enemies
{
    /// <summary>
    /// Handles the Planet Ship
    /// </summary>
    /// <seealso cref="SpaceInvaders.Model.Enemies.MotherShip" />
    public class PlanetShip : MotherShip
    {
        private const int TurnCap = 5;
        private int turnCount;
        private bool shouldGoOut;

        /// <summary>
        /// Gets and sets the player location.
        /// </summary>
        /// <value>
        /// The player location.
        /// </value>
        public double[] PlayerLocation { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="PlanetShip"/> class.
        /// </summary>
        public PlanetShip()
        {
            this.Score = 10;
            this.Sprite = new PlanetShipSprite();
            this.turnCount = 0;
            this.shouldGoOut = true;
        }


        /// <summary>
        /// Makes the PlanetShip Shoot
        /// </summary>
        public override Bullet Shoot()
        {
            Random rand = new Random();
            int doShoot = rand.Next(100);
            if (doShoot < ChanceToShoot)
            {
                return new Bullet(this, this.PlayerLocation);
            }

            return null;
        }

        /// <summary>
        /// Changes the state of the Sprite. Should be called on each tick
        /// </summary>
        public override void ChangeState()
        {
            var ship = (PlanetShipSprite)this.Sprite;
            if (this.shouldGoOut)
            {
               
                ship.MoveOut();
                this.turnCount++;
            }
            else
            {
                ship.MoveIn();
                this.turnCount--;
            }

            if (this.turnCount == TurnCap)
            {
                this.shouldGoOut = false;
            }

            if (this.turnCount == 0)
            {
                this.shouldGoOut = true;
            }
        }

    }
}
