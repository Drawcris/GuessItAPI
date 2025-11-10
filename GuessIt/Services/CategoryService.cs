using GuessIt.Models;
using GuessIt.Repositories;
using GuessIt.DTOs;
using GuessIt.Repositories.Interfaces;

namespace GuessIt.Services;

public class CategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    
    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<List<Category>> GetAllCategories()
    {
       return await _categoryRepository.GetAllCategories();
       
    }
    
    public async Task<Category?> GetCategoryById(int id)
    {
        return await _categoryRepository.GetCategoryById(id);
    }

    public async Task<Category> AddCategory(AddCategoryDTO dto)
    {
        var category = new Category()
        {
            Name = dto.Name
        };
        return await _categoryRepository.AddCategory(category);
    }
    
    public async Task<Category?> UpdateCategory(int id, AddCategoryDTO dto)
    {
        var category = new Category()
        {
            Name = dto.Name
        };
        return await _categoryRepository.UpdateCategory(id, category);
    }
    
    public async Task<bool> DeleteCategory(int id)
    {
        return await _categoryRepository.DeleteCategory(id);
    }
}