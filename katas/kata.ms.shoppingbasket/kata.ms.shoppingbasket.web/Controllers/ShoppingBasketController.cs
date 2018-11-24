using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kata.ms.shoppingbasket.domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace kata.ms.shoppingbasket.web.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingBasketController : ControllerBase
    {
        private IDataSourceService<ShoppingBasket> @object;

        public ShoppingBasketController(IDataSourceService<ShoppingBasket> @object)
        {
            this.@object = @object;
        }

        public async Task<OkObjectResult> Get()
        {
            throw new NotImplementedException();
        }
    }
}