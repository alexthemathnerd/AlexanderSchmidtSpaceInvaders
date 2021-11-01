using System;
using Windows.Foundation;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using SpaceInvaders.Model;
using SpaceInvaders.Model.Enemies;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SpaceInvaders.View
{
    /// <summary>
    /// The main page for the game.
    /// </summary>
    public sealed partial class MainPage
    {
        /// <summary>
        ///     The application height
        /// </summary>
        public const double ApplicationHeight = 480;

        /// <summary>
        ///     The application width
        /// </summary>
        public const double ApplicationWidth = 640;

        private readonly GameManager gameManager;
        private readonly EnemyManager enemyManager;
        private readonly DispatcherTimer timer;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainPage"/> class.
        /// </summary>
        public MainPage()
        {
            this.InitializeComponent();

            ApplicationView.PreferredLaunchViewSize = new Size { Width = ApplicationWidth, Height = ApplicationHeight };
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(ApplicationWidth, ApplicationHeight));

            Window.Current.CoreWindow.KeyDown += this.coreWindowOnKeyDown;

            this.gameManager = new GameManager(ApplicationHeight, ApplicationWidth);
            this.gameManager.InitializeGame(this.theCanvas);
            this.enemyManager = new EnemyManager(ApplicationHeight, ApplicationWidth);
            this.enemyManager.InitializeEnemies(this.theCanvas);

            this.timer =  new DispatcherTimer();
            this.timer.Tick += this.update_Tick;
            this.timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            this.timer.Start();

        }

        private void coreWindowOnKeyDown(CoreWindow sender, KeyEventArgs args)
        {
            switch (args.VirtualKey)
            {
                case VirtualKey.Left:
                    this.gameManager.MovePlayerShipLeft();
                    break;
                case VirtualKey.Right:
                    this.gameManager.MovePlayerShipRight();
                    break;
                case VirtualKey.Space:
                    this.gameManager.ShootPlayerBullet(this.theCanvas);
                    break;
            }
        }

        private void update_Tick(object sender, object e)
        {
            this.gameManager.MovePlayerBullet(this.enemyManager, this.theCanvas);
            this.scoreSummary.Text = $"Score: {this.gameManager.Score}";
            this.enemyManager.MoveEnemies();
            this.enemyManager.MakeMotherShipsShoot(this.theCanvas);
            this.enemyManager.MoveMotherShipsShots(this.gameManager, this.theCanvas);
            if (!this.enemyManager.HasMoreEnemies)
            {
                this.timer.Stop();
                this.scoreSummary.Text = "Game Over. You Win!";
            } else if (this.gameManager.IsGameOver)
            {
                this.timer.Stop();
                this.scoreSummary.Text = $"Game Over. You Lose! Your Score Was: {this.gameManager.Score}";
            }
        }
    }
}
