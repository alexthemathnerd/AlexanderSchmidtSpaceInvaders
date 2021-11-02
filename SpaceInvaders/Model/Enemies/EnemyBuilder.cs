using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.Web.Http.Headers;

namespace SpaceInvaders.Model.Enemies
{
    public class EnemyBuilder
    {

        private const int EnemyGap = 10;
        private const int EnemyWidth = 64;

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
