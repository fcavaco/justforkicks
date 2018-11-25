using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using kata.LawnMower.Models;
using Microsoft.AspNetCore.Mvc;

namespace kata.LawnMower.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LawnMowerController : ControllerBase
    {
        private readonly IControlledDevice _device;
        public LawnMowerController(IControlledDevice device)
        {
            _device = device;
        }

        // GET api/lawnmower/positions/current
        [HttpGet("/positions/current")]
        public async Task<IActionResult> GetCurrentPosition()
        {
            var position = _device.CurrentPosition();
            return Ok(position);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
