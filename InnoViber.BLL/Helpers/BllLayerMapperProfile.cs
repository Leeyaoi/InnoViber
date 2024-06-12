using AutoMapper;
using InnoViber.BLL.Models;
using InnoViber.DAL.Models;
using System.Linq.Expressions;

namespace InnoViber.BLL.Helpers;

public class BllLayerMapperProfile : Profile
{
    public BllLayerMapperProfile()
    {
        CreateMap<UserEntity, UserModel>().ReverseMap();

        CreateMap<MessageEntity, MessageModel>().ReverseMap();

        CreateMap<ChatEntity, ChatModel>().ReverseMap();
    }
}
