using Core;
using Core.Models;

namespace API.Repositories;

public interface IUserRepository
{
    IEnumerable<User> GetAll();
    void Add(User user);
    void Update(User userToUpdate);
    void Delete(User userToDelete);
    User GetById(int id);
    User GetByName(string name);
    IEnumerable<User> GetAll(Pagination pagination);
}