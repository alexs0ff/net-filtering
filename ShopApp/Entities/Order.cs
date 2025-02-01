namespace ShopApp.Entities;

public class Order
{
    public Guid Id { get; set; }
    public Guid CartId { get; set; }
    public Cart Cart { get; set; } = default!;
    public OrderStatus Status { get; set; }
}