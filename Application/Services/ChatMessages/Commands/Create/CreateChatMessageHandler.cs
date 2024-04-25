using Application.Interfaces.Contexts;
using Domain.Dtos;
using Domain.ChatRoom;
using MediatR;
using AutoMapper;
using Application.Services.ChatMessages.Commands.Create;

namespace Aplication.Services.Products.ChatMessages.Commands.Create
{
    public class CreateCommentHandler : IRequestHandler<CreateChatMessageCommand, BaseDto<CreateChatMessageRequestDto>>
    {
        private readonly IDataBaseContext context;
        private readonly IMapper mapper;
        public CreateCommentHandler(IDataBaseContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public Task<BaseDto<CreateChatMessageRequestDto>> Handle(CreateChatMessageCommand request, CancellationToken cancellationToken)
        {
            Domain.ChatRoom.ChatRoom? room = context.ChatRooms.FirstOrDefault(p => p.Id == request.RoomId);
            if (room == null) return Task.FromResult(new BaseDto<CreateChatMessageRequestDto>(true, new List<string> { "Chat Room Is Not Found" }, null));


            ChatMessage ChatMessage = new ChatMessage
            {
                Id = Guid.NewGuid(),
                ChatRoom = room,
                ChatRoomId = room.Id,
                Message = request.request.Message,
                Sender = request.request.Sender,
                Time = DateTime.Now
            };
            var entity = context.ChatMessages.Add(ChatMessage);
            context.SaveChanges();

            return Task.FromResult(new BaseDto<CreateChatMessageRequestDto>(true, new List<string> { "Add a New ChatMessage Is Success" }, mapper.Map<CreateChatMessageRequestDto>(ChatMessage)));
        }
    }

}
