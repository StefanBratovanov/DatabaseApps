namespace EF_CodeFirst.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EF_CodeFirst.ChatSystemContext>
    {
        public Configuration()
        {
            AutomaticMigrationDataLossAllowed = true;
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(EF_CodeFirst.ChatSystemContext context)
        {
            seedUsers();
            seedChannels();

            seedChannelMessages();
        }

        private void seedChannelMessages()
        {
            var context = new ChatSystemContext();
            if (context.ChannelMessages.Any())
            {
                return;
            }

            var chanelMalinki = context.Channels.FirstOrDefault(c => c.Name == "Malinki");
            var nakov = context.Users.FirstOrDefault(c => c.Username == "Nakov");
            var vlado = context.Users.FirstOrDefault(c => c.Username == "VGeorgiev");
            var petya = context.Users.FirstOrDefault(c => c.Username == "Petya");

            var channelMsgs = new List<ChannelMessage>()
            {
                new ChannelMessage{ ChannelId = chanelMalinki.Id, Content = "Hey dudes, are you ready for tonight?", DateTime = DateTime.Now, UserId = petya.Id },
                new ChannelMessage { ChannelId = chanelMalinki.Id, Content = "Hey Petya, this is the SoftUni chat.", DateTime = DateTime.Now, UserId = vlado.Id },
                new ChannelMessage { ChannelId = chanelMalinki.Id, Content = "Hahaha, we are ready!", DateTime = DateTime.Now, UserId = nakov.Id },
                new ChannelMessage { ChannelId = chanelMalinki.Id, Content = "Oh my god. I mean for drinking some beer!", DateTime = DateTime.Now, UserId = petya.Id },
                new ChannelMessage { ChannelId = chanelMalinki.Id, Content = "We are sure!", DateTime = DateTime.Now, UserId = vlado.Id },
            };

            foreach (var chMsg in channelMsgs)
            {
                context.ChannelMessages.Add(chMsg);
            }

            context.SaveChanges();



        }

        private void seedChannels()
        {
            var context = new ChatSystemContext();
            if (context.Channels.Any())
            {
                return;
            }
            var channels = new List<Channel>()
            {
                new Channel { Name = "Malinki"},
                new Channel { Name = "SoftUni"},
                new Channel { Name = "Admins"},
                new Channel { Name = "Programmers"},
                new Channel { Name = "Geeks"}
            };

            foreach (var channel in channels)
            {
                context.Channels.Add(channel);
            }
            context.SaveChanges();
        }

        private void seedUsers()
        {
            var context = new ChatSystemContext();
            if (context.Users.Any())
            {
                return;
            }
            var users = new List<User>()
            {
                new User {Username = "VGeorgiev", FullName = "Vladimir Georgiev", PhoneNumber = "0894545454"},
                new User {Username = "Nakov", FullName = "Svetlin Nakov", PhoneNumber = "0897878787"},
                new User {Username = "Ache", FullName = "Angel Georgiev", PhoneNumber = "0897121212"},
                new User {Username = "Alex", FullName = "Alexandra Svilarova", PhoneNumber = "0894151417"},
                new User {Username = "Petya", FullName = "Petya Grozdarska", PhoneNumber = "0895464646"}
            };

            foreach (var user in users)
            {
                context.Users.Add(user);
            }
            context.SaveChanges();
        }
    }
}
