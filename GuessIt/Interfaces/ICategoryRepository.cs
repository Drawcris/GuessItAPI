using GuessIt.Models;

namespace GuessIt.Interfaces;

public interface ICategoryRepository
{
    public Task<List<Category>> GetAllCategories();
    public Task<Category?> GetCategoryById(int id);
    public Task<Category> AddCategory(Category category);
    public Task<Category?> UpdateCategory(int id, Category category);
    public Task<bool> DeleteCategory(int id);
}