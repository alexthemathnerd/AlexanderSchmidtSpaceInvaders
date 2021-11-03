using SpaceInvaders.View.Sprites;

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
