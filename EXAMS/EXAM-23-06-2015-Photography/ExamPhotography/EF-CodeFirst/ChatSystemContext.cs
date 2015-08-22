namespace EF_CodeFirst
{
    using EF_CodeFirst.Migrations;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class ChatSystemContext : DbContext
    {
        
        public ChatSystemContext()
            : base("name=ChatSystemContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ChatSystemContext, Configuration>());
        }


        public IDbSet<User> Users { get; set; }
        public IDbSet<Channel> Channels { get; set; }
        public IDbSet<UserMessage> UserMessages { get; set; }
        public IDbSet<ChannelMessage> ChannelMessages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserMessage>()
                .HasRequired(um => um.Sender)
                .WithMany(m => m.SentUserMessages)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserMessage>()
                .HasRequired(um => um.Recipient)
                .WithMany(m => m.ReceivedUserMessages)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }

}