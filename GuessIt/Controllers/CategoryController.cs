using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GuessIt.Services;
using GuessIt.DTOs;
using GuessIt.Models;

namespace GuessIt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;
        
        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("get-all-categories")]
        public async Task<ActionResult<List<Category>>> GetAllCategories()
        {
            var results = await _categoryService.GetAllCategories();
            if (results.Count == 0)
            {
                return NotFound("No categories found.");
            }

            return Ok(results);
        }

        [HttpGet("get-category-by-id/{id}")]
        public async Task<ActionResult<Category?>> GetCategoryById(int id)
        {   
            var category =  await _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound("Category not found.");
            }
            return Ok(category);
        }

        [HttpPost("add-category")]
        public async Task<ActionResult<Category>> AddCategory(AddCategoryDTO dto)
        {
            var result = await _categoryService.AddCategory(dto);
            return Ok(result);
        }

        [HttpPut("update-category/{id}")]
        public async Task<ActionResult<Category?>> UpdateCategory(int id, AddCategoryDTO dto)
        {
            var category = await _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound("Category not found.");
            }
            var result = await _categoryService.UpdateCategory(id, dto);
            return Ok(result);
        }

        [HttpDelete("delete-category/{id}")]
        public async Task<ActionResult<bool>> DeleteCategory(int id)
        {
            var category = await _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound("Category not found.");
            }
            var result = await _categoryService.DeleteCategory(id);
            return Ok(result);
        }
        
    }
}
