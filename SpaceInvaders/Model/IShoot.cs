namespace SpaceInvaders.Model
{
    /// <summary>
    /// Handles defining something that can shot a bullet
    /// </summary>
    interface IShoot
    {
        /// <summary>
        /// Creates a Bullet
        /// </summary>
        /// <returns>the bullet created</returns>
       Bullet Shoot();

    }
}
