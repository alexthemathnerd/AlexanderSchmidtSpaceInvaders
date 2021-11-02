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

            this.gameManager = new GameManager(this.theCanvas);
            this.gameManager.ScoreUpdateEvent += this.onScoreUpdate;
            this.gameManager.GameOverEvent += this.onGameOver;
            this.gameManager.GameWinEvent += this.onGameWin;
            this.gameManager.InitializeGame();
            this.scoreSummary.Text = "Score: 0";

            Window.Current.CoreWindow.KeyDown += this.gameManager.OnKeyDown;
            this.timer =  new DispatcherTimer();
            this.timer.Tick += this.gameManager.OnTick;
            this.timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            this.timer.Start();

        }

        private void onGameWin(object sender, EventArgs e)
        {
            this.timer.Stop();
            this.scoreSummary.Text = "Game Over. You Win!";
        }

        private void onGameOver(object sender, EventArgs e)
        {
            this.timer.Stop();
            this.scoreSummary.Text = $"Game Over. You Lose!";
        }

        private void onScoreUpdate(object sender, ScoreUpdateArgs e)
        {
            this.scoreSummary.Text = $"Score: {e.NewScore}";
        }


    }
}
