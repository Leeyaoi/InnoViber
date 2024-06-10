using AutoMapper;
using InnoViber.BLL.Models;
using InnoViber.DAL.Models;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;

namespace InnoViber.BLL.Helpers;

public class Helper : Profile
{
    public Helper()
    {
        CreateMap<UserEntity, UserModel>().ReverseMap();

        CreateMap<MessageEntity, MessageModel>().ReverseMap();

        CreateMap<ChatEntity, ChatModel>().ReverseMap();
    }
}
