using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CookingRecipe.Models;
using CookingRecipe.DAL.Interfaces;
using System.Data.SqlClient;

namespace CookingRecipe.DAL.Services
{
    public class CategoryService : ICategory
    {
        private readonly string _connectionString;

        public CategoryService(string connectionString)
        {
            _connectionString = connectionString;
        }
        public IEnumerable<Recipes> GetRecipesByCategory(string category)
        {
            var recipes = new List<Recipes>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Recipes WHERE Category = @Category";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Category", category);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows) // Check if there are rows to read
                        {


                            while (reader.Read())
                            {
                                var recipe = new Recipes
                                {
                                    RecipeID = reader.GetInt32(reader.GetOrdinal("RecipeID")),
                                    UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                                    RecipeName = reader.GetString(reader.GetOrdinal("RecipeName")),
                                    RecipeDescription = reader.GetString(reader.GetOrdinal("RecipeDescription")),
                                    Ingredients = reader.GetString(reader.GetOrdinal("Ingredients")),
                                    Instructions = reader.GetString(reader.GetOrdinal("Instructions")),
                                    Category = reader.GetString(reader.GetOrdinal("Category")),
                                    State = reader.GetString(reader.GetOrdinal("State")),
                                };
                                recipes.Add(recipe);
                            }
                        }

                    }
                }
                return recipes;
            }
        }
    }
}