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
        /// Gets the owner.
        /// </summary>
        /// <value>
        /// The owner.
        /// </value>
        public GameObject Owner { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Bullet"/> class.
        /// </summary>
        /// <param name="owner">the owner of the bullet</param>
        public Bullet(GameObject owner)
        {
            this.Owner = owner;
            this.SetSpeed(0, 5);
            this.Sprite = new BulletSprite();
            this.X = this.Owner.X + this.Owner.Width / 2.0 - this.Width / 2;
            this.Y = this.Owner.Y + this.Height / 2;
        }

    }
}
