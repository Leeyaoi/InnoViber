using AutoMapper;
using InnoViber.BLL.Models;
using InnoViber.BLL.Services;

namespace InnoViber.Controllers;

public class UserController
{
    private readonly UserService _service;
    private readonly IMapper _mapper;

    public UserController(UserService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    public Task Create(UserViewModel user, CancellationToken ct)
    {
        var model = _mapper.Map<UserModel>(user);
        return _service.Create(model, ct);
    }

    public Task Delete(UserViewModel user, CancellationToken ct)
    {
        var model = _mapper.Map<UserModel>(user);
        return _service.Delete(model, ct);
    }

    public Task Update(UserViewModel user, CancellationToken ct)
    {
        var model = _mapper.Map<UserModel>(user);
        return _service.Update(model, ct);
    }

    public async Task<List<UserViewModel>> GetAll(CancellationToken ct)
    {
        var models = await _service.GetAll(ct);
        return _mapper.Map<List<UserViewModel>>(models);
    }

    public async Task<UserViewModel?> GetById(Guid id, CancellationToken ct)
    {
        var model = await _service.GetById(id, ct);
        return _mapper?.Map<UserViewModel>(model);
    }
}
