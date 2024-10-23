using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CookingRecipe.Models;

namespace CookingRecipe.DAL.Interfaces
{
    public interface ICategory
    {
        IEnumerable<Recipes> GetRecipesByCategory(string category);

    }
}