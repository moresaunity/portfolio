using Application.Interfaces.Contexts;
using AutoMapper;
using Domain.ChatRoom;
using Domain.Dtos;
using MediatR;

namespace Application.Services.Products.ChatMessages.Queries.Get
{
    public class GetChatMessageRequest : IRequest<BaseDto<List<GetChatMessageDto>>>
    {
        public Guid RoomId { get; private set; }
        public GetChatMessageRequest(Guid RoomId)
        {
            this.RoomId = RoomId;
        }

    }
    public class GetCommentOfChatMessageQuery : IRequestHandler<GetChatMessageRequest, BaseDto<List<GetChatMessageDto>>>
    {
        private readonly IDataBaseContext context;
        private readonly IMapper mapper;

        public GetCommentOfChatMessageQuery(IDataBaseContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public Task<BaseDto<List<GetChatMessageDto>>> Handle(GetChatMessageRequest request, CancellationToken cancellationToken)
        {
            List<ChatMessage>? ChatMessage = context.ChatMessages?.Where(p => p.ChatRoomId == request.RoomId).OrderBy(p => p.Time).ToList();
            if (ChatMessage == null) ChatMessage = context.ChatMessages?.Local.Where(p => p.ChatRoomId == request.RoomId).OrderBy(p => p.Time).ToList();

            if (ChatMessage == null) return Task.FromResult(new BaseDto<List<GetChatMessageDto>>(false, new List<string> { "ChatMessage Is Not Found" }, null));
            return Task.FromResult(new BaseDto<List<GetChatMessageDto>>(true, new List<string> { "Get ChatMessage Is Success" }, mapper.Map<List<GetChatMessageDto>>(ChatMessage)));
        }
    }

    public class GetChatMessageDto
    {
        public string Sender { get; set; }
        public string Message { get; set; }
        public DateTime Time { get; set; }
    }
}
