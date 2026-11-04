using System.Collections.Concurrent;
using CoffeeShopApi.Models;

namespace CoffeeShopApi.Data;

public class InMemoryStore
{
    public ConcurrentDictionary<int, Category> Categories { get; } = new();
    public ConcurrentDictionary<int, MenuItem> MenuItems { get; } = new();

    private int _categoryIdCounter = 0;
    private int _menuItemIdCounter = 0;

    public InMemoryStore()
    {
        var coffee = new Category { Id = ++_categoryIdCounter, Name = "Кава", Description = "Класичні кавові напої" };
        var dessert = new Category { Id = ++_categoryIdCounter, Name = "Десерти", Description = "Свіжа випічка" };

        Categories.TryAdd(coffee.Id, coffee);
        Categories.TryAdd(dessert.Id, dessert);

        var espresso = new MenuItem { Id = ++_menuItemIdCounter, Name = "Еспресо", Description = "Подвійний шот", Price = 55.0m, VolumeMl = 30, CategoryId = coffee.Id };
        var cappuccino = new MenuItem { Id = ++_menuItemIdCounter, Name = "Капучино", Description = "Збалансований вершково-кавовий смак із виразною текстурою щільної шовковистої піни.", Price = 75.0m, VolumeMl = 250, CategoryId = coffee.Id };
        var latte = new MenuItem { Id = ++_menuItemIdCounter, Name = "Лате", Description = "М’який, солодкуватий молочний смак, де кавова нота виступає лише фоном", Price = 80.0m, VolumeMl = 250, CategoryId = coffee.Id };
        var americano = new MenuItem { Id = ++_menuItemIdCounter, Name = "Американо", Description = "М’який смак без густої текстури, зберігає аромат еспресо, але має меншу концентрацію.", Price = 80.0m, VolumeMl = 250, CategoryId = coffee.Id };
        var flat_white = new MenuItem { Id = ++_menuItemIdCounter, Name = "Флет вайт", Description = "Інтенсивний, виразно кавовий напій із гладкою текстурою молока, міцніший за капучино.", Price = 80.0m, VolumeMl = 250, CategoryId = coffee.Id };
        var raf = new MenuItem { Id = ++_menuItemIdCounter, Name = "Раф", Description = "Десертний, однорідний напій із текстурою талого пломбіру та солодким ванільним посмаком.", Price = 80.0m, VolumeMl = 250, CategoryId = coffee.Id };


        MenuItems.TryAdd(espresso.Id, espresso);
        MenuItems.TryAdd(cappuccino.Id, cappuccino);
        MenuItems.TryAdd(latte.Id, latte);
        MenuItems.TryAdd(americano.Id, americano);
        MenuItems.TryAdd(flat_white.Id, flat_white);
        MenuItems.TryAdd(raf.Id, raf);
    }
    public int GetNextCategoryId() => Interlocked.Increment(ref _categoryIdCounter);
    public int GetNextMenuItemId() => Interlocked.Increment(ref _menuItemIdCounter);

}
