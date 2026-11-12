namespace CoffeeShopApi.DTOs;

public class MenuItemDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int VolumeMl { get; set; }
    public int CategoryId { get; set; }
    public string? CategoryName { get; set; }
}

public class CreateUpdateMenuItemDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int VolumeMl { get; set; }
    public int CategoryId { get; set; }
}