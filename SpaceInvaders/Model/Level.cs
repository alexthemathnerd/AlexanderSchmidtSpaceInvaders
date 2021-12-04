using System;
using System.Collections.Generic;
using SpaceInvaders.Model.Enemies;

namespace SpaceInvaders.Model
{

    /// <summary>
    /// The model of a Level
    /// </summary>
    public class Level
    {
        /// <summary>
        /// Gets or sets the type by row.
        /// </summary>
        /// <value>
        /// The type by row.
        /// </value>
        public IList<Type> TypeByRow { get; set; }

        /// <summary>
        /// Gets or sets the amount by row.
        /// </summary>
        /// <value>
        /// The amount by row.
        /// </value>
        public IList<int> AmountByRow { get; set; }

        /// <summary>
        /// Gets or sets the variance by row.
        /// </summary>
        /// <value>
        /// The variance by row.
        /// </value>
        public IList<double> VarianceByRow { get; set; }

        /// <summary>
        /// Gets the level.
        /// </summary>
        /// <param name="number">The level number.</param>
        /// <returns>a level</returns>
        public static Level GetLevel(int number)
        {
            if (number == 1)
            {
                return new Level {
                    TypeByRow = new List<Type>() { typeof(AdvancedAlienShip), typeof(AlienShip), typeof(AlienShip) },
                    AmountByRow = new List<int>() { 5, 5, 3},
                    VarianceByRow = new List<double>() { 1, 1, 1}
                };
            } else if (number == 2)
            {
                return new Level
                {
                    TypeByRow = new List<Type>() { typeof(MotherShip), typeof(AdvancedAlienShip), typeof(AlienShip) },
                    AmountByRow = new List<int>() { 2, 5, 5 },
                    VarianceByRow = new List<double>() { 1, 2, 3 }
                };
            }
            else
            {
                return new Level
                {
                    TypeByRow = new List<Type>() { typeof(PlanetShip), typeof(MotherShip), typeof(AdvancedAlienShip), typeof(AlienShip) },
                    AmountByRow = new List<int>() { 3, 4, 5, 5 },
                    VarianceByRow = new List<double>() { 1, 2, 3, 4 }
                };
            }
        }
    }
}
