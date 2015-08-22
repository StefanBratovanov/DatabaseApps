using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFrameworkMappings;
using System.Web.Script.Serialization;
using System.IO;

namespace ExportCharactersAndPlayersAsJson
{
    class ExportCharactersAndPlayersAsJson
    {
        static void Main()
        {
            var context = new DiabloEntities();

            var charactersAndPlayers = context.Characters
                .OrderBy(c => c.Name)
                .Select(c => new
                {
                    name = c.Name,
                    playedBy = c.UsersGames.Select(ug => ug.User.Username)
                });


            var JSerializer = new JavaScriptSerializer();
            var charactersAndUsers = JSerializer.Serialize(charactersAndPlayers);
            File.WriteAllText("../../characters.json", charactersAndUsers);
            Console.WriteLine("File characters.json exported.");

            
        }
    }
}
