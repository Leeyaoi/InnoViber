using AutoMapper;
using InnoViber.BLL.Interfaces;
using InnoViber.BLL.Models;
using InnoViber.DAL.Interfaces;
using InnoViber.DAL.Models;

namespace InnoViber.BLL.Services;

public class ChatService : GenericService<ChatModel, ChatEntity>, IChatService
{
    private readonly IMapper _mapper;
    private readonly IChatRepository _repository;
    private readonly IUserRepository _userRepository;

    public ChatService(IMapper mapper, IChatRepository repository, IUserRepository userRepository) : base(mapper, repository)
    {
        _mapper = mapper;
        _repository = repository;
        _userRepository = userRepository;
    }

    public async Task<ChatModel> UpdateUsersList(Guid id, Guid UserId, CancellationToken ct)
    {
        var entity = new ChatEntity() {Id = id};
        var user = await _userRepository.GetById(UserId, ct);
        if (user != null)
        {
            entity.Users.Add(user);
        }
        var result = await _repository.Update(entity, ct);
        return _mapper.Map<ChatModel>(result);
    }
}
