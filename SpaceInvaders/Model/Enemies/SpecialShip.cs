using SpaceInvaders.View.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace SpaceInvaders.Model.Enemies
{
    public class SpecialShip : EnemyShip
    {

        public event EventHandler LeavesScreenEvent;

        public SpecialShip() : base(1,0)
        {
            this.Sprite = new SpecialShipSprite();
            this.Score = 25;
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
