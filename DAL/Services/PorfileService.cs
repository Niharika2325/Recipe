using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using CookingRecipe.DAL.Interfaces;
using CookingRecipe.Models;

namespace CookingRecipe.DAL.Services
{
    public class PorfileService:IProfile
    {
        private readonly string _connectionString;

        public PorfileService(string connectionString)
        {
            _connectionString = connectionString;
        }
        public IEnumerable<User> GetUserById(int id)
        {
            var users = new List<User>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var cmd = new SqlCommand("SELECT * FROM Users WHERE UserID = @UserID", connection))
                {
                    cmd.Parameters.AddWithValue("@UserID", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var user = new User
                            {

                                UserID=reader.GetInt32(reader.GetOrdinal("UserID")),
                                Username = reader.GetString(reader.GetOrdinal("Username")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                


                                MobileNumber = reader.GetString(reader.GetOrdinal("MobileNumber")),

                            };
                            users.Add(user);
                        }
                    }
                }
            }
            return users;
        }
        public void UpdateUserField(int id, UserFieldUpdate model)
        {
            if (id <= 0 || model == null || string.IsNullOrEmpty(model.Field) || model.Value == null)
                throw new ArgumentException("Invalid input");

            var allowedFields = new[] { "Username", "Email", "FirstName", "LastName", "MobileNumber" };
            if (!allowedFields.Contains(model.Field))
                throw new ArgumentException("Invalid field name");

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = $"UPDATE Users SET {model.Field} = @Value WHERE UserID = @UserID";

                using (var cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Value", model.Value);
                    cmd.Parameters.AddWithValue("@UserID", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}