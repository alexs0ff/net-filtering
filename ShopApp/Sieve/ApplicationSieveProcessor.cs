using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using ShopApp.Entities;
using Sieve.Models;
using Sieve.Services;

namespace ShopApp.Sieve;

public class ApplicationSieveProcessor : SieveProcessor
{
    public ApplicationSieveProcessor(
        IOptions<SieveOptions> options,
        ISieveCustomSortMethods customSortMethods,
        ISieveCustomFilterMethods customFilterMethods)
        : base(options, customSortMethods, customFilterMethods)
    {
    }

    protected override SievePropertyMapper MapProperties(SievePropertyMapper mapper)
    {
        mapper.Property<Cart>(p => p.Id)
            .CanFilter();

        mapper.Property<Cart>(p => p.Total)
            .CanFilter()
            .CanSort();

        mapper.Property<Cart>(p => p.PromoCode)
            .CanFilter()
            .CanSort();

        mapper.Property<Cart>(p => p.UserName)
            .CanFilter()
            .CanSort();

        mapper.Property<Cart>(p => p.Order!.Status)
            .CanFilter();

        return mapper;
    }
}