using System;
using System.Threading.Tasks;
using AllSpice.Models;
using AllSpice.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AllSpice.Controllers
{
    [ApiController]
    [Route("api/recipes/{recipeId}/[controller]")]
    public class IngredientsController : ControllerBase
    {
        private readonly IngredientsService _iService;
        public IngredientsController(IngredientsService iService)
        {
            _iService = iService;
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Ingredient>> Create([FromBody] Ingredient ingredientData)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                Ingredient ingredient = _iService.Create(ingredientData, userInfo);
                return Ok(ingredient);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Ingredient>> Edit([FromBody] Ingredient updates, int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                updates.Id = id;
                Ingredient updated = _iService.Edit(updates, userInfo);
                return Ok(updated);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("id")]
        [Authorize]
        public async Task<ActionResult<string>> Remove(int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                return Ok(_iService.Remove(userInfo, id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}