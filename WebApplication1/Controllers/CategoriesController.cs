using Microsoft.AspNetCore.Mvc;
using CoffeeShopApi.Data;
using CoffeeShopApi.Models;

namespace CoffeeShopApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly InMemoryStore _store;

    public CategoriesController(InMemoryStore store)
    {
        _store = store;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Category>> GetAll()
    {
        return Ok(_store.Categories.Values);

    }

    [HttpGet("{id:int}")]
    public ActionResult<Category> GetById(int id)
    {
        if (_store.Categories.TryGetValue(id, out var category))
        {
            return Ok(category);
        }

        return NotFound($"Категорію з ID {id} не знайдено");
    }

    [HttpGet("{id:int}/items")]
    public ActionResult<IEnumerable<MenuItem>> GetItemsByCategory(int id)
    {
        if (!_store.Categories.ContainsKey(id))
        {
            return NotFound($"Категорію з ID {id} не знайдено.");
        }

        var items = _store.MenuItems.Values.Where(items => items.CategoryId == id);
        return Ok(items);
    }

    [HttpPost]
    public ActionResult<Category> Create([FromBody] Category category)
    {
        category.Id = _store.GetNextCategoryId();
        _store.Categories[category.Id] = category;
        return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
    }

    [HttpPut("{id:int}")]
    public IActionResult Update(int id, [FromBody] Category updated)
    {
        if (!_store.Categories.ContainsKey(id))
        {
            return NotFound($"Категорію з ID {id} не знайдено");
        }

        updated.Id = id;
        _store.Categories[id] = updated;
        return Ok(updated);
    }

    [HttpDelete]
    public IActionResult DeleteAll()
    {
        _store.Categories.Clear();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteById(int id)
    {
        if (_store.Categories.TryRemove(id, out _))
        {
            return NoContent();
        }

        return NotFound($"Категорію з ID {id} не знайдено");
    }
}