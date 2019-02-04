using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Journal.BusinessLogics.Interfaces;
using Journal.Entities;
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
        public ActionResult<ReturnMessage> GetAll()
        {
            return _userBL.GetAll(); 
        }

    }
}
