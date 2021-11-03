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
        /// Builds the row of enemies.
        /// </summary>
        /// <typeparam name="T">The Enemy to Populate the Row with</typeparam>
        /// <param name="canvas">The canvas to add the row to</param>
        /// <param name="amount">The amount of enemies on the row</param>
        /// <param name="row">The row to be added to</param>
        /// <param name="enemies">a list to store the added enemies</param>
        public static void BuildRow<T> (Canvas canvas, int amount, int row, List<EnemyShip> enemies) where T : EnemyShip, new()
        {
            var yPos = EnemyGap * row + EnemyWidth * (row - 1);
            var prevX = (int)((canvas.Width - (EnemyWidth * amount + EnemyGap * (amount - 1))) / 2);
            for (var i = 0; i < amount; i++)
            {
                EnemyShip enemy = new T();
                enemies.Add(enemy);
                canvas.Children.Add(enemy.Sprite);
                enemy.X = prevX;
                enemy.Y = yPos;
                prevX += EnemyWidth + EnemyGap;
            }
        }
    }
}
