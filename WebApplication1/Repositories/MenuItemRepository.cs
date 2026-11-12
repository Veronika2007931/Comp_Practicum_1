using CoffeeShopApi.Data;
using CoffeeShopApi.Models;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace CoffeeShopApi.Repositories;

public class MenuItemRepository : IMenuItemRepository
{
    private readonly CoffeeShopDbContext _context;

    public MenuItemRepository(CoffeeShopDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MenuItem>> GetAllAsync()
    {
        return await _context.MenuItem
        .Include(m => m.Category)
        .AsNoTracking()
        .ToListAsync();
    }

    public async Task<MenuItem?> GetByIdAsync(int id)
    {
        return await _context.MenuItem
        .Include(m => m.Category)
        .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<IEnumerable<MenuItem>> GetByCategoryIdAsync(int categoryId)
    {
        return await _context.MenuItem
        .Include(m => m.Category)
        .Where(m => m.CategoryId == categoryId)
        .AsNoTracking()
        .ToListAsync();
    }

    public async Task<MenuItem> AddAsync(MenuItem menuItem)
    {
        _context.MenuItem.Add(menuItem);
        await _context.SaveChangesAsync();

        await _context.Entry(menuItem).Reference(m => m.Category).LoadAsync();
        return menuItem;
    }

    public async Task<bool> UpdateAsync(MenuItem menuItem)
    {
        var existing = await _context.MenuItem.FindAsync(menuItem.Id);
        if (existing is null)
        {
            return false;
        }
        existing.Name = menuItem.Name;
        existing.Description = menuItem.Description;
        existing.Price = menuItem.Price;
        existing.VolumeMl = menuItem.VolumeMl;
        existing.CategoryId = menuItem.CategoryId;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var item = await _context.MenuItem.FindAsync(id);
        if (item is null)
        {
            return false;
        }

        _context.MenuItem.Remove(item);
        await _context.SaveChangesAsync();
        return true;

    }

    public async Task DeleteAllAsync()
    {
        await _context.MenuItem.ExecuteDeleteAsync();
    }

}