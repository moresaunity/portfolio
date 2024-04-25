using Application.Interfaces.Contexts;
using Domain.Dtos;
using Domain.Products;
using MediatR;

namespace Aplication.Services.Products.ChatRoom.Commands.LeaveAllByConnectionId
{
    public class LeaveAllByConnectionIdCommentHandler : IRequestHandler<LeaveAllByConnectionIdChatRoomCommand, BaseDto>
    {
        private readonly IDataBaseContext context;

        public LeaveAllByConnectionIdCommentHandler(IDataBaseContext context)
        {
            this.context = context;
        }
        public Task<BaseDto> Handle(LeaveAllByConnectionIdChatRoomCommand request, CancellationToken cancellationToken)
        {
            Domain.ChatRoom.ChatRoom? existChatRoom = context.ChatRooms.FirstOrDefault(p => p.ConnectionId.Contains(request.request.ConnectionId));

            if (existChatRoom != null)
            {
                existChatRoom.ConnectionId.Remove(request.request.ConnectionId);
                context.SaveChanges();
            }

            return Task.FromResult(new BaseDto(true, new List<string> { "Leave All By ConnectionId ChatRoom Is Success" }));
        }
    }

}
