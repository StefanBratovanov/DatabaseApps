using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace EF_CodeFirst
{
    public class PhonebookCodeFirst
    {
        static void Main()
        {
            var context = new ChatSystemContext();

            //            Console.WriteLine(context.Users.Count());

            var channels = context.Channels
                .Select(c => new
                {
                    ChannelName = c.Name,
                    ChannelMessages = c.ChannelMessages
                });

            foreach (var ch in channels)
            {
                Console.WriteLine(ch.ChannelName);
                Console.WriteLine("-- Messages: --");
                foreach (var chMessgae in ch.ChannelMessages)
                {
                    Console.WriteLine("Content: {0}, DateTime: {1}, User: {2}", chMessgae.Content, chMessgae.DateTime, chMessgae.User.Username);
                }
                Console.WriteLine();
            }
        }
    }
}
