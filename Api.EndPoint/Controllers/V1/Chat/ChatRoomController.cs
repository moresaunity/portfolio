using Aplication.Services.Products.ChatRoom.Commands.Create;
using Aplication.Services.Products.ChatRoom.Commands.Join;
using Aplication.Services.Products.ChatRoom.Commands.Leave;
using Aplication.Services.Products.ChatRoom.Commands.LeaveAllByConnectionId;
using Application.Services.ChatRoom.Commands.Create;
using Application.Services.ChatRoom.Commands.Join;
using Application.Services.ChatRoom.Commands.Leave;
using Application.Services.ChatRoom.Commands.LeaveAllByConnectionId;
using Application.Services.Products.ChatRoom.Queries.Get;
using Application.Services.Products.ChatRooms.Queries.GetByConnectionId;
using Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.EndPoint.Controllers.V1.Chat
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ChatRoomController : ControllerBase
    {
        private readonly IMediator mediator;

        public ChatRoomController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpGet]
        public IActionResult Get()
        {
            BaseDto<List<GetChatRoomDto>> result = mediator.Send(new GetChatRoomRequest()).Result;
            if (!result.IsSuccess) return BadRequest(result);
            return Ok(result);
        }
        [HttpGet("{ConnectionId}")]
        public IActionResult Get([FromRoute] string ConnectionId)
        {
            GetConnectionIdChatRoomRequest request = new GetConnectionIdChatRoomRequest(new GetConnectionIdChatRoomResponseDto(ConnectionId));
            BaseDto<Guid> result = mediator.Send(request).Result;
            return Ok(result);
        }
        [HttpPost]
        public IActionResult Post([FromHeader] string ConnectionId)
        {
            CreateChatRoomCommand command = new CreateChatRoomCommand(new CreateChatRoomRequestDto(ConnectionId));
            BaseDto<Guid> result = mediator.Send(command).Result;
            return Ok(result);
        }
        [HttpPut]
        public IActionResult Put([FromHeader] string ConnectionId, [FromHeader] Guid RoomId)
        {
            JoinChatRoomCommand command = new JoinChatRoomCommand(new JoinChatRoomRequestDto(ConnectionId, RoomId));
            BaseDto<Guid> result = mediator.Send(command).Result;
            return Ok(result);
        }
        [HttpDelete]
        public IActionResult Delete([FromHeader] string ConnectionId, [FromHeader] Guid RoomId)
        {
            LeaveChatRoomCommand command = new LeaveChatRoomCommand(new LeaveChatRoomRequestDto(ConnectionId, RoomId));
            BaseDto<Guid> result = mediator.Send(command).Result;
            return Ok(result);
        }
        [HttpDelete("{ConnectionId}")]
        public IActionResult Delete([FromRoute] string ConnectionId)
        {
            LeaveAllByConnectionIdChatRoomCommand command = new LeaveAllByConnectionIdChatRoomCommand(new LeaveAllByConnectionIdChatRoomRequestDto(ConnectionId));
            BaseDto result = mediator.Send(command).Result;
            return Ok(result);
        }
    }
}
