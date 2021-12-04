using SpaceInvaders.View.Sprites;

namespace SpaceInvaders.Model
{

    /// <summary>
    /// The model for a shield
    /// </summary>
    /// <seealso cref="SpaceInvaders.Model.GameObject" />
    public class Shield : GameObject
    {

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        public int State { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Shield"/> class.
        /// </summary>
        public Shield()
        {
            this.State = 3;
            this.Sprite = new ShieldSprite();
        }

    }
}
