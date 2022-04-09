using System;
using System.Collections.Generic;
using AllSpice.Models;
using AllSpice.Repositories;

namespace AllSpice.Services
{
    public class RecipesService
    {
        private readonly RecipesRepository _rRepo;

        public RecipesService(RecipesRepository rRepo)
        {
            _rRepo = rRepo;
        }

        internal List<Recipe> GetAll()
        {
            return _rRepo.GetAll();
        }

        internal Recipe Create(Recipe recipeData)
        {
            return _rRepo.Create(recipeData);
        }

        internal string Remove(int id, Account user)
        {
            Recipe recipe = _rRepo.GetById(id);
            if (recipe.creatorId != user.Id)
            {
                throw new Exception("Nope");
            }
            return _rRepo.Remove(id);
        }

        internal Recipe GetById(int id)
        {
            Recipe found = _rRepo.GetById(id);
            if (found == null)
            {
                throw new Exception("Invalid Id");
            }
            return found;
        }
    }
}