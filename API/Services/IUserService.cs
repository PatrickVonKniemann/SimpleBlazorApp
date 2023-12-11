using Core;
using Core.Models;

namespace API.Services;

public interface IUserService
{
    IEnumerable<User> GetAll(Pagination? pagination);
    void Add(UserCreateDto user);
    void Update(int toUpdate, UserUpdateDto userToUpdate);
    void Delete(int id);
    User GetById(int id);
    User Authenticate(string username, string password);
}