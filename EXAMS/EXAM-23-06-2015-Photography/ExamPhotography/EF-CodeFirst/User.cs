using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_CodeFirst
{
    public class User
    {
        private ICollection<Channel> channels;
        private ICollection<UserMessage> receivedUserMessages;
        private ICollection<UserMessage> sentUserMessages;

        public User()
        {
            this.channels = new HashSet<Channel>();
            this.receivedUserMessages = new HashSet<UserMessage>();
            this.sentUserMessages = new HashSet<UserMessage>();
        }

        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public virtual ICollection<Channel> Channels
        {
            get { return this.channels; }

            set { this.channels = value; }
        }

        public virtual ICollection<UserMessage> ReceivedUserMessages
        {
            get { return this.receivedUserMessages; }

            set { this.receivedUserMessages = value; }
        }

        public virtual ICollection<UserMessage> SentUserMessages
        {
            get { return this.sentUserMessages; }

            set { this.sentUserMessages = value; }
        }

    }
}
