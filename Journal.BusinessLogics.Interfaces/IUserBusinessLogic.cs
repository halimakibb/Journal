using System.Collections.Generic;
using Journal.Entities;

namespace Journal.BusinessLogics.Interfaces
{
    public interface IUserBusinessLogic
    {
        ReturnMessage GetAll();
        ReturnMessage GetById(long id);
        ReturnMessage GetByUsername(string username);
        ReturnMessage GetByEmail(string email);
        ReturnMessage Login (User user);
        ReturnMessage Register(User user);
        ReturnMessage DeactivateUser(User user);
    }
}