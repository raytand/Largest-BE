using Largest.Application.DTO_s;
using Largest.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Largest.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoriesController(ICategoryService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int userId = int.Parse(User.FindFirst("id")!.Value);
            var category = await _service.CreateCategoryAsync(userId, dto.Name, dto.IsIncome);
            return Ok(category);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            int userId = int.Parse(User.FindFirst("id")!.Value);
            await _service.DeleteCategoryAsync(userId, id);
            return NoContent();
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            int userId = int.Parse(User.FindFirst("id")!.Value);
            var categories = await _service.GetAllCategoriesByUserIdAsync(userId);
            return Ok(categories);
        }
    }

}
