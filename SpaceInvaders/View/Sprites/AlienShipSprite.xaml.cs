
// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

using Windows.UI;
using Windows.UI.Xaml.Media;

namespace SpaceInvaders.View.Sprites
{
    /// <summary>
    ///     Draws a player ship.
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Controls.UserControl" />
    public sealed partial class AlienShipSprite
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="AlienShipSprite" /> class.
        ///     Precondition: none
        ///     Postcondition: Sprite created.
        /// </summary>
        public AlienShipSprite(Color color)
        {
            SolidColorBrush brushColor = new SolidColorBrush(color);
            this.Resources.Add("EnemyShipColor", brushColor);
            this.InitializeComponent();
        }

        #endregion
        
    }
}
