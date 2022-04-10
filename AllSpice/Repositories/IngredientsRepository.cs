using System;
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
            (@Name, @Quantity, @RecipeId);
            SELECT LAST_INSERT_ID();";
            int id = _db.ExecuteScalar<int>(sql, ingredientData);
            ingredientData.Id = id;
            return ingredientData;
        }

        internal Ingredient GetById(int id)
        {
            string sql = @"
            SELECT * FROM ingredients
            WHERE id = @id;";
            return _db.QueryFirstOrDefault<Ingredient>(sql, new { id });
        }

        internal List<Ingredient> GetIngredientsByRecipeId(int id)
        {
            string sql = "SELECT * FROM ingredients i WHERE i.recipeId = @id;";
            return _db.Query<Ingredient>(sql, new { id }).ToList();
        }

        internal Ingredient Edit(Ingredient original)
        {
            string sql = @"
            UPDATE ingredients
            SET
            name = @Name,
            quantity = @Quantity
            WHERE id = @Id;";
            _db.Execute(sql, original);
            return original;

        }

        internal object Remove(int id)
        {
            string sql = @"
            DELETE FROM
            ingredients WHERE id = @id 
            LIMIT 1;";

            int rowsAffected = _db.Execute(sql, new { id });
            if (rowsAffected > 0)
            {
                return "Delorted Ingredient";
            }
            throw new Exception("Cannot not delete ingredient");
        }
    }
}