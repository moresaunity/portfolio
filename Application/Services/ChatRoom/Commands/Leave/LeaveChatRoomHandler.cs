using Application.Interfaces.Contexts;
using Domain.Dtos;
using Domain.Products;
using MediatR;

namespace Aplication.Services.Products.ChatRoom.Commands.Leave
{
    public class LeaveCommentHandler : IRequestHandler<LeaveChatRoomCommand, BaseDto<Guid>>
    {
        private readonly IDataBaseContext context;

        public LeaveCommentHandler(IDataBaseContext context)
        {
            this.context = context;
        }
        public Task<BaseDto<Guid>> Handle(LeaveChatRoomCommand request, CancellationToken cancellationToken)
        {
            Domain.ChatRoom.ChatRoom? existChatRoom = context.ChatRooms.SingleOrDefault(p => p.Id == request.request.RoomId);

            existChatRoom.ConnectionId.Remove(request.request.ConnectionId);

            var entity = context.ChatRooms.Update(existChatRoom);
            context.SaveChanges();

            return Task.FromResult(new BaseDto<Guid>(true, new List<string> { "Leave ChatRoom Is Success" }, entity.Entity.Id));
        }
    }

}
