using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Constants
{
    public class RabbitMQConstants
    {
        public const string Uri = "rabbitmq://localhost";
        public const string Username = "guest";
        public const string Password = "guest";

        public const string CompleteProcessQuene = "complete-process-info"; //use for event (publish)
        public const string UpdateProcessQuene = "update-process-info"; //use for command (send)
    }
}
