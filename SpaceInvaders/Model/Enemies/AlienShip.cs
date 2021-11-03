using SpaceInvaders.View.Sprites;

namespace SpaceInvaders.Model.Enemies
{

    /// <summary>
    /// Represents an Alien Ship
    /// </summary>
    /// <seealso cref="SpaceInvaders.Model.Enemies.EnemyShip" />
    public class AlienShip : EnemyShip
    {

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AlienShip"/> class.
        /// </summary>
        public AlienShip() : base(4, 0)
        {
            this.Score = 1;
            this.Sprite = new AlienShipSprite();
        }

        #endregion
    }
}