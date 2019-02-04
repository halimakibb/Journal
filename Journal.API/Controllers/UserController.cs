using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Journal.BusinessLogics.Interfaces;
using Journal.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Journal.API.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserBusinessLogic _userBL;
        
        public UserController(IUserBusinessLogic userBL)
        {
            _userBL = userBL;
        }

        [HttpGet]
        public IEnumerable<User> GetAll()
        {
            return _userBL.GetAll();
        }

    }
}
