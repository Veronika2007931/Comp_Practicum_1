using Microsoft.AspNetCore.Mvc;
using CoffeeShopApi.Data;
using CoffeeShopApi.Models;

namespace CoffeeShopApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MenuItemsController : ControllerBase
{
    private readonly InMemoryStore _store;

    public MenuItemsController(InMemoryStore store)
    {
        _store = store;
    }

    [HttpGet]
    public ActionResult<IEnumerable<MenuItem>> GetAll()
    {
        return Ok(_store.MenuItems.Values);
    }

    [HttpGet("{id:int}")]
    public ActionResult<MenuItem> GetById(int id)
    {
        if (_store.MenuItems.TryGetValue(id, out var menuItem))
        {
            return Ok(menuItem);
        }
        return NotFound($"Позицію з ID {id} не знайдено.");
    }

    [HttpPost]
    public ActionResult<MenuItem> Create([FromBody] MenuItem menuItem)
    {
        if (!_store.Categories.ContainsKey(menuItem.CategoryId))
        {
            return BadRequest($"Категорії з ID {menuItem.CategoryId} не існує.");
        }
        menuItem.Id = _store.GetNextMenuItemId();
        _store.MenuItems[menuItem.Id] = menuItem;
        return CreatedAtAction(nameof(GetById), new { id = menuItem.Id }, menuItem);
    }

    [HttpPut("{id:int}")]
    public IActionResult Update(int id, [FromBody] MenuItem updated)
    {
        if (!_store.MenuItems.ContainsKey(id))
        {
            return NotFound($"Позицію з ID {id} не знайдено.");
        }
        if (!_store.Categories.ContainsKey(updated.CategoryId))
        {
            return BadRequest($"Категорії з ID {updated.CategoryId} не існує.");
        }

        updated.Id = id;
        _store.MenuItems[id] = updated;
        return Ok(updated);
    }

    [HttpDelete]
    public IActionResult DeleteAll()
    {
        _store.MenuItems.Clear();
        return NoContent();
    }


    [HttpDelete("{id:int}")]
    public IActionResult DeleteById(int id)
    {
        if (_store.MenuItems.TryRemove(id, out _))
        {
            return NoContent();
        }
        return NotFound($"Позицію з ID {id} не знайдено.");
    }
}