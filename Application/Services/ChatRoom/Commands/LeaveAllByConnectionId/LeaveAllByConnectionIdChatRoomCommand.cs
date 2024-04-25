using Application.Services.ChatRoom.Commands.LeaveAllByConnectionId;
using Domain.Dtos;
using MediatR;

namespace Aplication.Services.Products.ChatRoom.Commands.LeaveAllByConnectionId
{
    public class LeaveAllByConnectionIdChatRoomCommand : IRequest<BaseDto>
    {
        public LeaveAllByConnectionIdChatRoomRequestDto request;

        public LeaveAllByConnectionIdChatRoomCommand(LeaveAllByConnectionIdChatRoomRequestDto request)
        {
            this.request = request;
        }

    }

}
