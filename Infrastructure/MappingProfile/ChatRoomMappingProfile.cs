using Application.Services.ChatMessages.Commands.Create;
using Application.Services.Products.ChatMessages.Queries.Get;
using Application.Services.Products.ChatRoom.Queries.Get;
using AutoMapper;
using Domain.ChatRoom;

namespace Infrastructure.MappingProfile
{
    public class ChatRoomMappingProfile: Profile
    {
        public ChatRoomMappingProfile() 
        {
            CreateMap<ChatRoom, GetChatRoomDto>().ReverseMap();

            CreateMap<ChatMessage, GetChatMessageDto>().ReverseMap();
            CreateMap<ChatMessage, CreateChatMessageRequestDto>().ReverseMap();
        }
    }
}
