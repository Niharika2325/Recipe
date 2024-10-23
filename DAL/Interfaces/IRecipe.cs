using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CookingRecipe.Models;

namespace CookingRecipe.DAL.Interfaces
{
    public interface IRecipe
    {
        IEnumerable<Recipes> GetAllRecipes();
        IEnumerable<Recipes> GetRecipesByUserId(int userId);
        Recipes GetRecipeByRecipeId(int recipeId);
        void AddRecipe(Recipes recipe);
        void UpdateRecipe(int recipeId, Recipes recipe);
        void DeleteRecipe(int recipeId);
    }
}