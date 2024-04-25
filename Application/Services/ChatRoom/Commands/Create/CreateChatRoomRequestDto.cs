using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ChatRoom.Commands.Create
{
    public class CreateChatRoomRequestDto
    {
        public string ConnectionId { get; set; }
        public CreateChatRoomRequestDto(string ConnectionId)
        {
            this.ConnectionId = ConnectionId;
        }
        public CreateChatRoomRequestDto()
        {
            
        }
    }
}
