using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;
using Windows.Storage;
using SpaceInvaders.Model.UserComparer;

namespace SpaceInvaders.Model
{

    /// <summary>
    /// A model of a leader board
    /// </summary>
    public class LeaderBoard
    {

        private const String LeaderBoardFileName = @"\leaderboard.xml";

        /// <summary>
        /// Reads the top players.
        /// </summary>
        /// <returns></returns>
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
            return topPlayers.GetRange(0, 10);
        }

        /// <summary>
        /// Writes the top players.
        /// </summary>
        /// <param name="users">The users.</param>
        public void WriteTopPlayers(IList<User> users)
        {
            Debug.WriteLine($"Users were saved at {ApplicationData.Current.LocalFolder.Path}");
            XmlSerializer serializer = new XmlSerializer(typeof(List<User>));
            FileStream fileStream = new FileStream(ApplicationData.Current.LocalFolder.Path + LeaderBoardFileName, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
            serializer.Serialize(fileStream, users);
            fileStream.Close();
        }
    }
}
