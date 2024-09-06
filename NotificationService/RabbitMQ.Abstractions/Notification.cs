using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Abstractions
{
    public class Notification
    {
        public string UserId { get; set; }
        public string OpenQuestionnaireUrl { get; set; }
        public string MessageText { get; set; }
        public string Email { get; set; }
    }
}
