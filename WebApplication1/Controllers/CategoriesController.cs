using CoffeeShopApi.DTOs;
using CoffeeShopApi.Models;
using CoffeeShopApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace CoffeeShopApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMenuItemRepository _menuItemRepository;

    public CategoriesController(
        ICategoryRepository categoryRepository,
        IMenuItemRepository menuItemRepository)
    {
        _categoryRepository = categoryRepository;
        _menuItemRepository = menuItemRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Category>>> GetAll()
    {
        var categories = await _categoryRepository.GetAllAsync();
        var dtos = categories.Select(c => new CategoryDto
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description
        });

        return Ok(dtos);

    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Category>> GetById(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category is null)
        {
            return NotFound($"Категорію з ID {id} не знайдено.");
        }

        return Ok(new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description
        });
    }

    [HttpGet("{id:int}/items")]
    public async Task<ActionResult<IEnumerable<MenuItem>>> GetItemsByCategory(int id)
    {
        if (!await _categoryRepository.ExistsAsync(id))
        {
            return NotFound($"Категорію з ID {id} не знайдено.");
        }

        var items = await _menuItemRepository.GetByCategoryIdAsync(id);
        var dtos = items.Select(m => new MenuItemDto
        {
            Id = m.Id,
            Name = m.Name,
            Description = m.Description,
            Price = m.Price,
            VolumeMl = m.VolumeMl,
            CategoryId = m.CategoryId,
            CategoryName = m.Category?.Name
        });
        return Ok(dtos);
    }

    [HttpPost]
    public async Task<ActionResult<Category>> Create([FromBody] CreateUpdateCategoryDto dto)
    {
        var category = new Category
        {
            Name = dto.Name,
            Description = dto.Description
        };

        var created = await _categoryRepository.AddAsync(category);

        var resultDto = new CategoryDto
        {
            Id = created.Id,
            Name = created.Name,
            Description = created.Description
        };

        return CreatedAtAction(nameof(GetById), new { id = resultDto.Id }, resultDto);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateUpdateCategoryDto dto)
    {
        var category = new Category
        {
            Id = id,
            Name = dto.Name,
            Description = dto.Description
        };

        var updated = await _categoryRepository.UpdateAsync(category);
        if (!updated)
        {
            return NotFound($"Категорію з ID {id} не знайдено");
        }

        return NoContent();

    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAll()
    {
        await _categoryRepository.DeleteAllAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteById(int id)
    {
        var deleted = await _categoryRepository.DeleteAsync(id);
        if (!deleted)
        {
            return NotFound($"Категорію з ID {id} не знайдено.");
        }
        return NoContent();
    }
}