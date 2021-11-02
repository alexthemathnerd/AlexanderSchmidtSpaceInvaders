﻿
// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace SpaceInvaders.View.Sprites
{
    /// <summary>
    ///     Draws a player ship.
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Controls.UserControl" />
    public sealed partial class PlanetShipSprite
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PlayerShipSprite" /> class.
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
    }
}