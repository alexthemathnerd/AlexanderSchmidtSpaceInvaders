using Windows.UI;
using SpaceInvaders.View.Sprites;

namespace SpaceInvaders.Model.Enemies
{

    /// <summary>
    /// Represents a Alien Ship
    /// </summary>
    /// <seealso cref="SpaceInvaders.Model.Enemies.EnemyShip" />
    public class AdvancedAlienShip : AlienShip, IAnimate
    {

        private const int TurnCap = 7;
        private int turnCount;
        private bool shouldGoOut;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AlienShip"/> class.
        /// </summary>
        public AdvancedAlienShip()
        {
            this.Score = 2;
            this.Sprite = new AdvancedAlienShipSprite();
            this.turnCount = 0;
            this.shouldGoOut = false;
        }

        #endregion


        public void ChangeState()
        {
            var ship = (AdvancedAlienShipSprite)this.Sprite;
            if (this.shouldGoOut)
            {

                ship.MoveOut();
                this.turnCount--;
            }
            else
            {
                ship.MoveIn();
                this.turnCount++;
            }

            if (this.turnCount == TurnCap)
            {
                this.shouldGoOut = true;
            }

            if (this.turnCount == 0)
            {
                this.shouldGoOut = false;
            }
        }
    }
}