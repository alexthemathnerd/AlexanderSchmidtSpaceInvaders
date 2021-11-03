namespace SpaceInvaders.View.Sprites
{
    /// <summary>
    ///     Draws a player ship.
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Controls.UserControl" />
    public sealed partial class AdvancedAlienShipSprite
    {

        private const int AngleChange = 2;

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="AdvancedAlienShipSprite" /> class.
        ///     Precondition: none
        ///     Postcondition: Sprite created.
        /// </summary>
        public AdvancedAlienShipSprite()
        {
            this.InitializeComponent();
        }

        #endregion

        /// <summary>
        /// Moves arms out
        /// </summary>
        public void MoveOut()
        {
            this.leftArm.Rotation += AngleChange;
            this.rightArm.Rotation -= AngleChange;
        }

        /// <summary>
        /// Moves the arms in
        /// </summary>
        public void MoveIn()
        {
            this.leftArm.Rotation -= AngleChange;
            this.rightArm.Rotation += AngleChange;
        }

    }
}
