using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CookingRecipe.DAL.Interfaces;
using CookingRecipe.Models;

namespace CookingRecipe.Controllers
{
    [RoutePrefix("api/Recipe")]
    public class RecipeController : ApiController
    {
        private readonly IRecipe _recipeService;

        public RecipeController(IRecipe recipeService)
        {
            _recipeService = recipeService;
        }

        [HttpGet]
        [Route("GetRecipes")]
        public HttpResponseMessage Get()
        {
            try
            {
                var recipes = _recipeService.GetAllRecipes();
                return Request.CreateResponse(HttpStatusCode.OK, recipes);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving recipes: {ex.Message}");
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An error occurred while retrieving recipes.");
            }
        }
        [HttpGet]
        [Route("GetRecipesByUserID/{userID}")]
        public HttpResponseMessage GetRecipesByUserID(int userID)
        {
            try
            {
                var recipes = _recipeService.GetRecipesByUserId(userID);
                return recipes.Any()
                    ? Request.CreateResponse(HttpStatusCode.OK, recipes)
                    : Request.CreateResponse(HttpStatusCode.NotFound, "No recipes found for this user.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving recipes for user ID {userID}: {ex.Message}");
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An error occurred while retrieving recipes.");
            }
        }
        [HttpGet]
        [Route("GetRecipeById/{RecipeID}")]
        public HttpResponseMessage GetRecipeById(int recipeId)
        {
            try
            {
                var recipe = _recipeService.GetRecipeByRecipeId(recipeId);
                if (recipe != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, recipe);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Recipe not found");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while fetching the recipe with ID {recipeId}: {ex.Message}");
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An error occurred while fetching the recipe.");
            }

        }
        [HttpPost]
        [Route("PostRecipes")]
        public HttpResponseMessage Post([FromBody] Recipes recipe)
        {
            try
            {
                if (recipe == null)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Recipe object is null");

                Console.WriteLine($"Received recipe: {recipe.RecipeName}, {recipe.UserID}"); // Debugging

                _recipeService.AddRecipe(recipe);
                return Request.CreateResponse(HttpStatusCode.Created, recipe);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while adding a recipe: {ex.Message}");
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An error occurred while adding the recipe.");
            }
        }

        [HttpPut]
        [Route("EditRecipes/{RecipeID}")]
        public HttpResponseMessage Put(int recipeId, [FromBody] Recipes recipe)
        {
            try
            {
                _recipeService.UpdateRecipe(recipeId, recipe);
                return Request.CreateResponse(HttpStatusCode.OK, recipe);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while updating the recipe with ID {recipeId}: {ex.Message}");
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An error occurred while updating the recipe.");
            }
        }

        [HttpDelete]
        [Route("DeleteRecipe/{RecipeID}")]
        public HttpResponseMessage Delete(int RecipeID)
        {
            try
            {
                _recipeService.DeleteRecipe(RecipeID);
                return Request.CreateResponse(HttpStatusCode.OK, "Deleted Successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while deleting the recipe with ID {RecipeID}: {ex.Message}");
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An error occurred while deleting the recipe.");
            }
        }

    }
}
