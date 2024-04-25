using Application.Interfaces.Contexts;
using Domain.Dtos;
using Domain.Products;
using MediatR;

namespace Aplication.Services.Products.ChatRoom.Commands.Create
{
    public class CreateCommentHandler : IRequestHandler<CreateChatRoomCommand, BaseDto<Guid>>
    {
        private readonly IDataBaseContext context;

        public CreateCommentHandler(IDataBaseContext context)
        {
            this.context = context;
        }
        public Task<BaseDto<Guid>> Handle(CreateChatRoomCommand request, CancellationToken cancellationToken)
        {
            //Domain.ChatRoom.ChatRoom? existChatRoom = context.ChatRooms.SingleOrDefault(p => p.ConnectionId.Contains(request.request.ConnectionId));
            //if (existChatRoom != null) return Task.FromResult(new BaseDto<Guid>(true, new List<string> { "Get ChatRoom Is Success" }, existChatRoom.Id));

            Domain.ChatRoom.ChatRoom ChatRoom = new Domain.ChatRoom.ChatRoom
            {
                Id = Guid.NewGuid(),
                ConnectionId = new List<string> { request.request.ConnectionId }
            };
            var entity = context.ChatRooms.Add(ChatRoom);
            context.SaveChanges();

            return Task.FromResult(new BaseDto<Guid>(true, new List<string> { "Add a New ChatRoom Is Success" }, entity.Entity.Id));
        }
    }

}
