using GuessIt.Models;
using GuessIt.Data;
using GuessIt.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GuessIt.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _context;
    
    public CategoryRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Category>> GetAllCategories()
    {
        return await _context.Categories.ToListAsync();
    }
    
    public async Task<Category?> GetCategoryById(int id)
    {
        return await _context.Categories.FindAsync(id);
    }
    
    public async Task<Category> AddCategory(Category category)
    {
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
        return category;
    }
    
    public async Task<Category?> UpdateCategory(int id, Category category)
    {
        var existingCategory = await _context.Categories.FindAsync(id);
        if (existingCategory == null)
        {
            return null;
        }
        
        existingCategory.Name = category.Name;
        await _context.SaveChangesAsync();
        return existingCategory;
    }

    public async Task<bool> DeleteCategory(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
        return true;
    }
}