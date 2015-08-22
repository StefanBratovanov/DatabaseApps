using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_EntityFrameworkMappings;
using System.Xml;
using System.Xml.Linq;


namespace _03_ExportInternationalMatchesAsXML
{
    class ExportInternationalMatchesAsXML
    {
        static void Main(string[] args)
        {
            var context = new FootballEntities();

            var mateches = context.InternationalMatches
                .OrderBy(im => im.MatchDate)
                .ThenBy(im => im.CountryHome)
                .ThenBy(im => im.CountryAway)
                .Select(im => new
                {
                    homeCountry = im.CountryHome.CountryName,
                    awayCountry = im.CountryAway.CountryName,
                    homeCountryCode = im.CountryHome.CountryCode,
                    awayCountryCode = im.CountryAway.CountryCode,
                    league = im.League.LeagueName,
                    matchDate = im.MatchDate,
                    homeScore = im.HomeGoals,
                    awayScore = im.AwayGoals
                });

            XElement xmlMatches = new XElement("matches");

            foreach (var m in mateches)
            {
                XElement match = new XElement("match");

                var homeCountry = new XElement("home-country", m.homeCountry);
                homeCountry.Add(new XAttribute("code", m.homeCountryCode));
                var awayCountry = new XElement("away-country", m.awayCountry);
                awayCountry.Add(new XAttribute("code", m.awayCountryCode));

                match.Add(homeCountry);
                match.Add(awayCountry);

                if (m.league != null)
                {
                    var league = new XElement("league", m.league);
                    match.Add(league);
                }

                if (m.homeScore != null && m.awayScore != null)
                {
                    var score = new XElement("score", m.homeScore + "-" + m.awayScore);
                    match.Add(score);
                }

                if (m.matchDate != null)
                {
                    if (m.matchDate.Value.TimeOfDay == TimeSpan.Zero )
                    {
                        var date = new XAttribute("date", m.matchDate.Value.ToString("dd-MMM-yyyy"));
                        match.Add(date);
                    }
                    else
                    {
                        var datetime = new XAttribute("date-time", m.matchDate.Value.ToString("dd-MMM-yyyy hh:mm"));
                        match.Add(datetime);
                    }
                }

                xmlMatches.Add(match);
            }

            var xmlDoc = new XDocument(xmlMatches);
            xmlDoc.Save("../../international-matches.xml");
        }
    }
}
