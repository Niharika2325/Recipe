using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CookingRecipe.DAL.Interfaces;
using CookingRecipe.Models;

namespace CookingRecipe.Controllers
{
    [RoutePrefix("api/Profile")]
    public class ProfileController : ApiController
    {
            private readonly IProfile _profileService;

            public ProfileController(IProfile profileService)
            {
                _profileService = profileService;
            }

            [HttpGet]
            [Route("GetUserById/{id}")]
            public HttpResponseMessage GetUserById(int id)
            {
                var user = _profileService.GetUserById(id);
                if (user == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User not found");
                }

                return Request.CreateResponse(HttpStatusCode.OK, user);
            }

            [HttpPut]
            [Route("UpdateUserField/{id}")]
            public HttpResponseMessage UpdateUserField(int id, [FromBody] UserFieldUpdate model)
            {
                try
                {
                    _profileService.UpdateUserField(id, model);
                    return Request.CreateResponse(HttpStatusCode.OK, "User field updated successfully.");
                }
                catch (ArgumentException ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }
        
    }

        



}

