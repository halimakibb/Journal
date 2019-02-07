using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DevOne.Security.Cryptography.BCrypt;
using Journal.BusinessLogics.Interfaces;
using Journal.Configurations;
using Journal.Entities;
using Journal.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Journal.BusinessLogics
{
    public class UserBusinessLogic : IUserBusinessLogic
    {
        private readonly IUserRepository _userRepo;
        private readonly AppSettings _appSettings;

        public UserBusinessLogic (IUserRepository userRepo, IOptions<AppSettings> appSettings)
        {
            _userRepo = userRepo;
            _appSettings = appSettings.Value;
        }

        public ReturnMessage DeactivateUser(User user)
        {
            ReturnMessage returnMessage = new ReturnMessage(){
                IsSuccess = false
            };

            try
            {
                _userRepo.DeactivateUser(user);
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
            ReturnMessage returnMessage = new ReturnMessage(){
                IsSuccess = false
            };
            
            try
            {
                returnMessage.IsSuccess = true;
                returnMessage.Data = JsonConvert.SerializeObject(_userRepo.GetByEmail(email));
            }
            catch (Exception ex)
            {
                returnMessage.Message = ex.Message;
                returnMessage.Data = JsonConvert.SerializeObject(new User());
            }
            
            return returnMessage;
        }

        public ReturnMessage GetById(long id)
        {
            ReturnMessage returnMessage = new ReturnMessage(){
                IsSuccess = false
            };
            
            try
            {
                returnMessage.IsSuccess = true;
                returnMessage.Data = JsonConvert.SerializeObject(_userRepo.GetById(id));
            }
            catch (Exception ex)
            {
                returnMessage.Message = ex.Message;
                returnMessage.Data = JsonConvert.SerializeObject(new User());
            }
            
            return returnMessage;
        }

        public ReturnMessage GetByUsername(string username)
        {
            ReturnMessage returnMessage = new ReturnMessage(){
                IsSuccess = false
            };
            
            try
            {
                returnMessage.IsSuccess = true;
                returnMessage.Data = JsonConvert.SerializeObject(_userRepo.GetByUsername(username));
            }
            catch (Exception ex)
            {
                returnMessage.Message = ex.Message;
                returnMessage.Data = JsonConvert.SerializeObject(new User());
            }
            
            return returnMessage;
        }

        public ReturnMessage Login(User user)
        {
            ReturnMessage returnMessage = new ReturnMessage(){
                IsSuccess = false,
                Message = "Failed to authenticate user"
            };

            try
            {
                User currentUser = _userRepo.GetByUsername(user.Username);
                if (currentUser != null)
                {
                    bool isPassword = BCryptHelper.CheckPassword(user.Password, currentUser.Password);
                    if (BCryptHelper.CheckPassword(user.Password, currentUser.Password))
                    {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[] 
                        {
                            new Claim(ClaimTypes.Name, currentUser.Username.ToString())
                        }),
                        Expires = DateTime.UtcNow.AddDays(7),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    currentUser.Token = tokenHandler.WriteToken(token);

                    _userRepo.AddToken(currentUser);
                    
                    returnMessage.IsSuccess = true;
                    returnMessage.Message = "User authenticated";
                    returnMessage.Data = JsonConvert.SerializeObject(currentUser);
                    }
                }
                else
                {
                    returnMessage.IsSuccess = false;
                    returnMessage.Message = "Username does not exist";
                }
            }
            catch (Exception ex)
            {
                returnMessage.Message = ex.Message;
            }

            return returnMessage;
        }

        public ReturnMessage Register(User user)
        {
            ReturnMessage returnMessage = new ReturnMessage(){
                IsSuccess = false
            };
            
            try
            {
                User currentUser = _userRepo.GetByEmail(user.Email);
                if (currentUser != null)
                {
                    returnMessage.Message = "Email already registered";
                }
                else
                {
                    currentUser = _userRepo.GetByUsername(user.Username);
                    if (currentUser != null)
                    {
                        returnMessage.Message = "Username already exist";
                    }
                    else
                    {
                        user.Password = BCryptHelper.HashPassword(user.Password, BCryptHelper.GenerateSalt(12));
                        _userRepo.Register(user);

                        returnMessage.IsSuccess = true;
                        returnMessage.Message = "User successfully registered";
                    }
                }
            }
            catch (Exception ex)
            {
                returnMessage.Message = ex.Message;
                returnMessage.Data = JsonConvert.SerializeObject(new User());
            }
            
            return returnMessage;
        }
    }
}