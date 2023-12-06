using Core;
using API.Models;

namespace API.Repositories;

public interface IUserRepository
{
    IEnumerable<User> GetAll();
    void Add(User user);
    void Update(User userToUpdate);
    void Delete(User userToDelete);
    User GetById(int id);
    IEnumerable<User> GetAll(Pagination pagination);
}