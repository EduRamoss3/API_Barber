using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Barber.Message
{
    public class RabbitMQSendMessage : IRabbitMQSendMessage
    {
        private readonly string _hostName;
        private readonly string _password;
        private readonly string _userName;
        private IConnection _connection;

        public RabbitMQSendMessage(string hostname, string password, string username)
        {
            _hostName = "localhost";
            _password = "guest";
            _userName = "guest";
        }
        void IRabbitMQSendMessage.SendMessage(BaseMessage baseMessage, string queueName)
        {
            throw new NotImplementedException();
        }

    }
}
