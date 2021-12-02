using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using SpaceInvaders.Model;

namespace SpaceInvaders.ViewModel
{

    public class LeaderBoardViewModel
    {
        private LeaderBoard leaderBoard;

        public ObservableCollection<User> TopPlayers { get; set; }

        public LeaderBoardViewModel()
        {
            this.leaderBoard = new LeaderBoard();
            this.TopPlayers = new ObservableCollection<User>(this.leaderBoard.TopPlayers);
        }

        public bool AddTopPlayer(User user)
        {
            if (this.leaderBoard.TopPlayers.Any(player => user.CompareTo(player) < 0))
            {
                this.leaderBoard.WriteTopPlayer(user);
            }

            return false;
        }

    }
}
