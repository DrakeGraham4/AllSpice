using System;
using System.Collections.Generic;
using AllSpice.Models;
using AllSpice.Repositories;

namespace AllSpice.Services
{
    public class IngredientsService
    {
        private readonly IngredientsRepository _iRepo;

        private readonly RecipesService _rService;
        public IngredientsService(IngredientsRepository iRepo, RecipesService rService)
        {
            _iRepo = iRepo;
            _rService = rService;
        }

        internal Ingredient Create(Ingredient ingredientData, string userId)
        {
            Recipe recipe = _rService.GetById(ingredientData.recipeId);
            if (recipe.creatorId != userId)
            {
                throw new Exception("Not your recipe bruv");
            }
            return _iRepo.Create(ingredientData);
        }

        internal List<Ingredient> GetIngredientsByRecipeId(int id)
        {
            return _iRepo.GetIngredientsByRecipeId(id);
        }
    }
}