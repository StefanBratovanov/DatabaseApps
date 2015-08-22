using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF_CodeFirst;
using System.Web.Script.Serialization;
using System.IO;

namespace ImportUserMessagesFromJSON
{
    public class ImportUserMessagesFromJSON
    {
        static void Main()
        {
            var json = File.ReadAllText("../../messages.json");
            var JsSerializer = new JavaScriptSerializer();
            var parsedMsgs = JsSerializer.Deserialize<IEnumerable<MessageDTO>>(json);

            foreach (var message in parsedMsgs)
            {
                try
                {
                    ImportMessageToDB(message);
                    Console.WriteLine("Message \"{0}\" imported", message.Content);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error :{0}", ex.Message);
                }
            }
        }

        private static void ImportMessageToDB(MessageDTO message)
        {
            if (string.IsNullOrWhiteSpace(message.Content))
            {
                throw new ArithmeticException("Content is required");
            }

            if (string.IsNullOrWhiteSpace(message.Recipient))
            {
                throw new ArithmeticException("Recipient is required");
            }

            if (string.IsNullOrWhiteSpace(message.Sender))
            {
                throw new ArithmeticException("Sender is required");
            }

            var context = new ChatSystemContext();

            var recipientId = context.Users
                .Where(u => u.Username == message.Recipient)
                .Select(r => r.Id)
                .FirstOrDefault();

            var senderId = context.Users
                .Where(u => u.Username == message.Sender)
                .Select(s => s.Id)
                .FirstOrDefault();

            var userMessage = new UserMessage()
            {
                Content = message.Content,
                DateTime = message.DateTime,
                RecipientId = recipientId,
                SenderId = senderId
            };

            context.UserMessages.Add(userMessage);
            context.SaveChanges();

        }
    }
}
