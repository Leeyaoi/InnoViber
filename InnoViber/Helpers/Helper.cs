using AutoMapper;
using InnoViber.BLL.Models;

namespace InnoViber.API.Helpers;

public class Helper : Profile
{
    public Helper()
    {
        CreateMap<ChatViewModel, ChatModel>().ReverseMap();

        CreateMap<UserViewModel, UserModel>().ReverseMap();

        CreateMap<MessageViewModel, MessageModel>().ReverseMap();
    }
}
