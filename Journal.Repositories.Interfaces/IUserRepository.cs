using System.Collections.Generic;
using Journal.Entities;

namespace Journal.Repositories.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
    }
}