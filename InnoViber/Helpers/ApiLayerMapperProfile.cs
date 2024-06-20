using AutoMapper;
using InnoViber.API.ViewModels.Chat;
using InnoViber.API.ViewModels.ChatRole;
using InnoViber.API.ViewModels.Message;
using InnoViber.API.ViewModels.User;
using InnoViber.BLL.Models;
using System.Linq.Expressions;

namespace InnoViber.API.Helpers;

public class ApiLayerMapperProfile : Profile
{
    public ApiLayerMapperProfile()
    {
        CreateMap<ChatViewModel, ChatModel>().ReverseMap();
        CreateMap<ChatViewModel, ChatShortViewModel>().ReverseMap();
        CreateMap<ChatModel, ChatShortViewModel>().ReverseMap();

        CreateMap<UserViewModel, UserModel>().ReverseMap();
        CreateMap<UserViewModel, UserShortViewModel>().ReverseMap();
        CreateMap<UserModel, UserShortViewModel>().ReverseMap();

        CreateMap<MessageViewModel, MessageModel>().ReverseMap();
        CreateMap<MessageViewModel, MessageShortViewModel>().ReverseMap();
        CreateMap<MessageViewModel, MessageChangeStatusViewModel>().ReverseMap();
        CreateMap<MessageModel, MessageChangeStatusViewModel>().ReverseMap();
        CreateMap<MessageModel, MessageShortViewModel>().ReverseMap();

        CreateMap<ChatRoleModel, ChatRoleViewModel>().ReverseMap();
        CreateMap<ChatRoleModel, ChatRoleShortViewModel>().ReverseMap();
        CreateMap<ChatRoleShortViewModel, ChatRoleViewModel>().ReverseMap();
    }
}
