using SpaceInvaders.View.Sprites;

namespace SpaceInvaders.Model
{

    /// <summary>
    /// Represents a Bullet
    /// </summary>
    /// <seealso cref="SpaceInvaders.Model.GameObject" />
    public class Bullet : GameObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Bullet"/> class.
        /// </summary>
        /// <param name="xPos">The starting x position.</param>
        /// <param name="yPos">The starting y position.</param>
        public Bullet(double xPos, double yPos)
        {
            
            this.SetSpeed(0, 5);
            this.Sprite = new BulletSprite(); 
            this.X = xPos - this.Width / 2;
            this.Y = yPos + this.Height / 2;
        }

    }
}
