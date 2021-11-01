namespace SpaceInvaders.Model.Enemies
{

    /// <summary>
    /// Represent a base class for Enemy Ships
    /// </summary>
    /// <seealso cref="SpaceInvaders.Model.GameObject" />
    public abstract class EnemyShip : GameObject
    {

        private int stepsTraveled;

        /// <summary>
        /// Gets a value indicating whether the enemy should go left.
        /// </summary>
        /// <value>
        ///   <c>true</c> if enemy should go left otherwise, <c>false</c>.
        /// </value>
        public bool ShouldGoLeft { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnemyShip"/> class.
        /// </summary>
        /// <param name="speedX">The speed x.</param>
        /// <param name="speedY">The speed y.</param>
        protected EnemyShip(int speedX, int speedY)
        {
            this.stepsTraveled = 0;
            this.SetSpeed(speedX, speedY);
        }

        /// <summary>
        /// Moves the game object left.
        /// Precondition: None
        /// Postcondition: X == X@prev + SpeedX
        /// </summary>
        public override void MoveLeft()
        {
            base.MoveLeft();
            this.stepsTraveled -= 1;
            if (this.stepsTraveled <= -10)
            {
                this.ShouldGoLeft = false;
            }
            
        }

        /// <summary>
        /// Moves the game object right.
        /// Precondition: None
        /// Postcondition: X == X@prev + SpeedX
        /// </summary>
        public override void MoveRight()
        {
            base.MoveRight();
            this.stepsTraveled += 1;
            if (this.stepsTraveled >= 20)
            {
                this.ShouldGoLeft = true;
            }

        }
    }
}
