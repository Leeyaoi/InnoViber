using AutoMapper;
using InnoViber.BLL.Models;

namespace InnoViber.API.Helpers;

public class ApiLayerMapperProfile : Profile
{
    public ApiLayerMapperProfile()
    {
        CreateMap<ChatViewModel, ChatModel>().ReverseMap();

        CreateMap<UserViewModel, UserModel>().ReverseMap();

        CreateMap<MessageViewModel, MessageModel>().ReverseMap();
    }
}
