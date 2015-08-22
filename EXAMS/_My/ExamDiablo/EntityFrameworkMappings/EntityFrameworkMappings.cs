using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace EntityFrameworkMappings
{
    class EntityFrameworkMappings
    {
        static void Main()
        {
            var context = new DiabloEntities();

            var charactersQ = context.Characters
                .Select(c => new
                {
                    c.Name
                });

            foreach (var character in charactersQ)
            {
                Console.WriteLine(character.Name);
            }
        }
    }
}
