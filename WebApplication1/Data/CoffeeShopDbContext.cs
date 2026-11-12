using System.Security.Cryptography.X509Certificates;
using CoffeeShopApi.Controllers;
using CoffeeShopApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShopApi.Data;

public class CoffeeShopDbContext : DbContext
{
    public CoffeeShopDbContext(DbContextOptions<CoffeeShopDbContext> options) : base(options)
    { }
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<MenuItem> MenuItem => Set<MenuItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<MenuItem>()
        .HasOne(m => m.Category)
        .WithMany(c => c.MenuItems)
        .HasForeignKey(m => m.CategoryId)
        .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Кава", Description = "Класичні та авторські кавові напої" },
            new Category { Id = 2, Name = "Десерти", Description = "Свіжа випічка та солодощі до кави" }
        );

        modelBuilder.Entity<MenuItem>().HasData(

            new MenuItem { Id = 1, Name = "Еспресо", Description = "Подвійний шот", Price = 55.0m, VolumeMl = 30, CategoryId = 1 },
            new MenuItem { Id = 2, Name = "Капучино", Description = "Збалансований вершково-кавовий смак із виразною текстурою щільної шовковистої піни.", Price = 75.0m, VolumeMl = 250, CategoryId = 1 },
            new MenuItem { Id = 3, Name = "Лате", Description = "М’який, солодкуватий молочний смак, де кавова нота виступає лише фоном", Price = 80.0m, VolumeMl = 250, CategoryId = 1 },
            new MenuItem { Id = 4, Name = "Американо", Description = "М’який смак без густої текстури, зберігає аромат еспресо, але має меншу концентрацію.", Price = 80.0m, VolumeMl = 250, CategoryId = 1 },
            new MenuItem { Id = 5, Name = "Флет вайт", Description = "Інтенсивний, виразно кавовий напій із гладкою текстурою молока, міцніший за капучино.", Price = 80.0m, VolumeMl = 250, CategoryId = 1 },
            new MenuItem { Id = 6, Name = "Раф", Description = "Десертний, однорідний напій із текстурою талого пломбіру та солодким ванільним посмаком.", Price = 80.0m, VolumeMl = 250, CategoryId = 1 },


            new MenuItem { Id = 7, Name = "Карамельний чизкейк", Description = "Пісочна основа сабле, запечений крем-сир, шар тягучої солоної карамелі.", Price = 180m, VolumeMl = 180, CategoryId = 2 },
            new MenuItem { Id = 8, Name = "Меренговий рулет", Description = "Хрустка зовні й ніжна всередині меренга, крем на основі маскарпоне або вершків, ягідна начинка.", Price = 230m, VolumeMl = 150, CategoryId = 2 },
            new MenuItem { Id = 9, Name = "Шарлотка з яблуками", Description = "Класичний бісквіт на яйцях і борошні з великою кількістю карамелізованих або свіжих яблук, кориця.", Price = 190m, VolumeMl = 150, CategoryId = 2 },
            new MenuItem { Id = 10, Name = "Вишневий штрудель", Description = "Тонке витяжне тісто, соковита вишня, кориця, мигдальні пластівці або крихта.", Price = 175m, VolumeMl = 140, CategoryId = 2 },
            new MenuItem { Id = 11, Name = "Брауні", Description = "Темний шоколад, вершкове масло, какао, яйця, мінімум борошна.", Price = 150m, VolumeMl = 120, CategoryId = 2 },
            new MenuItem { Id = 12, Name = "Трайфл «Снікерс»", Description = "Шоколадний бісквіт, солона карамель, арахісовий крем-чіз і обсмажений арахіс у порційному стаканчику.", Price = 165m, VolumeMl = 180, CategoryId = 2 },
            new MenuItem { Id = 13, Name = "Медовик", Description = "Тонкі заварні медові коржі, просочені легким сметанним або сметанно-вершковим кремом.", Price = 170m, VolumeMl = 160, CategoryId = 2 },
            new MenuItem { Id = 14, Name = "Наполеон", Description = "Безліч тонких листкових коржів, ніжний ванільний крем дипломат або класичний заварний патісьєр.", Price = 155m, VolumeMl = 140, CategoryId = 2 }
        );
    }


}