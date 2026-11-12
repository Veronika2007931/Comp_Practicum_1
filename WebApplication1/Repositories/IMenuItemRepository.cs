using CoffeeShopApi.Models;

namespace CoffeeShopApi.Repositories;

public interface IMenuItemRepository
{
    Task<IEnumerable<MenuItem>> GetAllAsync();
    Task<MenuItem?> GetByIdAsync(int id);
    Task<IEnumerable<MenuItem>> GetByCategoryIdAsync(int categoryId);
    Task<bool> UpdateAsync(MenuItem menuItem);
    Task<bool> DeleteAsync(int id);
    Task DeleteAllAsync();
}
