using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceInvaders.Model.Enemies;

namespace SpaceInvaders.Model
{
    public class Level
    {
        public IList<Type> TypeByRow { get; set; }
        public IList<int> AmountByRow { get; set; }
        public IList<double> VarianceByRow { get; set; }

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
