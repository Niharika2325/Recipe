using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using CookingRecipe.DAL.Interfaces;
using CookingRecipe.Models;
using Serilog;

namespace CookingRecipe.DAL.Services
{
    public class RecipeService : IRecipe
    {
        private readonly string _connectionString;

        public RecipeService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Recipes> GetAllRecipes()
        {
            var recipes = new List<Recipes>();
            try {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Recipes";
                    using (var cmd = new SqlCommand(query, connection))
                    {
                        using (var reader = cmd.ExecuteReader())
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
            
            }
            catch(Exception ex)
            {
                Log.Error(ex, "An error occurred while getting all recipes");
               
            }
            return recipes;
        }

        public IEnumerable<Recipes> GetRecipesByUserId(int userId)
        {
            var recipes = new List<Recipes>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var cmd = new SqlCommand("SELECT * FROM Recipes WHERE UserID = @UserID", connection))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userId);
                        using (var reader = cmd.ExecuteReader())
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
            }
            catch(Exception ex)
            {
                Log.Error(ex, "An error occurred while getting all the details of the recipes");
                
            }
            return recipes;
        }
        public Recipes GetRecipeByRecipeId(int recipeId)
        {
            Recipes recipe = null;
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var cmd = new SqlCommand("SELECT * FROM Recipes WHERE RecipeID = @RecipeID", connection))
                    {
                        cmd.Parameters.AddWithValue("@RecipeID", recipeId);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read()) // We expect only one result, as RecipeID should be unique
                            {
                                recipe = new Recipes
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
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while getting the recipe by RecipeID");
            }
            return recipe;
        }


        public void AddRecipe(Recipes recipe)
        {
            if (recipe == null)
            {
                throw new ArgumentNullException(nameof(recipe), "Recipe object cannot be null");
            }
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var cmd = new SqlCommand("INSERT INTO Recipes (UserID, RecipeName, RecipeDescription, Ingredients, Instructions, Category, State) VALUES (@UserID, @RecipeName, @RecipeDescription, @Ingredients, @Instructions, @Category, @State)", connection))
                    {
                        cmd.Parameters.AddWithValue("@UserID", recipe.UserID);
                        cmd.Parameters.AddWithValue("@RecipeName", recipe.RecipeName);
                        cmd.Parameters.AddWithValue("@RecipeDescription", recipe.RecipeDescription);
                        cmd.Parameters.AddWithValue("@Ingredients", recipe.Ingredients);
                        cmd.Parameters.AddWithValue("@Instructions", recipe.Instructions);
                        cmd.Parameters.AddWithValue("@Category", recipe.Category);
                        cmd.Parameters.AddWithValue("@State", recipe.State);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                Log.Error(ex, "An error occured while adding the recipe");
            }
        }

        public void UpdateRecipe(int recipeId, Recipes recipe)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var cmd = new SqlCommand("UPDATE Recipes SET RecipeName = @RecipeName, RecipeDescription = @RecipeDescription, Ingredients = @Ingredients, Instructions = @Instructions, Category = @Category, State = @State WHERE RecipeID = @RecipeID", connection))
                    {
                        cmd.Parameters.AddWithValue("@RecipeName", recipe.RecipeName);
                        cmd.Parameters.AddWithValue("@RecipeDescription", recipe.RecipeDescription);
                        cmd.Parameters.AddWithValue("@Ingredients", recipe.Ingredients);
                        cmd.Parameters.AddWithValue("@Instructions", recipe.Instructions);
                        cmd.Parameters.AddWithValue("@Category", recipe.Category);
                        cmd.Parameters.AddWithValue("@State", recipe.State);
                        cmd.Parameters.AddWithValue("@RecipeID", recipeId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                Log.Error(ex, "An error occurred while updating the recipe");
                
            }
        }

        public void DeleteRecipe(int recipeId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var cmd = new SqlCommand("DELETE FROM Recipes WHERE RecipeID = @RecipeID", connection))
                    {
                        cmd.Parameters.AddWithValue("@RecipeID", recipeId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                Log.Error(ex, "An error occured while deleting the recipe");
               
            }
        }
    }
}