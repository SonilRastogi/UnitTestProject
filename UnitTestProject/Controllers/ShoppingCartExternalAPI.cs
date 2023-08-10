using Microsoft.AspNetCore.Mvc;
using ShoppingCartProject.Inteface;
using System;
using System.Threading.Tasks;

namespace ShoppingCartProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShoppingCartExternalAPI : ControllerBase
    {
        private readonly IExternalApiService _externalApiService;

        public ShoppingCartExternalAPI(IExternalApiService externalApiService)
        {
            _externalApiService = externalApiService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            try
            {
                var product = await _externalApiService.GetProductAsync(id);
                
                if (product == null)
                {
                    return NotFound();
                }
                else
                    return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}


