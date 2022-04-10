using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AllSpice.Models;
using AllSpice.Services;
using Microsoft.AspNetCore.Authorization;
using CodeWorks.Auth0Provider;

namespace AllSpice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipesController : ControllerBase
    {
        private readonly RecipesService _rService;
        private readonly IngredientsService _iService;

        private readonly StepsService _sService;

        public RecipesController(RecipesService rService, IngredientsService iService, StepsService sService)
        {
            _rService = rService;
            _iService = iService;
            _sService = sService;
        }




        [HttpGet]
        public ActionResult<List<Recipe>> GetAll()
        {
            try
            {
                List<Recipe> recipes = _rService.GetAll();
                return Ok(recipes);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}/ingredients")]
        public ActionResult<List<Ingredient>> GetIngredientsByRecipeId(int id)
        {
            try
            {
                List<Ingredient> ingredients = _iService.GetIngredientsByRecipeId(id);
                return Ok(ingredients);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Recipe> GetById(int id)
        {
            try
            {
                Recipe recipe = _rService.GetById(id);
                return Ok(recipe);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Recipe>> Create([FromBody] Recipe recipeData)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                recipeData.CreatorId = userInfo.Id;
                Recipe recipe = _rService.Create(recipeData);
                recipe.Creator = userInfo;
                return Created($"api/recipes/{recipe.Id}", recipe);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Recipe>> Edit([FromBody] Recipe updates, int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                updates.Id = id;
                Recipe updated = _rService.Edit(updates, userInfo);
                return Ok(updated);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<string>> Remove(int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                return Ok(_rService.Remove(id, userInfo));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}/steps")]
        public ActionResult<List<Step>> GetStepsByRecipeId(int id)
        {
            try
            {
                List<Step> steps = _sService.GetStepsByRecipeId(id);
                return Ok(steps);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}