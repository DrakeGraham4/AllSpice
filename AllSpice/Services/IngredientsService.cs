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

        internal Ingredient Create(Ingredient ingredientData, Account userInfo)
        {

            Recipe recipe = _rService.GetById(ingredientData.RecipeId);
            if (recipe.CreatorId != userInfo.Id)
            {
                throw new Exception("This is not your recipe");
            }
            return _iRepo.Create(ingredientData);
        }

        internal List<Ingredient> GetIngredientsByRecipeId(int id)
        {
            return _iRepo.GetIngredientsByRecipeId(id);
        }

        internal Ingredient Edit(Ingredient updates, Account userInfo)
        {
            Ingredient original = _iRepo.GetById(updates.Id);
            Recipe recipe = _rService.GetById(original.RecipeId);
            updates.RecipeId = recipe.Id;
            if (recipe.CreatorId != userInfo.Id)
            {
                throw new Exception("Not your ingredient to edit");
            }

            original.Name = updates.Name ?? original.Name;
            original.Quantity = updates.Quantity ?? original.Quantity;
            original.RecipeId = original.RecipeId;
            _iRepo.Edit(original);
            return original;
        }

        internal object Remove(Account userInfo, int id)
        {
            Ingredient ingredient = _iRepo.GetById(id);
            Recipe recipe = _rService.GetById(ingredient.RecipeId);
            if (recipe.CreatorId != userInfo.Id)
            {
                throw new Exception("You cannot delete this recipe");
            }
            return _iRepo.Remove(id);
        }
    }
}