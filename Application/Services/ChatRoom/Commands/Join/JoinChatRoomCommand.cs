using Application.Services.ChatRoom.Commands.Join;
using Domain.Dtos;
using MediatR;

namespace Aplication.Services.Products.ChatRoom.Commands.Join
{
    public class JoinChatRoomCommand : IRequest<BaseDto<Guid>>
    {
        public JoinChatRoomRequestDto request;

        public JoinChatRoomCommand(JoinChatRoomRequestDto request)
        {
            this.request = request;
        }

    }

}
