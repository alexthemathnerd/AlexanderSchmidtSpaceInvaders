using System;
using Windows.UI.Xaml.Controls;
using SpaceInvaders.View.Sprites;

namespace SpaceInvaders.Model.Enemies
{

    /// <summary>
    /// Represents the Mother Ship 
    /// </summary>
    /// <seealso cref="SpaceInvaders.Model.Enemies.EnemyShip" />
    class MotherShip : EnemyShip
    {

        private Bullet shipBullet;

        /// <summary>
        /// Initializes a new instance of the <see cref="MotherShip"/> class.
        /// </summary>
        public MotherShip() : base(5, 0)
        {
            this.Sprite = new MotherShipSprite();
            this.shipBullet = null;
        }

        /// <summary>
        /// Makes the MotherShip Shoot
        /// </summary>
        /// <param name="canvas">The canvas.</param>
        public void Shoot(Canvas canvas)
        {
            Random rand = new Random();
            int doShoot = rand.Next(0, 100);
            if (doShoot < 1 && this.shipBullet == null)
            {
                this.shipBullet = new Bullet(this.X + this.Width / 2.0, this.Y);
                canvas.Children.Add(this.shipBullet.Sprite);
            }
        }

        /// <summary>
        /// Moves the shots.
        /// </summary>
        /// <param name="gameManager">The game manager.</param>
        /// <param name="canvas">The canvas.</param>
        public void MoveShots(GameManager gameManager, Canvas canvas)
        {
            if (this.shipBullet != null)
            {
                this.shipBullet.MoveDown();
                if (gameManager.CheckAndResolveCollisions(this.shipBullet, canvas) || this.shipBullet.Y > canvas.Height)
                {
                    canvas.Children.Remove(this.shipBullet.Sprite);
                    this.shipBullet = null;
                }
            }
        }


        /// <summary>
        /// Destroys the bullet.
        /// </summary>
        /// <param name="canvas">The canvas.</param>
        public void DestroyBullet(Canvas canvas)
        {
            if (this.shipBullet != null)
            {
                canvas.Children.Remove(this.shipBullet.Sprite);
                this.shipBullet = null;
            }
        }
    }
}