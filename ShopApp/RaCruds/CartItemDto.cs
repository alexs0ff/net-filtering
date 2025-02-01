namespace ShopApp.RaCruds;

public sealed record class CartItemDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Count { get; set; }
    public decimal Price { get; set; }
}
