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
    [RoutePrefix("api/Category")]
    public class CategoryController : ApiController
    {
        private readonly ICategory _categoryService;

        public CategoryController(ICategory categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [Route("GetRecipesByCategory/{category}")]
        public HttpResponseMessage Get(string category)
        {
            var categories = _categoryService.GetRecipesByCategory(category);
            return Request.CreateResponse(HttpStatusCode.OK, categories);
        }
    }

}

