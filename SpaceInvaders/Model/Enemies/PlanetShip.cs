using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceInvaders.View.Sprites;

namespace SpaceInvaders.Model.Enemies
{
    /// <summary>
    /// Handles the Planet Ship
    /// </summary>
    /// <seealso cref="SpaceInvaders.Model.Enemies.MotherShip" />
    public class PlanetShip : MotherShip
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="PlanetShip"/> class.
        /// </summary>
        /// <param name="score">the score of the ship</param>
        public PlanetShip()
        {
            this.Score = 10;
            this.Sprite = new PlanetShipSprite();
        }

    }
}
