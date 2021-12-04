using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Serialization;
using SpaceInvaders.Model;
using SpaceInvaders.Model.UserComparer;

namespace SpaceInvaders.ViewModel
{

    public class LeaderBoardViewModel : INotifyPropertyChanged
    {
        private readonly LeaderBoard leaderBoard;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<User> TopPlayers { get; set; }
        public ObservableCollection<UserSort> Sorts { get; set; }
        public ICommand SortCommand { get; private set; }

        public LeaderBoardViewModel()
        {
            this.leaderBoard = new LeaderBoard();
            this.TopPlayers = new ObservableCollection<User>(this.leaderBoard.ReadTopPlayers());
            this.Sorts = new ObservableCollection<UserSort> {
                new UserSort {
                    Name = "SCORE | NAME | LEVEL",
                    Sort = new ScoreNameLevelComparer()
                },
                new UserSort {
                    Name = "NAME | SCORE | LEVEL",
                    Sort = new NameScoreLevelComparer()
                },
                new UserSort {
                    Name = "LEVEL | SCORE | NAME",
                    Sort = new LevelScoreNameComparer()
                }
            };
        }

        public void Sort(UserSort sort)
        {
            var list = this.TopPlayers.ToList();

            list.Sort(sort.Sort ?? new ScoreNameLevelComparer());
            this.TopPlayers = new ObservableCollection<User>(list);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TopPlayers"));
        }

        public bool IsTopPlayer(int score)
        {
            var topPlayers = this.TopPlayers.ToList();
            return topPlayers.Any(player => score.CompareTo(player.Score) < 0) || topPlayers.Count() < 10;
        }

        public void AddTopPlayer(User user)
        {
            if (this.TopPlayers.Count() > 10)
            {
                this.TopPlayers.Remove(this.TopPlayers.Last());
            }
            this.TopPlayers.Add(user);
            this.leaderBoard.WriteTopPlayers(this.TopPlayers.ToList());
        }
    }
}
