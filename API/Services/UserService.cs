using AutoMapper;
using Core;
using API.Models;
using API.Repositories;

namespace API.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper; // AutoMapper mapper

    public UserService(IMapper mapper,IUserRepository userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public IEnumerable<User> GetAll(Pagination? pagination)
    {
        if (pagination != null)
        {
            return _userRepository.GetAll(pagination);
        }
        
        return _userRepository.GetAll();
    }

    public void Add(UserCreateDto user)
    {
        var userToCreate = _mapper.Map<User>(user);
        _userRepository.Add(userToCreate);
    }

    public void Update(int userId, UserUpdateDto userToUpdate)
    {
        var userToCheck = _userRepository.GetById(userId);
        if (userToCheck == null)
        {
            throw new Exception("User not found");
        }
        
        // Use AutoMapper to map the DTO to the existing entity
        _mapper.Map(userToUpdate, userToCheck);

        // Now the userToCheck contains the updated fields from userToUpdate
        _userRepository.Update(userToCheck);
        
    }

    public void Delete(int id)
    {
        var userToDelete = _userRepository.GetById(id);
        if (userToDelete == null)
        {
            throw new Exception("User not found");
        }
        
        _userRepository.Delete(userToDelete);
    }

    public User GetById(int id)
    {
        return _userRepository.GetById(id);
    }
}