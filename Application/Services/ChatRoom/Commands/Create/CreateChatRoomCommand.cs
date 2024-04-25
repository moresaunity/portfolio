using Application.Services.ChatRoom.Commands.Create;
using Domain.Dtos;
using MediatR;

namespace Aplication.Services.Products.ChatRoom.Commands.Create
{
    public class CreateChatRoomCommand : IRequest<BaseDto<Guid>>
    {
        public CreateChatRoomRequestDto request;

        public CreateChatRoomCommand(CreateChatRoomRequestDto request)
        {
            this.request = request;
        }

    }

}
