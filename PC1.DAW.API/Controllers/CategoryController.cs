using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PC1.DAW.CORE.Core.Entities;
using PC1.DAW.CORE.Core.Interfaces;

namespace PC1.DAW.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryRepository.GetCategories();
            return Ok(categories);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _categoryRepository.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPut]
        public async Task<IActionResult> PutCategory(Category category)
        {
            await _categoryRepository.UpdateCategory(category);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _categoryRepository.DeleteCategory(id);
            return Ok();
        }
    }
}
