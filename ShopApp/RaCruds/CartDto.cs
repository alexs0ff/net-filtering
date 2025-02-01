namespace ShopApp.RaCruds;

public sealed record class CartDto
{
    public Guid Id { get; set; }
    public string PromoCode { get; set; }

    public string UserName { get; set; }

    public decimal Total { get; set; }

    public OrderDto Order { get; set; }

    public ICollection<CartItemDto> CartItems { get; set; } = new List<CartItemDto>();
}
