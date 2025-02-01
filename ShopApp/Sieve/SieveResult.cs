namespace ShopApp.Sieve;

public class SieveResult<T>
{
    public IReadOnlyCollection<T> Items { get; set; }

    public int Count { get; set; }
}