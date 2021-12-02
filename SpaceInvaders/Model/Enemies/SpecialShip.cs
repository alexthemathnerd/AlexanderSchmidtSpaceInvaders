using SpaceInvaders.View.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace SpaceInvaders.Model.Enemies
{
    public class SpecialShip : EnemyShip, IShoot
    {
        private const double ChanceToShoot = 1;
        public const int SpecialShipStartingY = 2;
        public const int SpecialShipStartingX = -100;

        public double[] playerlocation;

        public event EventHandler LeavesScreenEvent;

        public SpecialShip() : base(2,0)
        {
            this.Sprite = new SpecialShipSprite();
            this.Score = 25;
        }

        public Bullet Shoot()
        {
            Random rand = new Random();
            int doShoot = rand.Next(100);
            if (doShoot < ChanceToShoot)
            {
                return new Bullet(this, playerlocation);
            }

            return null;
        }
        public static bool Spawn()
        {
            Random rand = new Random();
            if (rand.Next(0,100) > 90) 
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public override void Move()
        {
            this.MoveRight();

            if (this.X > (((Canvas)this.Sprite.Parent)).Width)
            {
                this.LeavesScreenEvent?.Invoke(this, new EventArgs());              
            }
        }
    }
}
