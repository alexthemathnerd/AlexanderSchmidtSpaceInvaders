using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceInvaders.View.Sprites;

namespace SpaceInvaders.Model
{
    public class Shield : GameObject
    {

        public int State { get; set; }

        public Shield()
        {
            this.State = 3;
            this.Sprite = new ShieldSprite();
        }

    }
}
