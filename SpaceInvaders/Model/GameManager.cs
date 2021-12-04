using System;
using System.Collections.Generic;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;
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

        public int Score { get; private set; }
        public int CurrentLevel { get; private set; }


        /// <summary>
        /// Occurs when [game over event].
        /// </summary>
        public event EventHandler GameOverEvent;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="GameManager" /> class.
        ///     Precondition: backgroundHeight &gt; 0 AND backgroundWidth &gt; 0
        /// </summary>
        /// <param name="canvas">The canvas.</param>
        public GameManager(Canvas canvas)
        {
            this.canvas = canvas;
            this.Score = 0;
            this.CurrentLevel = 0;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Occurs when [game over event].
        /// </summary>
        public event EventHandler GameOverEvent;

        /// <summary>
        ///     Occurs when [score update event].
        /// </summary>
        public event EventHandler<ScoreUpdateArgs> ScoreUpdateEvent;

        /// <summary>
        ///     Occurs when [health update event].
        /// </summary>
        public event EventHandler<HealthUpdateArgs> HealthUpdateEvent;

        public event EventHandler<LevelChangeEventArgs> LevelChangeEvent;

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
            this.CurrentLevel = 1;
            this.enemyManager.InitializeLevel(this.CurrentLevel);
            this.enemyManager.PlayerBulletCollideEvent += this.onEnemyCollision;
        }

        /// <summary>
        ///     Called when [tick].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        public void OnTick(object sender, object e)
        {
            if (Window.Current.CoreWindow.GetAsyncKeyState(VirtualKey.Space) == CoreVirtualKeyStates.Down)
            {
                this.playerManager.ShootBullet();
            }

            this.playerManager.MoveBullets();
            this.enemyManager.MoveEnemies();
            this.enemyManager.AnimateEnemies();
            this.enemyManager.ShootBullets(this.playerManager.player);
            this.enemyManager.MoveBullets();
            this.checkCollisions();
        }

        private void checkCollisions()
        {
            foreach (var aEnemyBullet in new List<Bullet>(this.enemyManager.Bullets))
            {
                this.playerManager.CheckCollision(aEnemyBullet);
            }

            foreach (var aPlayerBullet in new List<Bullet>(this.playerManager.Bullets))
            {
                this.enemyManager.CheckCollision(aPlayerBullet);
            }

            if (!this.enemyManager.HasMoreEnemies)
            {
                this.CurrentLevel++;
                this.enemyManager.InitializeLevel(this.CurrentLevel);
                this.LevelChangeEvent?.Invoke(this, new LevelChangeEventArgs(this.CurrentLevel));
            }
        }

        /// <summary>
        ///     Called when [key down].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="KeyEventArgs" /> instance containing the event data.</param>
        public void OnKeyDown(CoreWindow sender, KeyEventArgs args)
        {
            if (args.VirtualKey == VirtualKey.Left ||
                sender.GetAsyncKeyState(VirtualKey.Left) == CoreVirtualKeyStates.Down)
            {
                this.playerManager.MovePlayer(Direction.Left);
            }

            if (args.VirtualKey == VirtualKey.Right ||
                sender.GetAsyncKeyState(VirtualKey.Right) == CoreVirtualKeyStates.Down)
            {
                this.playerManager.MovePlayer(Direction.Right);
            }
        }

        private void onEnemyCollision(object sender, CollisionEventArgs e)
        {
            if (sender is SpecialShip)
            {
                this.playerManager.PowerUp();
                this.enemyManager.hasSpecialShip = false;
                SoundManager.Stop(SoundEffectsEnum.SpecialShip);
            }

            var ship = (EnemyShip)sender;
            this.canvas.Children.Remove(ship.Sprite);
            this.canvas.Children.Remove(e.Bullet.Sprite);
            this.playerManager.Bullets.Remove(e.Bullet);
            this.Score += ship.Score;
            this.ScoreUpdateEvent?.Invoke(this, new ScoreUpdateArgs(this.Score));

            
        }

        private void onPlayerCollision(object sender, CollisionEventArgs e)
        {
            var ship = (PlayerShip)sender;
            this.canvas.Children.Remove(e.Bullet.Sprite);
            this.enemyManager.Bullets.Remove(e.Bullet);
            if (this.playerManager.PlayerHealth == 0)
            {
                this.canvas.Children.Remove(ship.Sprite);
                this.HealthUpdateEvent?.Invoke(this, new HealthUpdateArgs(this.playerManager.PlayerHealth));
                this.GameOverEvent?.Invoke(this, EventArgs.Empty);
                SoundManager.Play(SoundEffectsEnum.Lose);
            }
            else
            {
                this.HealthUpdateEvent?.Invoke(this, new HealthUpdateArgs(this.playerManager.PlayerHealth));
            }
        }

        #endregion
    }
}