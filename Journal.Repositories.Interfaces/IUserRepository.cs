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
        void AddToken(User user);
        void Register(User user);
        void DeactivateUser(User user);
    }
}