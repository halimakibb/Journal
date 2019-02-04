using System.Collections.Generic;
using Journal.Entities;

namespace Journal.BusinessLogics.Interfaces
{
    public interface IUserBusinessLogic
    {
        IEnumerable<User> GetAll();
    }
}