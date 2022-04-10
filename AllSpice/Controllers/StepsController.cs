using System;
using System.Collections.Generic;
using System.Linq;
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
    public class StepsController : ControllerBase
    {
        private readonly StepsService _sService;
        public StepsController(StepsService sService)
        {
            _sService = sService;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Step>> Create([FromBody] Step stepData)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                Step step = _sService.Create(stepData, userInfo);
                return Ok(step);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Step>> Update([FromBody] Step stepData, int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                stepData.Id = id;
                Step step = _sService.Update(stepData, userInfo);
                return Ok(step);
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
                return Ok(_sService.Remove(userInfo, id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}