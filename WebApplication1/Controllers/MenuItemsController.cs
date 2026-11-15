using CoffeeShopApi.DTOs;
using CoffeeShopApi.Models;
using CoffeeShopApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShopApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MenuItemsController : ControllerBase
{
    private readonly IMenuItemRepository _menuItemRepository;
    private readonly ICategoryRepository _categoryRepository;

    public MenuItemsController(
        IMenuItemRepository menuItemRepository,
        ICategoryRepository categoryRepository)
    {
        _menuItemRepository = menuItemRepository;
        _categoryRepository = categoryRepository;
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<MenuItemDto>>> GetAll()
    {
        var items = await _menuItemRepository.GetAllAsync();
        var dtos = items.Select(MapToDto);
        return Ok(dtos);
    }


    [HttpGet("{id:int}")]
    public async Task<ActionResult<MenuItemDto>> GetById(int id)
    {
        var item = await _menuItemRepository.GetByIdAsync(id);
        if (item is null)
        {
            return NotFound($"Позицію меню з ID {id} не знайдено.");
        }

        return Ok(MapToDto(item));
    }


    [HttpPost]
    public async Task<ActionResult<MenuItemDto>> Create([FromBody] CreateUpdateMenuItemDto dto)
    {
        // Перевіряємо, чи існує вказана категорія в БД
        if (!await _categoryRepository.ExistsAsync(dto.CategoryId))
        {
            return BadRequest($"Категорії з ID {dto.CategoryId} не існує.");
        }

        var menuItem = new MenuItem
        {
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            VolumeMl = dto.VolumeMl,
            CategoryId = dto.CategoryId
        };

        var created = await _menuItemRepository.AddAsync(menuItem);
        var resultDto = MapToDto(created);

        return CreatedAtAction(nameof(GetById), new { id = resultDto.Id }, resultDto);
    }


    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateUpdateMenuItemDto dto)
    {
        if (!await _categoryRepository.ExistsAsync(dto.CategoryId))
        {
            return BadRequest($"Категорії з ID {dto.CategoryId} не існує.");
        }

        var menuItem = new MenuItem
        {
            Id = id,
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            VolumeMl = dto.VolumeMl,
            CategoryId = dto.CategoryId
        };

        var updated = await _menuItemRepository.UpdateAsync(menuItem);
        if (!updated)
        {
            return NotFound($"Позицію меню з ID {id} не знайдено.");
        }

        return NoContent();
    }


    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteById(int id)
    {
        var deleted = await _menuItemRepository.DeleteAsync(id);
        if (!deleted)
        {
            return NotFound($"Позицію меню з ID {id} не знайдено.");
        }

        return NoContent();
    }


    [HttpDelete]
    public async Task<IActionResult> DeleteAll()
    {
        await _menuItemRepository.DeleteAllAsync();
        return NoContent();
    }


    private static MenuItemDto MapToDto(MenuItem item) => new()
    {
        Id = item.Id,
        Name = item.Name,
        Description = item.Description,
        Price = item.Price,
        VolumeMl = item.VolumeMl,
        CategoryId = item.CategoryId,
        CategoryName = item.Category?.Name
    };
}