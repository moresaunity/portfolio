using Api.EndPoint.Models.Dtos;
using Application.Services.ChatMessages.Commands.Create;
using AutoMapper;

namespace Api.EndPoint.MappingProfiles
{
    public class ChatMessageMappingProfile: Profile
    {
        public ChatMessageMappingProfile()
        {
            CreateMap<ChatMessageDto, CreateChatMessageRequestDto>().ReverseMap();
        }
    }
}
