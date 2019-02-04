using System.Collections.Generic;
using Journal.BusinessLogics.Interfaces;
using Journal.Entities;
using Journal.Repositories.Interfaces;

namespace Journal.BusinessLogics
{
    public class UserBusinessLogic : IUserBusinessLogic
    {
        private readonly IUserRepository _userRepo;
        public UserBusinessLogic (IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }
        public IEnumerable<User> GetAll()
        {
            return _userRepo.GetAll();
        }
    }
}