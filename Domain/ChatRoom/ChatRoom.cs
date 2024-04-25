using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.ChatRoom
{
    public class ChatRoom
    {
        public Guid Id { get; set; }
        public List<string> ConnectionId { get; set; }
        public ICollection<ChatMessage> chatMessages { get; set; }
    }
}
