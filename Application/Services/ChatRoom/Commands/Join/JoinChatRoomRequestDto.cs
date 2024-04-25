using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ChatRoom.Commands.Join
{
    public class JoinChatRoomRequestDto
    {
        public string ConnectionId { get; set; }
        public Guid RoomId { get; set; }
        public JoinChatRoomRequestDto(string ConnectionId, Guid roomId)
        {
            this.ConnectionId = ConnectionId;
            RoomId = roomId;
        }
        public JoinChatRoomRequestDto()
        {
            
        }
    }
}
