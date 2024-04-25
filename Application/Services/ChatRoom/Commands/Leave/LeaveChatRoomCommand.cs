using Application.Services.ChatRoom.Commands.Leave;
using Domain.Dtos;
using MediatR;

namespace Aplication.Services.Products.ChatRoom.Commands.Leave
{
    public class LeaveChatRoomCommand : IRequest<BaseDto<Guid>>
    {
        public LeaveChatRoomRequestDto request;

        public LeaveChatRoomCommand(LeaveChatRoomRequestDto request)
        {
            this.request = request;
        }

    }

}
