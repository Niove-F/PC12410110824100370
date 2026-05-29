using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Text;
using PC1.DAW.CORE.Core.Entities;
using PC1.DAW.CORE.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using PC1.DAW.CORE.Core.Interfaces;

namespace PC1.DAW.CORE.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly StoreDbContext _context;

        public CategoryRepository(StoreDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _context.Category.ToListAsync();
        }
        //Create Category
        public async Task<Category> GetCategoryById(int id)
        {
            return await _context.Category.Where(c => c.Id == id).FirstOrDefaultAsync();
        }
        //Update Category
        public async Task UpdateCategory(Category category)
        {
            var existingCategory = await _context.Category.Where(category => category.Id == category.Id).FirstOrDefaultAsync();
            if (existingCategory != null)
            {
                existingCategory.Description = category.Description;
                await _context.SaveChangesAsync();
            }
        }
        //Delete Category
        public async Task DeleteCategory(int id)
        {
            var existingCategory = await _context.Category.Where(c => c.Id == id).FirstOrDefaultAsync();
            if (existingCategory != null)
            {
                existingCategory.IsActive = false;
                await _context.SaveChangesAsync();
            }
        }

    }
}
