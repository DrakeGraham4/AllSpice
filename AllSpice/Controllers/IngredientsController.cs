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
    [Route("api/[controller]")]
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
                Ingredient ingredient = _iService.Create(ingredientData, userInfo.Id);
                return Created($"api/ingredients/{ingredient.Id}", ingredient);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}