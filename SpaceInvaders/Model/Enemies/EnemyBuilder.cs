using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

namespace SpaceInvaders.Model.Enemies
{
    /// <summary>
    /// Used to Build a Collection of enemies
    /// </summary>
    public class EnemyBuilder
    {

        private const int EnemyGap = 10;
        private const int EnemyWidth = 64;

        /// <summary>
        /// Builds the enemy.
        /// </summary>
        /// <typeparam name="T">the type of the enemy</typeparam>
        /// <param name="xPos">The x position.</param>
        /// <param name="yPos">The y position.</param>
        /// <returns>the created ship</returns>
        public static EnemyShip BuildEnemy<T>(int xPos, int yPos) where T : EnemyShip, new()
        {
            return new T
            {
                X = xPos,
                Y = yPos
            };
        }

        /// <summary>
        /// Builds the row of enemies.
        /// </summary>
        /// <typeparam name="T">The Enemy to Populate the Row with</typeparam>
        /// <param name="canvas">The canvas to add the row to</param>
        /// <param name="amount">The amount of enemies on the row</param>
        /// <param name="row">The row to be added to</param>
        /// <returns>A list of enemies in the row</returns>
        public static IList<EnemyShip> BuildRow<T> (Canvas canvas, int amount, int row) where T : EnemyShip, new()
        {
            var yPos = EnemyGap * row + EnemyWidth * (row - 1);
            var prevX = (int)((canvas.Width - (EnemyWidth * amount + EnemyGap * (amount - 1))) / 2);
            var enemies = new List<EnemyShip>();
            for (var i = 0; i < amount; i++)
            {
                var enemy = BuildEnemy<T>(prevX, yPos);
                enemies.Add(enemy);
                canvas.Children.Add(enemy.Sprite);
                prevX += EnemyWidth + EnemyGap;
            }

            return enemies;
        }
    }
}
