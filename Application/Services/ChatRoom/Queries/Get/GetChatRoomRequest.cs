using Application.Interfaces.Contexts;
using AutoMapper;
using Domain.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Products.ChatRoom.Queries.Get
{
    public class GetChatRoomRequest : IRequest<BaseDto<List<GetChatRoomDto>>>
    {

    }
    public class GetCommentOfChatRoomQuery : IRequestHandler<GetChatRoomRequest, BaseDto<List<GetChatRoomDto>>>
    {
        private readonly IDataBaseContext context;
        private readonly IMapper mapper;

        public GetCommentOfChatRoomQuery(IDataBaseContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public Task<BaseDto<List<GetChatRoomDto>>> Handle(GetChatRoomRequest request, CancellationToken cancellationToken)
        {
            List<Domain.ChatRoom.ChatRoom>? ChatRoom = context.ChatRooms?.Include(p => p.chatMessages).Where(p => p.chatMessages.Any()).ToList();
            if (ChatRoom.Count == 0) ChatRoom = context.ChatRooms?.Local.ToList();

            if (ChatRoom == null) return Task.FromResult(new BaseDto<List<GetChatRoomDto>>(false, new List<string> { "ChatRoom Is Empty" }, null));
            return Task.FromResult(new BaseDto<List<GetChatRoomDto>>(true, new List<string> { "Get ChatRoom Is Success" }, mapper.Map<List<GetChatRoomDto>>(ChatRoom)));
        }
    }

    public class GetChatRoomDto
    {
        public Guid Id { get; set; }
        public string ConnectionId { get; set; }
    }
}
