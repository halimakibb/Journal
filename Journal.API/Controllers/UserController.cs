using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Journal.BusinessLogics.Interfaces;
using Journal.DataTransferObjects;
using Journal.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Journal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserBusinessLogic _userBL;
        
        public UserController(IUserBusinessLogic userBL)
        {
            _userBL = userBL;
        }

        [HttpGet]
        [Authorize]
        public ActionResult<ReturnMessage> GetAll()
        {
            return _userBL.GetAll(); 
        }

        [HttpPost]
        [Route("register")]
        public ActionResult<ReturnMessage> Register(UserDto userDto)
        {
            User user = new User();
            user.Username = userDto.Username;
            user.Password = userDto.Password;
            user.Email = userDto.Email;
            return _userBL.Register(user);
        }

        [HttpPost]
        [Route("login")]
        public ActionResult<ReturnMessage> Login(UserDto userDto)
        {
            User user = new User();
            user.Username = userDto.Username;
            user.Password = userDto.Password;
            user.Email = userDto.Email;
            
            return _userBL.Login(user);
        }
    }
}
