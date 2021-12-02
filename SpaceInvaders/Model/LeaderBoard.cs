using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SpaceInvaders.Model
{
    public class LeaderBoard
    {

        private const String LeaderBoardFileName = "leaderboard.xml";

        public IEnumerable<User> TopPlayers => this.readTopPlayers();

        private IEnumerable<User> readTopPlayers()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<User>));
            string executableLocation = Path.GetDirectoryName(
                Assembly.GetExecutingAssembly().Location);
            string leaderBoardFileLocation = Path.Combine(executableLocation, LeaderBoardFileName);
            FileStream fileStream = new FileStream(leaderBoardFileLocation, FileMode.OpenOrCreate);
            var topPlayers = (IEnumerable<User>)serializer.Deserialize(fileStream);
            fileStream.Close();
            return topPlayers;
        }

        public void WriteTopPlayer(User user)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(User));
            TextWriter writer = new StreamWriter(LeaderBoardFileName, true);
            serializer.Serialize(writer, user);
            writer.Close();
        }
    }
}
