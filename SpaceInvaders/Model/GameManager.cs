using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

        private readonly PlayerManager playerManager;
        private readonly EnemyManager enemyManager;
        private readonly ShieldManager shieldManager;
        private int numberOfPlayers;

        private readonly Canvas canvas;

        public event EventHandler GameOverEvent;

        /// <summary>
        /// Gets the score.
        /// </summary>
        /// <value>
        /// The score.
        /// </value>
        public int Score { get; private set; }

        /// <summary>
        /// Gets the current level.
        /// </summary>
        /// <value>
        /// The current level.
        /// </value>
        public int CurrentLevel { get; private set; }

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
            this.CurrentLevel = 1;
            this.playerManager = new PlayerManager(this.canvas);
            this.enemyManager = new EnemyManager(this.canvas);
            this.shieldManager = new ShieldManager();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Occurs when [score update event].
        /// </summary>
        public event EventHandler<ScoreUpdateArgs> ScoreUpdateEvent;

        /// <summary>
        ///     Occurs when [health update event].
        /// </summary>
        public event EventHandler<HealthUpdateArgs> HealthUpdateEvent;

        /// <summary>
        /// Occurs when [level change event].
        /// </summary>
        public event EventHandler<LevelChangeEventArgs> LevelChangeEvent;

        /// <summary>
        ///     Initializes the game placing player ship and enemy ship in the game.
        ///     Precondition: background != null
        ///     Postcondition: Game is initialized and ready for play.
        /// </summary>
        public void InitializeGame(int player)
        {
            this.numberOfPlayers = player;
            this.canvas.Children.Clear();
            this.playerManager.Initialize(numberOfPlayers);
            this.playerManager.EnemyBulletCollideEvent += this.onPlayerCollision;
            this.enemyManager.Initialize(this.CurrentLevel);
            this.enemyManager.PlayerBulletCollideEvent += this.onEnemyCollision;
            this.shieldManager.Initialize(this.canvas);
            this.shieldManager.ShieldBulletCollideEvent += this.onShieldCollision;
        }

        /// <summary>
        ///     Called when [tick].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        public void OnTick(object sender, object e)
        {
            if (Window.Current.CoreWindow.GetAsyncKeyState(VirtualKey.Up) == CoreVirtualKeyStates.Down)
            {
                this.playerManager.ShootPlayer1Bullet();
            }

            if (this.numberOfPlayers == 2 && Window.Current.CoreWindow.GetAsyncKeyState(VirtualKey.W) == CoreVirtualKeyStates.Down)
            {
                this.playerManager.ShootPlayer2Bullet();
            }

            this.playerManager.MoveBullets();
            this.enemyManager.MoveEnemies();
            this.enemyManager.AnimateEnemies();
            this.enemyManager.ShootBullets(this.playerManager.Player1, this.playerManager.Player2);
            this.enemyManager.MoveBullets();
            this.checkCollisions();
        }

        private void checkCollisions()
        {
            foreach (var aEnemyBullet in new List<Bullet>(this.enemyManager.Bullets))
            {
                this.playerManager.CheckCollision(aEnemyBullet);
                this.shieldManager.CheckCollision(aEnemyBullet);
            }

            foreach (var aPlayerBullet in new List<Bullet>(this.playerManager.BulletsPlayer1.Union(this.playerManager.BulletsPlayer2)))
            {
                this.enemyManager.CheckCollision(aPlayerBullet);
                this.shieldManager.CheckCollision(aPlayerBullet);
            }

            if (!this.enemyManager.HasMoreEnemies)
            {
                this.CurrentLevel++;
                Debug.WriteLine(this.CurrentLevel);
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
                this.playerManager.MovePlayer1(Direction.Left);
            }

            if (args.VirtualKey == VirtualKey.Right ||
                sender.GetAsyncKeyState(VirtualKey.Right) == CoreVirtualKeyStates.Down)
            {
                this.playerManager.MovePlayer1(Direction.Right);
            }

            if (this.numberOfPlayers == 2 && args.VirtualKey == VirtualKey.A ||
                sender.GetAsyncKeyState(VirtualKey.A) == CoreVirtualKeyStates.Down)
            {
                this.playerManager.MovePlayer2(Direction.Left);
            }

            if (this.numberOfPlayers == 2 && args.VirtualKey == VirtualKey.D ||
                sender.GetAsyncKeyState(VirtualKey.D) == CoreVirtualKeyStates.Down)
            {
                this.playerManager.MovePlayer2(Direction.Right);
            }
        }

        private void onEnemyCollision(object sender, CollisionEventArgs e)
        {
            if (sender is SpecialShip)
            {
                this.playerManager.PowerUp();
                this.enemyManager.HasSpecialShip = false;
                SoundManager.Stop(SoundEffectsEnum.SpecialShip);
            }

            var ship = (EnemyShip)sender;
            this.canvas.Children.Remove(ship.Sprite);
            this.canvas.Children.Remove(e.Bullet.Sprite);
            this.playerManager.BulletsPlayer1.Remove(e.Bullet);
            this.playerManager.BulletsPlayer2.Remove(e.Bullet);
            this.Score += ship.Score;
            this.ScoreUpdateEvent?.Invoke(this, new ScoreUpdateArgs(this.Score));

            
        }

        private void onShieldCollision(object sender, CollisionEventArgs e)
        {
            var shield = (Shield)sender;
            if (shield.State == 0)
            {
                this.canvas.Children.Remove(shield.Sprite);
            }
            this.canvas.Children.Remove(e.Bullet.Sprite);
            this.playerManager.BulletsPlayer1.Remove(e.Bullet);
            this.playerManager.BulletsPlayer2.Remove(e.Bullet);
            this.enemyManager.Bullets.Remove(e.Bullet);
        }

        private void onPlayerCollision(object sender, CollisionEventArgs e)
        {
            var ship = (PlayerShip)sender;
            this.canvas.Children.Remove(e.Bullet.Sprite);
            this.enemyManager.Bullets.Remove(e.Bullet);
            if (this.playerManager.TotalHealth == 0)
            {
                this.canvas.Children.Remove(ship.Sprite);
                this.GameOverEvent?.Invoke(this, EventArgs.Empty);
                SoundManager.Play(SoundEffectsEnum.Lose);
            }
            else
            {
                this.HealthUpdateEvent?.Invoke(this, new HealthUpdateArgs(this.playerManager.TotalHealth));
            }
        }

        #endregion
    }
}