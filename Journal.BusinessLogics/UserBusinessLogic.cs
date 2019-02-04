using System;
using System.Collections.Generic;
using Journal.BusinessLogics.Interfaces;
using Journal.Entities;
using Journal.Repositories.Interfaces;
using Newtonsoft.Json;

namespace Journal.BusinessLogics
{
    public class UserBusinessLogic : IUserBusinessLogic
    {
        private readonly IUserRepository _userRepo;
        public UserBusinessLogic (IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public ReturnMessage DeactivateUser(long id)
        {
            ReturnMessage returnMessage = new ReturnMessage(){
                IsSuccess = false
            };

            try
            {
                _userRepo.DeactivateUser(id);
                returnMessage.IsSuccess = true;
                returnMessage.Message = "User has been deactivated successfully";
            }
            catch (Exception ex)
            {
                returnMessage.Message = ex.Message;
            }
            
            return returnMessage;

        }
        
        public ReturnMessage GetAll()
        {
            ReturnMessage returnMessage = new ReturnMessage(){
                IsSuccess = false
            };
            
            try
            {
                returnMessage.IsSuccess = true;
                returnMessage.Data = JsonConvert.SerializeObject(_userRepo.GetAll());
            }
            catch (Exception ex)
            {
                returnMessage.Message = ex.Message;
                returnMessage.Data = JsonConvert.SerializeObject(new List<User>());
            }
            
            return returnMessage;
        }

        public ReturnMessage GetByEmail(string email)
        {
            throw new System.NotImplementedException();
        }

        public ReturnMessage GetById(long id)
        {
            throw new System.NotImplementedException();
        }

        public ReturnMessage GetByUsername(string username)
        {
            throw new System.NotImplementedException();
        }

        public ReturnMessage Login(User user)
        {
            throw new System.NotImplementedException();
        }

        public ReturnMessage Register(User user)
        {
            throw new System.NotImplementedException();
        }
    }
}