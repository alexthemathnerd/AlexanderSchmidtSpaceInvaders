using System;
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
        /// <param name="type">the type of the enemy</param>
        /// <param name="xPos">The x position.</param>
        /// <param name="yPos">The y position.</param>
        /// <returns>the created ship</returns>
        public static EnemyShip BuildEnemy(Type type, int xPos, int yPos)
        {
            EnemyShip enemy = (EnemyShip)Activator.CreateInstance(type);
            enemy.X = xPos;
            enemy.Y = yPos;
            return enemy;
        }

        /// <summary>
        /// Builds the row of enemies.
        /// </summary>
        /// <param name="canvas">The canvas to add the row to</param>
        /// <param name="type">The type of enemies i</param>
        /// <param name="amount">The amount of enemies on the row</param>
        /// <param name="variance">The amount of enemies on the row</param>
        /// <param name="row">The row to be added to</param>
        /// <returns>A list of enemies in the row</returns>
        public static IList<EnemyShip> BuildRow(Canvas canvas, Type type, int amount, double variance, int row)
        {
            var yPos = EnemyGap * row + EnemyWidth * (row - 1);
            var prevX = (int)((canvas.Width - (EnemyWidth * amount + EnemyGap * (amount - 1))) / 2);
            var enemies = new List<EnemyShip>();
            for (var i = 0; i < amount; i++)
            {
                var enemy = BuildEnemy(type, prevX, yPos);
                enemy.MaxStep = (int)(enemy.MaxStep * variance);
                enemies.Add(enemy);
                canvas.Children.Add(enemy.Sprite);
                prevX += EnemyWidth + EnemyGap;
            }

            return enemies;
        }

        /// <summary>
        /// Builds the level.
        /// </summary>
        /// <param name="canvas">The canvas.</param>
        /// <param name="level">The level.</param>
        /// <returns></returns>
        public static IList<EnemyShip> BuildLevel(Canvas canvas, Level level)
        {
            var enemies = new List<EnemyShip>();
            for (int i = 0; i < level.TypeByRow.Count; i++)
            {
                enemies.AddRange(BuildRow(canvas, level.TypeByRow[i], level.AmountByRow[i], level.VarianceByRow[i] ,  i + 1));
            }

            return enemies;
        }
    }
}
