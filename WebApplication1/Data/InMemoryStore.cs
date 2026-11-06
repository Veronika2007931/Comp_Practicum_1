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

        var cheescake = new MenuItem { Id = ++_menuItemIdCounter, Name = "Карамельний чизкейк", Description = "Пісочна основа сабле, запечений крем-сир, шар тягучої солоної карамелі.", Price = 180m, VolumeMl = 180, CategoryId = dessert.Id };
        var meringue_roulade = new MenuItem { Id = ++_menuItemIdCounter, Name = "Меренговий рулет", Description = "Хрустка зовні й ніжна всередині меренга, крем на основі маскарпоне або вершків, ягідна начинка.", Price = 230m, VolumeMl = 150, CategoryId = dessert.Id };
        var apple_charlotte = new MenuItem { Id = ++_menuItemIdCounter, Name = "Шарлотка з яблуками", Description = "Класичний бісквіт на яйцях і борошні з великою кількістю карамелізованих або свіжих яблук, кориця.", Price = 190m, VolumeMl = 150, CategoryId = dessert.Id };
        var cherry_strudel = new MenuItem { Id = ++_menuItemIdCounter, Name = "Вишневий штрудель", Description = "Тонке витяжне тісто , соковита вишня, кориця, мигдальні пластівці або крихта.", Price = 175m, VolumeMl = 140, CategoryId = dessert.Id };
        var brownie = new MenuItem { Id = ++_menuItemIdCounter, Name = "Брауні", Description = "Темний шоколад , вершкове масло, какао, яйця, мінімум борошна.", Price = 150m, VolumeMl = 120, CategoryId = dessert.Id };
        var snickers_trifle = new MenuItem { Id = ++_menuItemIdCounter, Name = "Трайфл «Снікерс»", Description = "Шоколадний бісквіт, солона карамель, арахісовий крем-чіз і обсмажений арахіс у порційному стаканчику.", Price = 165m, VolumeMl = 180, CategoryId = dessert.Id };
        var medovyk = new MenuItem { Id = ++_menuItemIdCounter, Name = "Медовик", Description = "Тонкі заварні медові коржі, просочені легким сметанним або сметанно-вершковим кремом.", Price = 170m, VolumeMl = 160, CategoryId = dessert.Id };
        var napoleon = new MenuItem { Id = ++_menuItemIdCounter, Name = "Наполеон", Description = "Безліч тонких листкових коржів, ніжний ванільний крем дипломат або класичний заварний патісьєр.", Price = 155m, VolumeMl = 140, CategoryId = dessert.Id };


        MenuItems.TryAdd(espresso.Id, espresso);
        MenuItems.TryAdd(cappuccino.Id, cappuccino);
        MenuItems.TryAdd(latte.Id, latte);
        MenuItems.TryAdd(americano.Id, americano);
        MenuItems.TryAdd(flat_white.Id, flat_white);
        MenuItems.TryAdd(raf.Id, raf);

        MenuItems.TryAdd(cheescake.Id, cheescake);
        MenuItems.TryAdd(meringue_roulade.Id, meringue_roulade);
        MenuItems.TryAdd(apple_charlotte.Id, apple_charlotte);
        MenuItems.TryAdd(cherry_strudel.Id, cherry_strudel);
        MenuItems.TryAdd(brownie.Id, brownie);
        MenuItems.TryAdd(snickers_trifle.Id, snickers_trifle);
        MenuItems.TryAdd(medovyk.Id, medovyk);
        MenuItems.TryAdd(napoleon.Id, napoleon);
    }
    public int GetNextCategoryId() => Interlocked.Increment(ref _categoryIdCounter);
    public int GetNextMenuItemId() => Interlocked.Increment(ref _menuItemIdCounter);

}
