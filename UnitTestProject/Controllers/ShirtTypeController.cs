using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ShirtType.Interface;
using ShirtType.Models;

namespace ShoppingCartProject.Controllers
{
    [ApiController]
        [Route("api/[controller]")]
        public class ShirtTypeController : ControllerBase
        {
            private readonly IShirtTypeService _shirtTypeService;

            public ShirtTypeController(IShirtTypeService shirtTypeService)
            {
                _shirtTypeService = shirtTypeService;
            }

            [HttpGet]
            public IActionResult GetAllShirtTypes()
            {
                IEnumerable<ShirtTypeModel> shirtTypes = _shirtTypeService.GetAllShirtTypes();
                return Ok(shirtTypes);
            }

            [HttpGet("{shirtType}")]
            public IActionResult GetShirtType(string shirtType)
            {
                ShirtTypeModel shirtTypeModel = _shirtTypeService.GetShirtType(shirtType);
                if (shirtTypeModel != null)
                {
                    return Ok(shirtTypeModel);
                }
                return NotFound();
            }

            [HttpPost]
            public IActionResult AddShirtType(ShirtTypeModel shirtType)
            {
                _shirtTypeService.AddShirtType(shirtType);
                return CreatedAtAction(nameof(GetShirtType), new { shirtType = shirtType.ShirtType }, shirtType);
            }

            [HttpPut("{shirtType}")]
            public IActionResult UpdateShirtType(string shirtType, ShirtTypeModel updatedShirtType)
            {
                if (shirtType != updatedShirtType.ShirtType)
                {
                    return BadRequest();
                }

                _shirtTypeService.UpdateShirtType(updatedShirtType);
                return NoContent();
            }

            [HttpDelete("{shirtType}")]
            public IActionResult DeleteShirtType(string shirtType)
            {
                _shirtTypeService.DeleteShirtType(shirtType);
                return NoContent();
            }
        }
    }

