using Windows.UI;
using SpaceInvaders.View.Sprites;

namespace SpaceInvaders.Model.Enemies
{

    /// <summary>
    /// Represents a Alien Ship
    /// </summary>
    /// <seealso cref="SpaceInvaders.Model.Enemies.EnemyShip" />
    public class AlienShip : EnemyShip
    {
        #region Properties

        /// <summary>
        /// Gets a value indicating whether this instance is advance.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is advance; otherwise, <c>false</c>.
        /// </value>
        public bool IsAdvance { get; internal set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AlienShip"/> class.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="isAdvance">if set to <c>true</c> [is advance].</param>
        public AlienShip(Color color, bool isAdvance) : base(4, 0)
        {
            Sprite = new AlienShipSprite(color);
            this.IsAdvance = isAdvance;
        }

        #endregion
    }
}