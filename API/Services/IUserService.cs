using Core;
using API.Models;

namespace API.Services;

public interface IUserService
{
    IEnumerable<User> GetAll(Pagination? pagination);
    void Add(UserCreateDto user);
    void Update(int toUpdate, UserUpdateDto userToUpdate);
    void Delete(int id);
    User GetById(int id);
}