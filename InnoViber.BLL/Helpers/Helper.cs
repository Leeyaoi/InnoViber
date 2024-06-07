using AutoMapper;
using InnoViber.BLL.Models;
using InnoViber.DAL.Models;

namespace InnoViber.BLL.Helpers;

public class Helper : Profile
{
    public Helper()
    {
        CreateMap<UserEntity, UserDTO>().ReverseMap();

        CreateMap<MessageEntity, MessageDTO>().ReverseMap();

        CreateMap<ChatEntity, ChatDTO>().ReverseMap();
    }
}
