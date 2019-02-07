using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Journal.Entities;
using Journal.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using Journal.Configurations;
using Microsoft.Extensions.Options;

namespace Journal.Repositories
{
public class UserRepository : IUserRepository
{
    private readonly ConnectionStrings _connectionStrings;

    #region Get Connection
    public UserRepository(IOptionsMonitor<ConnectionStrings> connectionStrings) {
        _connectionStrings = connectionStrings.CurrentValue;
    }

        public IDbConnection Connection 
        {
        get
        {
            return new SqlConnection(_connectionStrings.DefaultConnection);
        }
    }
    #endregion

    #region Stored Procedures
    const string sp_User_GetAll = "Sp_User_GetAll";
    const string sp_User_GetByUsername = "Sp_User_GetByUsername";
    const string sp_User_GetByEmail = "Sp_User_GetByEmail";
    const string sp_User_Register = "Sp_User_Register";
    string sp_User_Update = "Sp_User_Update";
    string sp_User_Deactivate = "Sp_User_Deactivate";
    #endregion

    public void AddToken(User user)
    {
        using (IDbConnection dbConnection = Connection)
        {
            List<User> users = new List<User>();
            try
            {
                dbConnection.Open();
                using (var transaction = dbConnection.BeginTransaction())
                {
                    try
                    {
                        dbConnection.Query<User>(sp_User_Update,
                            new { UserId = user.UserId,
                                Token = user.Token },
                            transaction: transaction,
                            commandType: CommandType.StoredProcedure);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    } 
                }
                
                
            }
            catch (Exception ex)
            {
                //placeholder
                Console.WriteLine(ex.Message);
                throw ex;
            }
            finally
            {
                dbConnection.Close();
                dbConnection.Dispose();
            }
        }
    }
    public void DeactivateUser(User user)
    {
        using (IDbConnection dbConnection = Connection)
        {
            List<User> users = new List<User>();
            try
            {
                dbConnection.Open();
                using (var transaction = dbConnection.BeginTransaction())
                {
                    try
                    {
                        dbConnection.Query<User>(sp_User_Deactivate,
                            new { UserId = user.UserId,
                                UpdatedBy = user.UpdatedBy },
                            commandType: CommandType.StoredProcedure);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    } 
                }
                
                
            }
            catch (Exception ex)
            {
                //placeholder
                Console.WriteLine(ex.Message);
                throw ex;
            }
            finally
            {
                dbConnection.Close();
                dbConnection.Dispose();
            }

        }
    }

    public IEnumerable<User> GetAll()
    {
        using (IDbConnection dbConnection = Connection)
        {
            List<User> users = new List<User>();
            try
            {
                string storedProcedure = sp_User_GetAll;
                dbConnection.Open();
                users = dbConnection.Query<User>(storedProcedure,
                    commandType: CommandType.StoredProcedure).AsList();
            }
            catch (Exception ex)
            {
                //placeholder
                Console.WriteLine(ex.Message);
                throw ex;
            }
            finally
            {
                dbConnection.Close();
                dbConnection.Dispose();
            }

            return users;
        }
    }

    public User GetById(long id)
    {
        using (IDbConnection dbConnection = Connection)
        {
            User user = new User();
            try
            {
                dbConnection.Open();
                user = dbConnection.QueryFirstOrDefault<User>(sp_User_GetAll,
                    commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                //placeholder
                Console.WriteLine(ex.Message);
                throw ex;
            }
            finally
            {
                dbConnection.Close();
                dbConnection.Dispose();
            }

            return user;
        }
    }

    public void Register(User user)
    {
        using (IDbConnection dbConnection = Connection)
        {
            List<User> users = new List<User>();
            try
            {
                dbConnection.Open();
                using (var transaction = dbConnection.BeginTransaction())
                {
                    try
                    {
                        dbConnection.Query<User>(sp_User_Register,
                        new { Username = user.Username,
                            Email = user.Email,
                            Password = user.Password,
                            CreatedBy = user.CreatedBy,
                            UpdatedBy = user.UpdatedBy },
                            transaction: transaction,
                            commandType: CommandType.StoredProcedure);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    } 
                }
                
            }
            catch (Exception ex)
            {
                //placeholder
                Console.WriteLine(ex.Message);
                throw ex;
            }
            finally
            {
                dbConnection.Close();
                dbConnection.Dispose();
            }
        }
    }

    public User GetByUsername(string username)
    {
        using (IDbConnection dbConnection = Connection)
        {
            User user = null;
            try
            {
                dbConnection.Open();
                user = dbConnection.QueryFirstOrDefault<User>(sp_User_GetByUsername,
                    new { Username = username },
                    commandType: CommandType.StoredProcedure);
                
            }
            catch (Exception ex)
            {
                //placeholder
                Console.WriteLine(ex.Message);
                throw ex;
            }
            finally
            {
                dbConnection.Close();
                dbConnection.Dispose();
            }
            
            return user;
        }
    }

    public User GetByEmail(string email)
    {
        using (IDbConnection dbConnection = Connection)
        {
            User user = new User();
            try
            {
                dbConnection.Open();
                user = dbConnection.QueryFirstOrDefault<User>(sp_User_GetByEmail,
                    new { Email = email },
                    commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                //placeholder
                Console.WriteLine(ex.Message);
                throw ex;
            }
            finally
            {
                dbConnection.Close();
                dbConnection.Dispose();
            }

            return user;
        }
    }
}
}