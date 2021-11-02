using System;
using System.Collections.Generic;
using System.Diagnostics;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using SpaceInvaders.Model.Enemies;

namespace SpaceInvaders.Model
{
    /// <summary>
    ///     Manages the entire game.
    /// </summary>
    public class GameManager
    {
        #region Data members

        private PlayerManager playerManager;
        private EnemyManager enemyManager;
        private readonly Canvas canvas;
        private int score;

        /// <summary>
        /// Occurs when [game over event].
        /// </summary>
        public event EventHandler GameOverEvent;

        /// <summary>
        /// Occurs when [game win event].
        /// </summary>
        public event EventHandler GameWinEvent;

        /// <summary>
        /// Occurs when [score update event].
        /// </summary>
        public event EventHandler<ScoreUpdateArgs> ScoreUpdateEvent;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GameManager" /> class.
        /// Precondition: backgroundHeight &gt; 0 AND backgroundWidth &gt; 0
        /// </summary>
        /// <param name="canvas">The canvas.</param>
        public GameManager(Canvas canvas)
        {
            this.canvas = canvas;
            this.score = 0;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Initializes the game placing player ship and enemy ship in the game.
        ///     Precondition: background != null
        ///     Postcondition: Game is initialized and ready for play.
        /// </summary>
        public void InitializeGame()
        {
            this.playerManager = new PlayerManager(this.canvas);
            this.playerManager.EnemyBulletCollideEvent += this.onPlayerCollision;
            this.enemyManager = new EnemyManager(this.canvas);
            this.enemyManager.PlayerBulletCollideEvent += this.onEnemyCollision;
        }

        /// <summary>
        /// Called when [tick].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        public void OnTick(object sender, object e)
        {
            this.playerManager.MoveBullet();
            this.enemyManager.MoveEnemies();
            this.enemyManager.ShootBullets();
            this.enemyManager.MoveBullets();
            this.checkCollisions();
        }

        private void checkCollisions()
        {

            foreach (var aEnemyBullet in new List<Bullet>(this.enemyManager.Bullets))
            {
                this.playerManager.CheckCollision(aEnemyBullet);
            }
            foreach (var aPlayerBullet in new List<Bullet> (this.playerManager.Bullets))
            {
                this.enemyManager.CheckCollision(aPlayerBullet);
            }

            if (!this.enemyManager.HasMoreEnemies)
            {
                this.GameWinEvent?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Called when [key down].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        public void OnKeyDown(CoreWindow sender, KeyEventArgs args)
        {
            switch (args.VirtualKey)
            {
                case VirtualKey.Left:
                    this.playerManager.MovePlayer(Direction.Left);
                    break;
                case VirtualKey.Right:
                    this.playerManager.MovePlayer(Direction.Right);
                    break;
                case VirtualKey.Space:
                    this.playerManager.ShotBullet();
                    break;
            }
        }

        private void onEnemyCollision(object sender, CollisionEventArgs e)
        {
            EnemyShip ship = (EnemyShip)sender;
            this.canvas.Children.Remove(ship.Sprite);
            this.canvas.Children.Remove(e.Bullet.Sprite);
            this.playerManager.Bullets.Remove(e.Bullet);
            this.playerManager.PlayerBullet = null;
            this.score += ship.Score;
            this.ScoreUpdateEvent?.Invoke(this, new ScoreUpdateArgs(this.score));
        }


        private void onPlayerCollision(object sender, CollisionEventArgs e)
        {
            PlayerShip ship = (PlayerShip)sender;
            this.canvas.Children.Remove(e.Bullet.Sprite);
            this.canvas.Children.Remove(ship.Sprite);
            this.enemyManager.Bullets.Remove(e.Bullet);
            this.GameOverEvent?.Invoke(this, EventArgs.Empty);
        }

        #endregion


    }
}
