using AutoMapper;
using InnoViber.BLL.Interfaces;
using InnoViber.BLL.Models;
using InnoViber.DAL.Interfaces;
using InnoViber.DAL.Models;

namespace InnoViber.BLL.Services;

public class UserService : GenericService<UserModel, UserEntity>, IUserService
{
    public UserService(IMapper mapper, IUserRepository userRepository) : base(mapper, userRepository)
    { }
}
