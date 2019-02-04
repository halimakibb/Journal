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
        public IEnumerable<User> GetAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                List<User> users = new List<User>();
                try
                {
                    string sQuery = "SELECT * FROM Users";
                    dbConnection.Open();
                    users = dbConnection.Query<User>(sQuery).AsList();
                }
                catch (Exception ex)
                {
                    //placeholder
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    dbConnection.Close();
                    dbConnection.Dispose();
                }

                return users;
            }
        }
    }
}