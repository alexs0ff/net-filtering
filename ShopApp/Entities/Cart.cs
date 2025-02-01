namespace ShopApp.Entities;

public class Cart
{
    public Guid Id { get; set; }
    public string PromoCode { get; set; } = default!;
    public string UserName { get; set; } = default!;
    public decimal Total { get; set; }
    public Guid? OrderId { get; set; }
    public Order? Order { get; set; }
    public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
}