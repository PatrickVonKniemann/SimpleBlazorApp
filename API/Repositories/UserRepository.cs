using Core;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class UserRepository : IUserRepository
{
    private AppDbContext appDbContext { get; }

    public UserRepository(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    public IEnumerable<User> GetAll()
    {
        return appDbContext.Users.ToList();
    }
    
    
    public IEnumerable<User> GetAll(Pagination pagination)
    {
        var users = 
            appDbContext.Users
                .Skip(pagination.Page)
                .Take(pagination.ItemsPerPage)
                .Where(user => user.Name.Contains(pagination.FilterValue));
        return users;
    }

    public void Add(User user)
    {
        appDbContext.Users.Add(user);
        appDbContext.SaveChanges();
    }

    public void Update(User userToUpdate)
    {
        appDbContext.Users.Update(userToUpdate);
        appDbContext.SaveChanges();
    }

    public void Delete(User userToDelete)
    {
        appDbContext.Users.Remove(userToDelete);
        appDbContext.SaveChanges();
    }

    public User GetById(int id)
    {
        return appDbContext.Users.Find(id);
    }

}