using AutoMapper;
using UserService.API.UserViewModels;
using UserService.BLL.Models;

namespace UserService.API.Helpers;

public class ApiMapperProfile : Profile
{
    public ApiMapperProfile()
    {
        CreateMap<UserModel, UserViewModel>().ReverseMap();
        CreateMap<UserModel, UserShortViewModel>().ReverseMap();
    }
}
