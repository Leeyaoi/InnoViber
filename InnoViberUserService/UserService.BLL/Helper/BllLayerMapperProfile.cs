using AutoMapper;
using UserService.BLL.Models;
using UserService.DAL.Entities;

namespace UserService.BLL.Helper;

public class BllLayerMapperProfile : Profile
{
    public BllLayerMapperProfile()
    {
        CreateMap<UserEntity, UserModel>().ReverseMap();
    }
}
