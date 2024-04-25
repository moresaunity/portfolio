using Application.Interfaces.Contexts;
using Domain.Dtos;
using Domain.Products;
using MediatR;

namespace Aplication.Services.Products.ChatRoom.Commands.Join
{
    public class JoinCommentHandler : IRequestHandler<JoinChatRoomCommand, BaseDto<Guid>>
    {
        private readonly IDataBaseContext context;

        public JoinCommentHandler(IDataBaseContext context)
        {
            this.context = context;
        }
        public Task<BaseDto<Guid>> Handle(JoinChatRoomCommand request, CancellationToken cancellationToken)
        {
            Domain.ChatRoom.ChatRoom? existChatRoom = context.ChatRooms.SingleOrDefault(p => p.Id == request.request.RoomId);

            existChatRoom.ConnectionId.Add(request.request.ConnectionId);

            var entity = context.ChatRooms.Update(existChatRoom);
            context.SaveChanges();

            return Task.FromResult(new BaseDto<Guid>(true, new List<string> { "Join ChatRoom Is Success" }, entity.Entity.Id));
        }
    }

}
