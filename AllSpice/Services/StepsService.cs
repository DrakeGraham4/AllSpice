using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AllSpice.Models;
using AllSpice.Repositories;

namespace AllSpice.Services
{
    public class StepsService
    {
        private readonly StepsRepository _sRepo;
        private readonly RecipesService _rService;

        public StepsService(StepsRepository sRepo, RecipesService rService)
        {
            _sRepo = sRepo;
            _rService = rService;
        }

        internal List<Step> GetStepsByRecipeId(int id)
        {
            return _sRepo.GetStepsByRecipeId(id);
        }

        internal Step Create(Step stepData, Account userInfo)
        {
            Recipe recipe = _rService.GetById(stepData.RecipeId);
            stepData.RecipeId = recipe.Id;
            if (recipe.CreatorId != userInfo.Id)
            {
                throw new Exception("Not your recipe bruv");
            }
            return _sRepo.Create(stepData);
        }

        internal Step Update(Step stepData, Account userInfo)
        {
            Step original = _sRepo.GetById(stepData.Id);
            Recipe recipe = _rService.GetById(original.RecipeId);
            stepData.RecipeId = recipe.Id;
            if (recipe.CreatorId != userInfo.Id)
            {
                throw new Exception("not your recipe");
            }
            original.Position = stepData.Position;
            original.Body = stepData.Body ?? original.Body;
            original.RecipeId = original.RecipeId;
            _sRepo.Update(original);
            return original;
        }

        internal object Remove(Account userInfo, int id)
        {
            Step step = _sRepo.GetById(id);
            Recipe recipe = _rService.GetById(step.RecipeId);
            if (recipe.CreatorId != userInfo.Id)
            {
                throw new Exception("Not your recipe");
            }
            return _sRepo.Remove(id);
        }
    }
}