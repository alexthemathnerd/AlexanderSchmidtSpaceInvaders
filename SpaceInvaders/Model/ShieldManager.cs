using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace SpaceInvaders.Model
{
    public class ShieldManager
    {
        private const int ShieldCount = 3;
        private const int ShieldGap = 150;
        private const int ShieldWidth = 64;

        private IList<Shield> shields;

        /// <summary>
        /// Occurs when [shield bullet collide event].
        /// </summary>
        public event EventHandler<CollisionEventArgs> ShieldBulletCollideEvent;

        public ShieldManager()
        {
            this.shields = new List<Shield>();
        }

        public void Initialize(Canvas canvas)
        {
            this.shields.Clear();
            var prevX = (int)((canvas.Width - (ShieldWidth * ShieldCount + ShieldGap * (ShieldCount - 1))) / 2);
            for (int i = 0; i < ShieldCount; i++)
            {
                Shield shield = new Shield();

                shield.X = prevX;
                shield.Y = canvas.Height * 3 / 4;
                this.shields.Add(shield);
                canvas.Children.Add(shield.Sprite);
                prevX += ShieldWidth + ShieldGap;
            }
        }

        /// <summary>
        /// Checks if the given bullet collides and raises a collision event.
        /// </summary>
        /// <param name="bullet">the bullet to check collision with enemies</param>
        public void CheckCollision(Bullet bullet)
        {
            Shield shieldToRemove = null;
            foreach (var aShield in this.shields)
            {
                var x1 = bullet.X + bullet.Width / 2;
                var y1 = bullet.Y + bullet.Height / 2;
                var x2 = aShield.X + aShield.Width / 2;
                var y2 = aShield.Y + aShield.Height / 2;
                var distance = DistanceCalculator.CalculateDistance(x1, y1, x2, y2);
                if (distance < (bullet.Width + aShield.Width) / 2)
                {
                    aShield.State--;
                    aShield.Sprite.Opacity = aShield.Sprite.Opacity - (1.0/3);
                    if (aShield.State == 0)
                    {
                        shieldToRemove = aShield;

                    }
                    this.ShieldBulletCollideEvent?.Invoke(aShield, new CollisionEventArgs(bullet));
                    break;
                }
            }

            if (shieldToRemove != null)
            {
                this.shields.Remove(shieldToRemove);
            }

        }

    }
}
