namespace ShopApp.Entities;

public class CartItem
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public int Count { get; set; }
    public decimal Price { get; set; }
    public Guid CartId { get; set; }
    public Cart Cart { get; set; } = default!;
}