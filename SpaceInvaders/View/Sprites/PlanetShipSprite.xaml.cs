namespace SpaceInvaders.View.Sprites
{
    /// <summary>
    ///     Draws a player ship.
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Controls.UserControl" />
    public sealed partial class PlanetShipSprite
    {

        private const int AngleChange = 2;

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PlanetShipSprite" /> class.
        ///     Precondition: none
        ///     Postcondition: Sprite created.
        /// </summary>
        public PlanetShipSprite()
        {
            this.InitializeComponent();
        }

        #endregion

        private void MotherShipSprite_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Moves arms out
        /// </summary>
        public void MoveOut()
        {
            this.leftArm.Rotation += AngleChange;
            this.rightArm.Rotation -= AngleChange;
            this.mother.MoveOut();
        }

        /// <summary>
        /// Moves arms in
        /// </summary>
        public void MoveIn()
        {
            this.leftArm.Rotation -= AngleChange;
            this.rightArm.Rotation += AngleChange;
            this.mother.MoveIn();
        }
    }
}