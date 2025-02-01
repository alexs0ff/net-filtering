using ShopApp.Entities;

namespace ShopApp.RaCruds;

public sealed record class OrderDto
{
    public Guid Id { get; set; }

    public OrderStatus Status { get; set; }
}
