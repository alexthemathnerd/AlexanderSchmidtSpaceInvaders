using Windows.UI;
using SpaceInvaders.View.Sprites;

namespace SpaceInvaders.Model.Enemies
{

    /// <summary>
    /// Represents a Alien Ship
    /// </summary>
    /// <seealso cref="SpaceInvaders.Model.Enemies.EnemyShip" />
    public class AdvancedAlienShip : AlienShip
    {

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AlienShip"/> class.
        /// </summary>
        public AdvancedAlienShip()
        {
            this.Score = 2;
            this.Sprite = new AdvancedAlienShipSprite();
        }

        #endregion
    }
}