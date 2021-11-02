
// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

using Windows.UI;
using Windows.UI.Xaml.Media;

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
        ///     Initializes a new instance of the <see cref="AlienShipSprite" /> class.
        ///     Precondition: none
        ///     Postcondition: Sprite created.
        /// </summary>
        public AdvancedAlienShipSprite()
        {
            this.InitializeComponent();
        }

        #endregion

        public void MoveOut()
        {
            this.leftArm.Rotation += AngleChange;
            this.rightArm.Rotation -= AngleChange;
        }

        public void MoveIn()
        {
            this.leftArm.Rotation -= AngleChange;
            this.rightArm.Rotation += AngleChange;
        }

    }
}
