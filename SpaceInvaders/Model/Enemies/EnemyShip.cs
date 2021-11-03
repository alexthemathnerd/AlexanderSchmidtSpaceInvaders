namespace SpaceInvaders.Model.Enemies
{

    /// <summary>
    /// Represent a base class for Enemy Ships
    /// </summary>
    /// <seealso cref="SpaceInvaders.Model.GameObject" />
    public abstract class EnemyShip : GameObject
    {
        private const int MaxStep = 10; 
        private int stepsTraveled;

        /// <summary>
        /// Gets a value indicating whether the enemy should go left.
        /// </summary>
        /// <value>
        ///   <c>true</c> if enemy should go left otherwise, <c>false</c>.
        /// </value>
        public bool ShouldGoLeft { get; private set; }

        /// <summary>
        /// Gets the score of the Enemy
        /// </summary>
        /// <value>
        /// The score.
        /// </value>
        public int Score { get; internal set; }

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
            this.stepsTraveled--;
            if (this.stepsTraveled <= -MaxStep)
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
            this.stepsTraveled++;
            if (this.stepsTraveled > MaxStep)
            {
                this.ShouldGoLeft = true;
            }

        }

        /// <summary>
        /// Moves this Enemy;
        /// </summary>
        public void Move()
        {
            if (this.ShouldGoLeft)
            {
                this.MoveLeft();
            }
            else
            {
                this.MoveRight();
            }
        }
    }
}
