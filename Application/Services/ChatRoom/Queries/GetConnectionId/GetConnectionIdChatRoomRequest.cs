using Application.Interfaces.Contexts;
using Domain.Dtos;
using MediatR;
using static Application.Services.Products.ChatRooms.Queries.GetByConnectionId.GetConnectionIdCommentOfCatalogItemQuery;

namespace Application.Services.Products.ChatRooms.Queries.GetByConnectionId
{
    public class GetConnectionIdChatRoomRequest : IRequest<BaseDto<Guid>>
    {
        public GetConnectionIdChatRoomResponseDto requestDto;

        public GetConnectionIdChatRoomRequest(GetConnectionIdChatRoomResponseDto requestDto)
        {
            this.requestDto = requestDto;
        }
    }
    public class GetConnectionIdCommentOfCatalogItemQuery : IRequestHandler<GetConnectionIdChatRoomRequest, BaseDto<Guid>>
    {
        private readonly IDataBaseContext context;

        public GetConnectionIdCommentOfCatalogItemQuery(IDataBaseContext context)
        {
            this.context = context;
        }
        public Task<BaseDto<Guid>> Handle(GetConnectionIdChatRoomRequest request, CancellationToken cancellationToken)
        {
            var ChatRoom = context.ChatRooms?.FirstOrDefault(p => p.ConnectionId.Contains(request.requestDto.ConnectionId));
            if (ChatRoom == null) return Task.FromResult(new BaseDto<Guid>(false, new List<string> { "Not Found ChatRoom!" }, new Guid()));
            return Task.FromResult(new BaseDto<Guid>(true, new List<string> { "Get By Id ChatRoom Is Success" }, ChatRoom.Id));
        }
    }
    public class GetConnectionIdChatRoomResponseDto
    {
        public string ConnectionId;

        public GetConnectionIdChatRoomResponseDto(string ConnectionId)
        {
            this.ConnectionId = ConnectionId;
        }
        public GetConnectionIdChatRoomResponseDto()
        {

        }
    }
}
