using Api.EndPoint.Models.Dtos;
using Aplication.Services.Products.ChatMessages.Commands.Create;
using Application.Services.ChatMessages.Commands.Create;
using Application.Services.Products.ChatMessages.Queries.Get;
using AutoMapper;
using Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.EndPoint.Controllers.V1.Chat
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ChatMessageController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public ChatMessageController(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }
        [HttpGet("{RoomId}")]
        public IActionResult Get([FromRoute] Guid RoomId)
        {
            BaseDto<List<GetChatMessageDto>> result = mediator.Send(new GetChatMessageRequest(RoomId)).Result;
            if(!result.IsSuccess) return BadRequest(result);
            return Ok(result);
        }
        [HttpPost("{RoomId}")]
        public IActionResult Post([FromRoute] Guid RoomId, [FromBody] ChatMessageDto chatMessageDto)
        {
            CreateChatMessageCommand command = new CreateChatMessageCommand(RoomId, mapper.Map<CreateChatMessageRequestDto>(chatMessageDto));
            BaseDto<CreateChatMessageRequestDto> result = mediator.Send(command).Result;
            if(!result.IsSuccess) return BadRequest(result);
            return Ok(result);
        }
    }
}
