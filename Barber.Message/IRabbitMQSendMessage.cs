using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barber.Message
{
    public interface IRabbitMQSendMessage
    {
        void SendMessage(BaseMessage baseMessage, string queueName);
    }
}
