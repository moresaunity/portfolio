using Application.Services.ChatMessages.Commands.Create;
using Domain.Dtos;
using MediatR;

namespace Aplication.Services.Products.ChatMessages.Commands.Create
{
    public class CreateChatMessageCommand : IRequest<BaseDto<CreateChatMessageRequestDto>>
    {
        public Guid RoomId { get; set; }
        public CreateChatMessageRequestDto request;

        public CreateChatMessageCommand(Guid RoomId ,CreateChatMessageRequestDto request)
        {
            this.RoomId = RoomId;
            this.request = request;
        }

    }

}
