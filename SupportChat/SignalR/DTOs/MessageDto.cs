using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalR.DTOs
{
    public class MessageDto
    {
        public int MessageId { get; set; }

        public string SenderId { get; set; }

        public string RecipientId { get; set; }

        public string Content { get; set; }

        public DateTime MessageSent { get; set; } = DateTime.Now;
    }
}
