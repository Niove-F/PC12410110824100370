using PC1.DAW.CORE.Core.Entities;

namespace PC1.DAW.CORE.Core.Interfaces
{
    public interface ICategoryRepository
    {
        Task DeleteCategory(int id);
        Task<IEnumerable<Category>> GetCategories();
        Task<Category> GetCategoryById(int id);
        Task UpdateCategory(Category category);
    }
}