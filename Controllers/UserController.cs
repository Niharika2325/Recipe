using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CookingRecipe.Models;


namespace CookingRecipe.Controllers
{
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString);
        SqlCommand cmd = null;
        SqlDataAdapter da = null;
        [HttpPost]
        [Route("UserRegistration")]
        public string User_Registration(User user)
        {
            string msg = string.Empty;
            try
            {
                cmd = new SqlCommand("usp_Registration", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Username", user.Username);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                cmd.Parameters.AddWithValue("@LastName", user.LastName);
                cmd.Parameters.AddWithValue("@MobileNumber", user.MobileNumber);

                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                if (i > 0)
                {
                    msg = "Registered Successfully";
                }
                else
                {
                    msg = "Error.";
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return msg;
        }

       

        public class LoginResponse
        {
            public string Message { get; set; }
            public int? UserId { get; set; } // Nullable in case login fails
        }
        [HttpPost]
        [Route("UserLogin")]
        public LoginResponse User_Login(User login)
        {
            var response = new LoginResponse();
            try
            {
                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
                using (var da = new SqlDataAdapter("user_Login", conn))
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@Username", login.Username);
                    da.SelectCommand.Parameters.AddWithValue("@Password", login.Password);

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        // Assuming the UserId is in the first row
                        response.UserId = Convert.ToInt32(dt.Rows[0]["UserId"]);
                        response.Message = "Login Successful...!!";
                    }
                    else
                    {
                        response.Message = "Login Failed..!!!";
                    }
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
