using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using EntityFrameworkMappings;

namespace ExportFinishedGamesAsXML
{
    class ExportFinishedGamesAsXML
    {
        static void Main()
        {
            var context = new DiabloEntities();

            var finishedGames = context.Games
                .Where(g => g.IsFinished == true)
                .OrderBy(g => g.Name)
                .ThenBy(g => g.Duration)
                .Select(g => new
                {
                    GameName = g.Name,
                    GameDuration = g.Duration,
                    Users = new
                    {
                        user = g.UsersGames.Select(ug => ug.User)
                    }
                });

            var resultXml = new XElement("games");

            foreach (var game in finishedGames)
            {
                var gameXml = new XElement("game");
                gameXml.Add(new XAttribute("name", game.GameName));
                if (game.GameDuration.HasValue)
                {
                    gameXml.Add(new XAttribute("duration", game.GameDuration));
                }

                var users = new XElement("users");

                foreach (var user in game.Users.user)
                {
                    users.Add(new XElement("user",
                        new XAttribute("username", user.Username),
                        new XAttribute("ip-address", user.IpAddress)));
                }

                gameXml.Add(users);

                resultXml.Add(gameXml);

            }
            var resultXmlDoc = new XDocument();
            resultXmlDoc.Add(resultXml);
            resultXmlDoc.Save("../../finished-games.xml");

            Console.WriteLine("Finished Games exported to finished-games.xml");
        }
    }
}
