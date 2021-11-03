namespace SpaceInvaders.Model.Enemies
{

    /// <summary>
    /// An Interface to Represent a Object that can animate 
    /// </summary>
    interface IAnimate
    {

        /// <summary>
        /// Changes the state of the Sprite. Should be called on each tick
        /// </summary>
        void ChangeState();

    }
}
