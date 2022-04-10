using System.Collections.Generic;
using System.Data;
using System.Linq;
using AllSpice.Models;
using Dapper;

namespace AllSpice.Repositories
{
    public class IngredientsRepository
    {
        private readonly IDbConnection _db;
        public IngredientsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal Ingredient Create(Ingredient ingredientData)
        {
            string sql = @"
            INSERT INTO
            ingredients (name, quantity, recipeId)
            VALUES
            (@Name, @Quantity, @RecipeId);";
            int id = _db.ExecuteScalar<int>(sql, ingredientData);
            ingredientData.Id = id;
            return ingredientData;
        }

        internal List<Ingredient> GetIngredientsByRecipeId(int id)
        {
            string sql = "SELECT * FROM ingredients i WHERE i.recipeId = @id;";
            return _db.Query<Ingredient>(sql, new { id }).ToList();
        }
    }
}