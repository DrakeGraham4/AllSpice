using System;
using System.Data;
using System.Linq;
using AllSpice.Models;
using Dapper;

namespace AllSpice.Repositories
{
    public class FavoritesRepository
    {
        private readonly IDbConnection _db;
        public FavoritesRepository(IDbConnection db)
        {
            _db = db;
        }
        internal Favorite GetById(int id)
        {
            string sql = @"
           SELECT
           f.*
           FROM favorites f
           WHERE f.id = @id;
           ";
            return _db.Query<Favorite>(sql, new { id }).FirstOrDefault();
        }

        internal object Create(Favorite favoriteData)
        {
            string sql = @"
            INSERT INTO favorites
            (accountId, recipeId)
            VALUES
            (@AccountId, @RecipeId);
            SELECT LAST_INSERT_ID();";
            int id = _db.ExecuteScalar<int>(sql, favoriteData);
            favoriteData.Id = id;
            return favoriteData;
        }

        internal object Remove(int id)
        {
            string sql = @"
            DELETE FROM favorites
            WHERE id = @id LIMIT 1;";
            int rowsAffected = _db.Execute(sql, new { id });
            if (rowsAffected > 0)
            {
                return "deleted";
            }
            throw new Exception("could not delete");
        }
    }
}
