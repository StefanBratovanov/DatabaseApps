using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_EntityFrameworkMappings;
using System.Web.Script.Serialization;
using System.IO;

namespace _02_ExportLeaguesAndTeamsJSON
{
    class ExportLeaguesAndTeamsJSON
    {
        static void Main(string[] args)
        {
            var context = new FootballEntities();

            var leaguesTeams = context.Leagues
                .OrderBy(l => l.LeagueName)
                .Select(l => new
                {
                    leagueName = l.LeagueName,
                    teams = l.Teams.OrderBy(t => t.TeamName).Select(t => t.TeamName)
                });

            //foreach (var league in leaguesTeams)
            //{
            //    Console.WriteLine(league.leagueName);
            //    foreach (var team in league.teams)
            //    {
            //        Console.WriteLine(team);
            //    }
            //}

            var JSerializer = new JavaScriptSerializer();
            var jsonleaguesTeams = JSerializer.Serialize(leaguesTeams);
            File.WriteAllText("../../leagues-and-teams.json", jsonleaguesTeams);
        }
    }
}
