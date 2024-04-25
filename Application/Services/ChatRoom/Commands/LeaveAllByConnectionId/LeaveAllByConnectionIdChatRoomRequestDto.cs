using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ChatRoom.Commands.LeaveAllByConnectionId
{
    public class LeaveAllByConnectionIdChatRoomRequestDto
    {
        public string ConnectionId { get; set; }
        public LeaveAllByConnectionIdChatRoomRequestDto(string ConnectionId)
        {
            this.ConnectionId = ConnectionId;
        }
        public LeaveAllByConnectionIdChatRoomRequestDto()
        {
            
        }
    }
}
