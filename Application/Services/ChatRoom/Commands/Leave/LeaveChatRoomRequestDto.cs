using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ChatRoom.Commands.Leave
{
    public class LeaveChatRoomRequestDto
    {
        public string ConnectionId { get; set; }
        public Guid RoomId { get; set; }
        public LeaveChatRoomRequestDto(string ConnectionId, Guid roomId)
        {
            this.ConnectionId = ConnectionId;
            RoomId = roomId;
        }
        public LeaveChatRoomRequestDto()
        {
            
        }
    }
}
