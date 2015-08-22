using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_EntityFrameworkMappings
{
    class EntityFrameworkMappings
    {
        static void Main(string[] args)
        {
            var contetx = new FootballEntities();

            var teamNames = contetx.Teams
                .Select(t => t.TeamName)
                .ToList();

            foreach (var team in teamNames)
            {
                Console.WriteLine(team);
            }
        }
    }
}
