using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.Storage;
using Windows.UI.Xaml;
using SpaceInvaders.Model.UserComparer;

namespace SpaceInvaders.Model
{
    public class LeaderBoard
    {

        private const String LeaderBoardFileName = @"\leaderboard.xml";

        public IList<User> ReadTopPlayers()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<User>));
            FileStream fileStream = new FileStream(ApplicationData.Current.LocalFolder.Path + LeaderBoardFileName, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite);
            if (fileStream.Length == 0)
            {
                return new List<User>();
            }
            var topPlayers = (List<User>)serializer.Deserialize(fileStream);
            fileStream.Close();
            topPlayers.Sort(new ScoreNameLevelComparer());
            return topPlayers;
        }

        public void WriteTopPlayers(IList<User> user)
        {
            Debug.WriteLine($"Users were saved at {ApplicationData.Current.LocalFolder.Path}");
            XmlSerializer serializer = new XmlSerializer(typeof(List<User>));
            FileStream fileStream = new FileStream(ApplicationData.Current.LocalFolder.Path + LeaderBoardFileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
            serializer.Serialize(fileStream, user);
            fileStream.Close();
        }
    }
}
