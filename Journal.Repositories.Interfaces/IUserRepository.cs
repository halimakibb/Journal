using System.Collections.Generic;
using Journal.Entities;

namespace Journal.Repositories.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User GetById(long id);
        User GetByUsername(string username);
        User GetByEmail(string email);
        void Register(User user);
        void DeactivateUser(long id);
    }
}