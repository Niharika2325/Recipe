using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CookingRecipe.Models
{
    public class Recipes
    {
        public int RecipeID { get; set; }
        public int UserID { get; set; }
        public string RecipeName { get; set; }
        public string RecipeDescription { get; set; }
        public string Ingredients { get; set; }
        public string Instructions { get; set; }
        public string Category { get; set; }
        public string State { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}