
// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

using System.Numerics;

namespace SpaceInvaders.View.Sprites
{
    /// <summary>
    ///     Draws a player ship.
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Controls.UserControl" />
    public sealed partial class MotherShipSprite
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="MotherShipSprite" /> class.
        ///     Precondition: none
        ///     Postcondition: Sprite created.
        /// </summary>
        public MotherShipSprite()
        {
            this.InitializeComponent();
        }

        #endregion

        /// <summary>
        /// Moves Barrels in
        /// </summary>
        public void MoveIn()
        {
            this.leftBarrel.Translation -= new Vector3(0, 1, 0);
            this.rightBarrel.Translation -= new Vector3(0, 1, 0);
        }

        /// <summary>
        /// Moves Barrels out
        /// </summary>
        public void MoveOut()
        {
            this.leftBarrel.Translation += new Vector3(0, 1, 0);
            this.rightBarrel.Translation += new Vector3(0, 1, 0);
        }
    }

}
