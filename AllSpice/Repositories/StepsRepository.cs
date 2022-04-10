using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using AllSpice.Models;
using Dapper;

namespace AllSpice.Repositories
{
    public class StepsRepository
    {
        private readonly IDbConnection _db;

        public StepsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal List<Step> GetStepsByRecipeId(int id)
        {
            string sql = @"
            SELECT * FROM steps s
            WHERE s.recipeId = @id;
            ";
            return _db.Query<Step>(sql, new { id }).ToList();
        }

        internal Step Create(Step stepData)
        {
            string sql = @"
            INSERT INTO steps
                (position, body, recipeId)
            VALUES
                (@Position, @Body, @RecipeId);
            SELECT LAST_INSERT_ID();";
            int id = _db.ExecuteScalar<int>(sql, stepData);
            stepData.Id = id;
            return stepData;
        }

        internal Step GetById(int id)
        {
            string sql = @"
            SELECT * FROM steps
            WHERE id = @id;";
            return _db.QueryFirstOrDefault<Step>(sql, new { id });
        }

        internal Step Update(Step original)
        {
            string sql = @"
            UPDATE steps
            SET 
            position = @Position,
            body = @Body
            WHERE id = @Id;";
            _db.Execute(sql, original);
            return original;
        }

        internal object Remove(int id)
        {
            string sql = @"
            DELETE FROM steps 
            WHERE id = @id LIMIT 1;";
            int rowsAffected = _db.Execute(sql, new { id });
            if (rowsAffected > 0)
            {
                return "Step removed";
            }
            throw new Exception("Cannot remove step");
        }
    }
}