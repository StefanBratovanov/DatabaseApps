using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_EntityFrameworkMappings;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace _04_Import_LeaguesAndTeamsFromXML
{
    class ImportLeaguesAndTeamsFromXML
    {
        static void Main(string[] args)
        {
            var context = new FootballEntities();
            XDocument xmlDoc = XDocument.Load("../../leagues-and-teams.xml");

            int leagueNumber = 1;

            var leagueNodes = xmlDoc.XPathSelectElements("/leagues-and-teams/league");


            foreach (var xmlLeague in leagueNodes)
            {
                Console.WriteLine("Processing league #{0} ...", leagueNumber++);
                League leagueToAdd = null;

                if (xmlLeague.Element("league-name") != null)
                {
                    var leagueName = xmlLeague.Element("league-name").Value;

                    leagueToAdd = context.Leagues.FirstOrDefault(l => l.LeagueName == leagueName);
                    if (leagueToAdd != null)
                    {
                        Console.WriteLine("Existing league: {0}", leagueName);
                    }
                    else
                    {
                        leagueToAdd = new League() { LeagueName = leagueName };
                        context.Leagues.Add(leagueToAdd);

                        Console.WriteLine("Created league: {0}", leagueName);
                    }
                }

                var teamNodes = xmlLeague.XPathSelectElements("teams/team");
                foreach (var t in teamNodes)
                {
                    Team teamToAdd = null;
                    var teamName = t.Attribute("name").Value;
                    var countryAtt = t.Attribute("country");
                    string countryName = null;

                    if (countryAtt != null)
                    {
                        countryName = countryAtt.Value;
                    }

                    teamToAdd = context.Teams.FirstOrDefault(team => team.TeamName == teamName && team.Country.CountryName == countryName);
                    if (teamToAdd != null)
                    {
                        Console.WriteLine("Existing team: {0} ({1})", teamName, countryName);
                    }
                    else
                    {
                        var counrty = context.Countries.FirstOrDefault(c => c.CountryName == countryName);

                        teamToAdd = new Team()
                        {
                            TeamName = teamName,
                            Country = counrty
                        };

                        context.Teams.Add(teamToAdd);

                        Console.WriteLine("Created team: {0} ({1})", teamName, countryName ?? "no counrty");
                    }

                    //add teamToAdd in leagues

                    if (leagueToAdd != null)
                    {
                        leagueToAdd.Teams.Add(teamToAdd);
                    }

                }
            }
            context.SaveChanges();
        }
    }
}
