using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ShopApp.Entities;

public class ShopAppContext : DbContext
{
    public ShopAppContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<CartItem> CartItems { get; set; } = null!;
    public DbSet<Cart> Carts { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Cart>(cfg =>
        {
            cfg.HasKey(i => i.Id);
            cfg.Property(i => i.Total).IsRequired();
            cfg.Property(i => i.OrderId);
            cfg.Property(i => i.PromoCode).HasMaxLength(200);
            cfg.HasOne(i => i.Order).WithOne(i => i.Cart)
                .HasForeignKey<Cart>(i => i.OrderId);
            cfg.HasMany(i => i.CartItems).WithOne(i => i.Cart).HasForeignKey(i => i.CartId);

            cfg.HasData(new[]
            {
                new Cart
                {
                    Id = new Guid("F1AB33BE-CD77-4904-9859-F8714C69BD34"),
                    PromoCode = "Sale1",
                    Total = 50.00M,
                    UserName = "petrov2"
                },
                new Cart
                {
                    Id = new Guid("870E2F1E-E06F-4877-B90F-3E33E8B7ED83"),
                    PromoCode = "Promo_Test1",
                    Total = 123.11M,
                    UserName = "ivan1",
                    OrderId = new Guid("6DC7864F-1CDF-4458-AEC3-521BD5A8336C")
                }

                ,
                new Cart
                {
                    Id = new Guid("BB0A0D79-79CD-474A-BBEC-A3AC899827A2"),
                    PromoCode = "J1N",
                    Total = 80M,
                    UserName = "pet2",
                    OrderId = new Guid("3F46A3A1-7D10-48CA-B7F7-D1EE1A000358")
                },
                new Cart
                {
                    Id = new Guid("F171504B-FCE1-46D8-A44B-6E7F545A875E"),
                    PromoCode = "Promo_Test1",
                    Total = 90M,
                    UserName = "ivanov3",
                    OrderId = new Guid("4F7E9FE7-B11D-44F1-AC66-2B2F8637C328")
                },
                new Cart
                {
                    Id = new Guid("C88DF46F-8924-4CAB-8D4D-9F67299AAF3D"),
                    PromoCode = "Free",
                    Total = 90M,
                    UserName = "dog1",
                    OrderId = new Guid("47F5BB79-4112-4E6A-9336-2F9D35A1B06D")
                },
                new Cart//
                {
                    Id = new Guid("59C2FA95-4EF3-4299-8468-B4C43F9CFD72"),
                    PromoCode = "Promo_Test1",
                    Total = 100M,
                    UserName = "dog1",
                    OrderId = new Guid("271313AB-BACE-4132-8564-F7FAF9E64BB4")
                },
                new Cart
                {
                    Id = new Guid("E87D21D1-96DC-436E-92A6-283884AF589E"),
                    PromoCode = "Free",
                    Total = 100M,
                    UserName = "ivan2",
                    OrderId = new Guid("5956302C-6AA6-43DE-AFD1-0B527473F933")
                },
                new Cart
                {
                    Id = new Guid("929644B5-66D9-48D2-8955-B10F64C72961"),
                    PromoCode = "Next",
                    Total = 60M,
                    UserName = "petrov3",
                    OrderId = new Guid("EC40D1D6-0B96-431A-98DE-58AB42FF5488")
                },
                new Cart
                {
                    Id = new Guid("7E04BC7B-D205-4F9B-A21B-D71A241DB55C"),
                    PromoCode = "Last",
                    Total = 70M,
                    UserName = "ivan2",
                    OrderId = new Guid("E40E3199-6E85-405A-B0D7-6BEA67BF8AFD")
                }
            });

        });

        modelBuilder.Entity<Order>(cfg =>
        {
            cfg.HasKey(i => i.Id);
            cfg.Property(i => i.CartId).IsRequired();
            cfg.Property(i => i.Status).HasMaxLength(32).HasConversion(new EnumToStringConverter<OrderStatus>())
                .IsRequired();
            cfg.HasOne(i => i.Cart).WithOne(i => i.Order)
                .HasForeignKey<Order>(i => i.CartId);
            cfg.HasData(new[]
            {
                new Order()
                {
                    Id = new Guid("6DC7864F-1CDF-4458-AEC3-521BD5A8336C"),
                    CartId = new Guid("870E2F1E-E06F-4877-B90F-3E33E8B7ED83"),
                    Status = OrderStatus.Init
                }
                ,
                new Order()
                {
                    Id = new Guid("3F46A3A1-7D10-48CA-B7F7-D1EE1A000358"),
                    CartId = new Guid("BB0A0D79-79CD-474A-BBEC-A3AC899827A2"),
                    Status = OrderStatus.Paid
                },

                new Order()
                {
                    Id = new Guid("4F7E9FE7-B11D-44F1-AC66-2B2F8637C328"),
                    CartId = new Guid("F171504B-FCE1-46D8-A44B-6E7F545A875E"),
                    Status = OrderStatus.Refunded
                },
                    
                new Order()
                {
                    Id = new Guid("47F5BB79-4112-4E6A-9336-2F9D35A1B06D"),
                    CartId = new Guid("C88DF46F-8924-4CAB-8D4D-9F67299AAF3D"),
                    Status = OrderStatus.Paid
                },

                new Order()
                {
                    Id = new Guid("271313AB-BACE-4132-8564-F7FAF9E64BB4"),
                    CartId = new Guid("59C2FA95-4EF3-4299-8468-B4C43F9CFD72"),
                    Status = OrderStatus.Fail
                },
                    
                new Order()
                {
                    Id = new Guid("5956302C-6AA6-43DE-AFD1-0B527473F933"),
                    CartId = new Guid("E87D21D1-96DC-436E-92A6-283884AF589E"),
                    Status = OrderStatus.Init
                },

                new Order()
                {
                    Id = new Guid("EC40D1D6-0B96-431A-98DE-58AB42FF5488"),
                    CartId = new Guid("929644B5-66D9-48D2-8955-B10F64C72961"),
                    Status = OrderStatus.Init
                },

                new Order()
                {
                    Id = new Guid("E40E3199-6E85-405A-B0D7-6BEA67BF8AFD"),
                    CartId = new Guid("7E04BC7B-D205-4F9B-A21B-D71A241DB55C"),
                    Status = OrderStatus.Refunded
                }
            });
        });

        modelBuilder.Entity<CartItem>(cfg =>
        {
            cfg.HasKey(i => i.Id);
            cfg.Property(i => i.Name).IsRequired();
            cfg.Property(i => i.Count).IsRequired();
            cfg.Property(i => i.Price).IsRequired();
            cfg.Property(i => i.CartId).IsRequired();

            cfg.HasData(new[]
            {
                new CartItem
                {
                    Id = new Guid("A8CD310D-1E52-44E1-B6B7-478A682C80CB"),
                    Count = 5,
                    Name = "Ластик",
                    CartId = new Guid("F1AB33BE-CD77-4904-9859-F8714C69BD34"),
                    Price = 10.00M
                },
                new CartItem
                {
                    Id = new Guid("FD2EFBD8-7969-4210-A283-760B6D63779B"),
                    Count = 1,
                    Name = "Карандаш",
                    CartId = new Guid("870E2F1E-E06F-4877-B90F-3E33E8B7ED83"),
                    Price = 23.11M
                },
                new CartItem
                {
                    Id = new Guid("7EB24925-B00F-4C90-9A78-F465BE9E3669"),
                    Count = 2,
                    Name = "Тетрадь",
                    CartId = new Guid("870E2F1E-E06F-4877-B90F-3E33E8B7ED83"),
                    Price = 50.00M
                },
                new CartItem
                {
                    Id = new Guid("6F7A011A-003E-48E2-9C32-4643F238D8B7"),
                    Count = 1,
                    Name = "Стол",
                    CartId = new Guid("BB0A0D79-79CD-474A-BBEC-A3AC899827A2"),
                    Price = 80.00M
                },
                new CartItem
                {
                    Id = new Guid("6B4FE1AE-E223-422D-A0DD-1D9782AC7548"),
                    Count = 1,
                    Name = "Тетрадь",
                    CartId = new Guid("F171504B-FCE1-46D8-A44B-6E7F545A875E"),
                    Price = 90.00M
                },
                new CartItem
                {
                    Id = new Guid("73C2742D-2201-431D-8197-62BB7E357495"),
                    Count = 1,
                    Name = "Линейка",
                    CartId = new Guid("C88DF46F-8924-4CAB-8D4D-9F67299AAF3D"),
                    Price = 90.00M
                },
                new CartItem
                {
                    Id = new Guid("8412671E-997C-4214-A53E-9B266DED9158"),
                    Count = 1,
                    Name = "Пластилин",
                    CartId = new Guid("E87D21D1-96DC-436E-92A6-283884AF589E"),
                    Price = 50.00M
                },
                new CartItem
                {
                    Id = new Guid("7B41DA5A-0738-49C0-8DB0-AC81C712C860"),
                    Count = 1,
                    Name = "Стул",
                    CartId = new Guid("E87D21D1-96DC-436E-92A6-283884AF589E"),
                    Price = 50.00M
                },
                new CartItem
                {
                    Id = new Guid("9839E221-F9E5-4A2A-8D7F-29DC4C5D594E"),
                    Count = 1,
                    Name = "Атлас",
                    CartId = new Guid("929644B5-66D9-48D2-8955-B10F64C72961"),
                    Price = 60.00M
                },
                new CartItem
                {
                    Id = new Guid("B2A91A99-6CF6-48ED-A281-F0001299E84A"),
                    Count = 1,
                    Name = "Карандаш",
                    CartId = new Guid("7E04BC7B-D205-4F9B-A21B-D71A241DB55C"),
                    Price = 20.00M
                },
                new CartItem
                {
                    Id = new Guid("4092421E-2500-430E-B1CD-95139A9B2121"),
                    Count = 1,
                    Name = "Пластилин",
                    CartId = new Guid("7E04BC7B-D205-4F9B-A21B-D71A241DB55C"),
                    Price = 50.00M
                },
                new CartItem
                {
                    Id = new Guid("5528C8C3-CFA5-4FB7-9CDF-2D287A444435"),
                    Count = 1,
                    Name = "Комод",
                    CartId = new Guid("59C2FA95-4EF3-4299-8468-B4C43F9CFD72"),
                    Price = 100.00M
                }
            });
        });
    }
}