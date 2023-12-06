using AutoMapper;

namespace Core.Models;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserReadDto>();
        CreateMap<UserReadDto, User>(); 
        
        CreateMap<User, UserCreateDto>();
        CreateMap<UserCreateDto, User>();
        
        CreateMap<User, UserUpdateDto>();
        CreateMap<UserUpdateDto, User>();
    }
}