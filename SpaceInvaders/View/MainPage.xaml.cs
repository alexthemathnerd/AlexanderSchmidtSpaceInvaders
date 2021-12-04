using System;
using Windows.Foundation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;
using SpaceInvaders.Model;

namespace SpaceInvaders.View
{
    /// <summary>
    ///     The main page for the game.
    /// </summary>
    public sealed partial class MainPage
    {
        #region Data members

        /// <summary>
        ///     The application height
        /// </summary>
        public const double ApplicationHeight = 720;

        /// <summary>
        ///     The application width
        /// </summary>
        public const double ApplicationWidth = 1080;
        private readonly DispatcherTimer timer;

        private int numberOfPlayers;
        private GameManager gameManager;
        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="MainPage" /> class.
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
            this.gameManager.LevelChangeEvent += this.onLevelChange;
            this.gameManager.HealthUpdateEvent += this.onHealthUpdate;
            this.scoreSummary.Text = "Score: 0";
            this.gameSummary.Text = "SPACE INVADERS!!!";

            Window.Current.CoreWindow.KeyDown += gameManager.OnKeyDown;

            this.timer = new DispatcherTimer();
            this.timer.Tick += gameManager.OnTick;
            this.timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            this.timer.Start();
        }

        #endregion

        #region Methods

        private void onLevelChange(object sender, LevelChangeEventArgs e)
        {
            var gameManager = (GameManager)sender;
            if (e.NewLevel > 3)
            {
                this.timer.Stop();
                this.gameSummary.Text = "Game Over. You Win!";
                
                this.Frame.Navigate(typeof(EndGamePage), new [] { gameManager.Score, gameManager.CurrentLevel - 1});
            }
            else
            {
                this.gameSummary.Text = $"Level: {e.NewLevel}";
                gameManager.InitializeGame(this.numberOfPlayers);
            }
        }

        private void onHealthUpdate(object sender, HealthUpdateArgs e)
        {
            this.healthSummary.Text = $"Health: {e.NewHealth}";
        }

        private void onGameOver(object sender, EventArgs e)
        {
            this.timer.Stop();

            var gameManager = (GameManager)sender;
            this.Frame.Navigate(typeof(EndGamePage), new [] {gameManager.Score, gameManager.CurrentLevel - 1});

        }

        private void onScoreUpdate(object sender, ScoreUpdateArgs e)
        {
            this.scoreSummary.Text = $"Score: {e.NewScore}";
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.numberOfPlayers = (int) (e.Parameter ?? throw new ArgumentException());
            this.gameManager.InitializeGame(this.numberOfPlayers);
            var health = this.numberOfPlayers == 2 ? 6 : 3;
            this.healthSummary.Text = $"Health: {health}";

        }

        #endregion
    }
}