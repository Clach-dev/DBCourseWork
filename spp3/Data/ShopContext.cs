using API.Data.Models;
using Microsoft.EntityFrameworkCore;
using spp3.Data.Models;

namespace spp3.Data
{
    public partial class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> options) : base(options) { }

        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<CommercialOrganization> CommercialOrganizations { get; set; }
        public DbSet<TradeOutlet> TradeOutlets { get; set; }
        public DbSet<OutletSection> OutletSections { get; set; }
        public DbSet<SectionManager> SectionManagers { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Deal> Deals { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<BonusCard> BonusCards { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductToOrder> ProductsToOrders { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderToSupplier> OrdersToSuppliers { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        public ShopContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(ur => ur.urId);

                entity.Property(ur => ur.role).IsRequired(true);

                entity.HasMany(ur => ur.Users).WithOne(us => us.UserRole).HasForeignKey(us => us.urId).IsRequired(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(us => us.usId);

                entity.Property(us => us.login).IsRequired(true);
                entity.Property(us => us.password).IsRequired(true);

                entity.HasOne(us => us.UserRole).WithMany(ur => ur.Users).IsRequired(false);

            });


            modelBuilder.Entity<CommercialOrganization>(entity =>
            {
                entity.HasKey(co => co.coId);

                entity.Property(co => co.organizationName).IsRequired(true);

                entity.HasMany(co => co.TradeOutlets).WithOne(to => to.CommercialOrganization).HasForeignKey(to => to.coId).IsRequired(false); ;
            });

            modelBuilder.Entity<TradeOutlet>(entity =>
            {
                entity.HasKey(to => to.toId);

                entity.Property(to => to.outletName).IsRequired(true);
                entity.Property(to => to.outletType).IsRequired(false);
                entity.Property(to => to.size).IsRequired(false);
                entity.Property(to => to.rent).IsRequired(false);
                entity.Property(to => to.counters).IsRequired(false);

                entity.HasOne(to => to.CommercialOrganization).WithMany(co => co.TradeOutlets).IsRequired(true);
                entity.HasMany(to => to.OutletSections).WithOne(os => os.TradeOutlet).HasForeignKey(os => os.toId).IsRequired(false);
            });

            modelBuilder.Entity<OutletSection>(entity =>
            {
                entity.HasKey(os => os.osId);

                entity.Property(os => os.sectionName).IsRequired(true);
                entity.Property(os => os.sectionFloor).IsRequired(false);
                entity.Property(os => os.sectionHall).IsRequired(false);

                entity.HasOne(os => os.TradeOutlet).WithMany(to => to.OutletSections).IsRequired(true);
                entity.HasOne(os => os.SectionManager).WithOne(sm => sm.OutletSection).HasForeignKey<SectionManager>(sm => sm.osId).IsRequired(false);
                entity.HasMany(os => os.Sellers).WithOne(sel => sel.OutletSection).HasForeignKey(sel => sel.osId).IsRequired(false);
            });

            modelBuilder.Entity<SectionManager>(entity =>
            {
                entity.HasKey(sm => sm.smId);

                entity.Property(sm => sm.firstName).IsRequired(true);
                entity.Property(sm => sm.secondName).IsRequired(true);
                entity.Property(sm => sm.patrynomic).IsRequired(false);
                entity.Property(sm => sm.salary).IsRequired(false);
                entity.Property(sm => sm.phoneNumber).IsRequired(true);
                entity.Property(sm => sm.experience).IsRequired(false);

                entity.HasOne(sm => sm.OutletSection).WithOne(os => os.SectionManager).IsRequired(true);
            });

            modelBuilder.Entity<Seller>(entity =>
            {
                entity.HasKey(sel => sel.selId);

                entity.Property(sel => sel.firstName).IsRequired(true);
                entity.Property(sel => sel.secondName).IsRequired(true);
                entity.Property(sel => sel.patrynomic).IsRequired(false);
                entity.Property(sel => sel.salary).IsRequired(true);
                entity.Property(sm => sm.phoneNumber).IsRequired(true);
                entity.Property(sel => sel.endOfContract).IsRequired(false);

                entity.HasOne(sel => sel.OutletSection).WithMany(os => os.Sellers).IsRequired(true);
                entity.HasMany(sel => sel.Deals).WithOne(dl => dl.Seller).HasForeignKey(dl => dl.selId).IsRequired(false);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(cu => cu.cuId);

                entity.Property(cu => cu.firstName).IsRequired(true);
                entity.Property(cu => cu.secondName).IsRequired(true);
                entity.Property(cu => cu.patrynomic).IsRequired(false);
                entity.Property(cu => cu.age).IsRequired(false);
                entity.Property(cu => cu.gender).IsRequired(false);
                entity.Property(cu => cu.adress).IsRequired(false);
                entity.Property(cu => cu.phoneNumber).IsRequired(true);

                entity.HasOne(cu => cu.BonusCard).WithOne(bc => bc.Customer).HasForeignKey<BonusCard>(bc => bc.cuId).IsRequired(false);
                entity.HasMany(cu => cu.Deals).WithOne(dl => dl.Customer).HasForeignKey(dl => dl.cuId).IsRequired(false);
            });

            modelBuilder.Entity<BonusCard>(entity =>
            {
                entity.HasKey(bc => bc.bcId);

                entity.Property(bc => bc.number).IsRequired(false);
                entity.Property(bc => bc.discount).IsRequired(true);

                entity.HasOne(bc => bc.Customer).WithOne(cu => cu.BonusCard).IsRequired(true);
            });

            modelBuilder.Entity<ProductType>(entity =>
            {
                entity.HasKey(pt => pt.ptId);

                entity.Property(pt => pt.name).IsRequired(true);
                entity.Property(pt => pt.ageLimit).IsRequired(false);

                entity.HasMany(pt => pt.Products).WithOne(pr => pr.ProductType).HasForeignKey(pr => pr.ptId).IsRequired(false);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(pr => pr.prId);

                entity.Property(pr => pr.name).IsRequired(true);
                entity.Property(pr => pr.quantity).IsRequired(false);
                entity.Property(pr => pr.price).IsRequired(false);

                entity.HasOne(pr => pr.ProductType).WithMany(pt => pt.Products);
                entity.HasMany(pr => pr.Deals).WithOne(dl => dl.Product).HasForeignKey(dl => dl.prId).IsRequired(false);
            });

            modelBuilder.Entity<ProductToOrder>(entity =>
            {
                entity.HasKey(pto => new { pto.orId, pto.prId });

                entity.HasOne(pto => pto.Order)
                      .WithMany(or => or.ProductsToOrders)
                      .HasForeignKey(pto => pto.orId);

                entity.HasOne(pto => pto.Product)
                      .WithMany(pr => pr.ProductsToOrders)
                      .HasForeignKey(pto => pto.prId);
            });

            modelBuilder.Entity<Deal>(entity =>
            {
                entity.HasKey(dl => dl.dlId);

                entity.Property(dl => dl.dealMoment).IsRequired(true);

                entity.HasOne(dl => dl.Customer).WithMany(cu => cu.Deals).IsRequired(true);
                entity.HasOne(dl => dl.Seller).WithMany(sel => sel.Deals).IsRequired(true);
                entity.HasOne(dl => dl.Product).WithMany(pr => pr.Deals).IsRequired(true);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(or => or.orId);

                entity.Property(or => or.orderNumber).IsRequired(true);
                entity.Property(or => or.orderStatus).IsRequired(true);
                entity.Property(or => or.quantity).IsRequired(true);

            });

            modelBuilder.Entity<OrderToSupplier>(entity =>
            {
                entity.HasKey(ots => new { ots.orId, ots.suId });

                entity.HasOne(ots => ots.Order)
                      .WithMany(or => or.OrdersToSuppliers)
                      .HasForeignKey(ots => ots.orId);

                entity.HasOne(ots => ots.Supplier)
                      .WithMany(su => su.OrdersToSuppliers)
                      .HasForeignKey(ots => ots.suId);
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.HasKey(su => su.suId);

                entity.Property(su => su.supplierName).IsRequired(true);
                entity.Property(su => su.price).IsRequired(true);
                entity.Property(su => su.quantity).IsRequired(true);
            });

            AddDbRows<UserRole>(modelBuilder,
                new UserRole { urId = 1, role = "Admin" },
                new UserRole { urId = 2, role = "Moder" }
                );

            AddDbRows<User>(modelBuilder,
                new User { usId = 1, login = "Admin", password = "1234", urId = 1 }
                );

            AddDbRows<CommercialOrganization>(modelBuilder,
                new CommercialOrganization { coId = 1, organizationName = "Dionis" },
                new CommercialOrganization { coId = 2, organizationName = "У Кирилла на диване" },
                new CommercialOrganization { coId = 3, organizationName = "Подарки от Игоряо" },
                new CommercialOrganization { coId = 4, organizationName = "Сказочный Ромочка" }
                );

            AddDbRows<TradeOutlet>(modelBuilder,
                new TradeOutlet { toId = 1, outletName = "Овощи и Фрукты", outletType = "Продуктовый магазин", size = 24.4, rent = 15000, counters = 8, coId = 1 },
                new TradeOutlet { toId = 2, outletName = "Семейный дом", outletType = "Супермаркет", size = 100.2, rent = 25000, counters = 12, coId = 2 },
                new TradeOutlet { toId = 3, outletName = "Вкусные товары", outletType = "Продуктовый магазин", size = 69.4, rent = 10000, counters = 5, coId = 3 },
                new TradeOutlet { toId = 4, outletName = "Фруктовая Гавань", outletType = "Минимаркет", size = 50.5, rent = 20000, counters = 10, coId = 4 },
                new TradeOutlet { toId = 5, outletName = "Магазин 'Здоровая пища'", outletType = "Продуктовый магазин", size = 43.4, rent = 17000, counters = 7, coId = 1 },
                new TradeOutlet { toId = 6, outletName = "Уголок радости", outletType = "Минимаркет", size = 23.1, rent = 12000, counters = 6, coId = 2 },
                new TradeOutlet { toId = 7, outletName = "Лавка сказок", outletType = "Продуктовый магазин", size = 110.3, rent = 30000, counters = 15, coId = 3 },
                new TradeOutlet { toId = 8, outletName = "Фруктовый оазис", outletType = "Супермаркет", size = 124.5, rent = 28000, counters = 14, coId = 4 },
                new TradeOutlet { toId = 9, outletName = "Домашний уют", outletType = "Продуктовый магазин", size = 75.1, rent = 18000, counters = 9, coId = 1 },
                new TradeOutlet { toId = 10, outletName = "Супермаркет 'Домашний рай'", outletType = "Супермаркет", size = 220.2, rent = 50000, counters = 20, coId = 2 },
                new TradeOutlet { toId = 11, outletName = "Фруктовый сад", outletType = "Продуктовый магазин", size = 99.9, rent = 22000, counters = 11, coId = 3 },
                new TradeOutlet { toId = 12, outletName = "Маленький рай", outletType = "Минимаркет", size = 34.3, rent = 13000, counters = 4, coId = 4 },
                new TradeOutlet { toId = 13, outletName = "Продуктовая сказка", outletType = "Супермаркет", size = 64.4, rent = 19000, counters = 8, coId = 1 },
                new TradeOutlet { toId = 14, outletName = "Фруктовый рай", outletType = "Продуктовый магазин", size = 111.1, rent = 24000, counters = 12, coId = 2 },
                new TradeOutlet { toId = 15, outletName = "Магазин 'Все вкусно'", outletType = "Минимаркет", size = 15.5, rent = 11000, counters = 5, coId = 3 }
                );
            AddDbRows<OutletSection>(modelBuilder,
                new OutletSection { osId = 1, sectionName = "Фрукты и Овощи", sectionFloor = 1, sectionHall = "Основной", toId = 1 },
                new OutletSection { osId = 2, sectionName = "Мясо и Птица", sectionFloor = 1, sectionHall = "Основной", toId = 1 },
                new OutletSection { osId = 3, sectionName = "Кондитерские изделия", sectionFloor = 2, sectionHall = "Основной", toId = 2 },
                new OutletSection { osId = 4, sectionName = "Молочные продукты", sectionFloor = 2, sectionHall = "Основной", toId = 2 },
                new OutletSection { osId = 5, sectionName = "Зерновые продукты", sectionFloor = 1, sectionHall = "Основной", toId = 3 },
                new OutletSection { osId = 6, sectionName = "Напитки", sectionFloor = 1, sectionHall = "Основной", toId = 3 },
                new OutletSection { osId = 7, sectionName = "Кулинарные ингредиенты", sectionFloor = 1, sectionHall = "Основной", toId = 4 },
                new OutletSection { osId = 8, sectionName = "Хлебобулочные изделия", sectionFloor = 1, sectionHall = "Основной", toId = 4 },
                new OutletSection { osId = 9, sectionName = "Фрукты и Овощи", sectionFloor = 1, sectionHall = "Основной", toId = 5 },
                new OutletSection { osId = 10, sectionName = "Мясо и Птица", sectionFloor = 1, sectionHall = "Основной", toId = 5 },
                new OutletSection { osId = 11, sectionName = "Зерновые продукты", sectionFloor = 1, sectionHall = "Основной", toId = 6 },
                new OutletSection { osId = 12, sectionName = "Напитки", sectionFloor = 1, sectionHall = "Основной", toId = 6 },
                new OutletSection { osId = 13, sectionName = "Кулинарные ингредиенты", sectionFloor = 1, sectionHall = "Основной", toId = 7 },
                new OutletSection { osId = 14, sectionName = "Хлебобулочные изделия", sectionFloor = 1, sectionHall = "Основной", toId = 7 },
                new OutletSection { osId = 15, sectionName = "Фрукты и Овощи", sectionFloor = 1, sectionHall = "Основной", toId = 8 }
                );

            AddDbRows<SectionManager>(modelBuilder,
                new SectionManager { smId = 1, firstName = "Иван", secondName = "Петров", phoneNumber = "123456789", experience = 5, osId = 1 },
                new SectionManager { smId = 2, firstName = "Анна", secondName = "Иванова", phoneNumber = "987654321", experience = 7, osId = 2 },
                new SectionManager { smId = 3, firstName = "Михаил", secondName = "Сидоров", phoneNumber = "555666777", experience = 3, osId = 3 },
                new SectionManager { smId = 4, firstName = "Елена", secondName = "Александрова", phoneNumber = "444333222", experience = 8, osId = 4 },
                new SectionManager { smId = 5, firstName = "Сергей", secondName = "Васильев", phoneNumber = "111222333", experience = 6, osId = 5 },
                new SectionManager { smId = 6, firstName = "Мария", secondName = "Павлова", phoneNumber = "999888777", experience = 4, osId = 6 },
                new SectionManager { smId = 7, firstName = "Алексей", secondName = "Николаев", phoneNumber = "666777888", experience = 9, osId = 7 },
                new SectionManager { smId = 8, firstName = "Ольга", secondName = "Дмитриева", phoneNumber = "333444555", experience = 2, osId = 8 },
                new SectionManager { smId = 9, firstName = "Артем", secondName = "Егоров", phoneNumber = "222333444", experience = 7, osId = 9 },
                new SectionManager { smId = 10, firstName = "Екатерина", secondName = "Андреева", phoneNumber = "777888999", experience = 3, osId = 10 },
                new SectionManager { smId = 11, firstName = "Денис", secondName = "Семенов", phoneNumber = "888999000", experience = 5, osId = 11 },
                new SectionManager { smId = 12, firstName = "Алина", secondName = "Федорова", phoneNumber = "444555666", experience = 8, osId = 12 },
                new SectionManager { smId = 13, firstName = "Павел", secondName = "Иванов", phoneNumber = "111222333", experience = 6, osId = 13 },
                new SectionManager { smId = 14, firstName = "Виктория", secondName = "Смирнова", phoneNumber = "999888777", experience = 4, osId = 14 },
                new SectionManager { smId = 15, firstName = "Андрей", secondName = "Петров", phoneNumber = "666777888", experience = 9, osId = 15 }
                );
            AddDbRows<Seller>(modelBuilder,
                new Seller { selId = 1, firstName = "Алексей", secondName = "Сидоров", phoneNumber = "111222333", endOfContract = DateOnly.FromDateTime(DateTime.Now.AddYears(1)), osId = 1, salary = 400.12 },
                new Seller { selId = 2, firstName = "Елена", secondName = "Козлова", phoneNumber = "444555666", endOfContract = DateOnly.FromDateTime(DateTime.Now.AddYears(2)), osId = 2, salary = 500.12 },
                new Seller { selId = 3, firstName = "Дмитрий", secondName = "Иванов", phoneNumber = "777888999", endOfContract = DateOnly.FromDateTime(DateTime.Now.AddYears(3)), osId = 3, salary = 1900.12 },
                new Seller { selId = 4, firstName = "Ольга", secondName = "Петрова", phoneNumber = "888999000", endOfContract = DateOnly.FromDateTime(DateTime.Now.AddYears(4)), osId = 4, salary = 1580.12 },
                new Seller { selId = 5, firstName = "Ирина", secondName = "Смирнова", phoneNumber = "333444555", endOfContract = DateOnly.FromDateTime(DateTime.Now.AddYears(5)), osId = 1, salary = 1700.12 },
                new Seller { selId = 6, firstName = "Андрей", secondName = "Попов", phoneNumber = "666777888", endOfContract = DateOnly.FromDateTime(DateTime.Now.AddYears(6)), osId = 2, salary = 1430.12 },
                new Seller { selId = 7, firstName = "Марина", secondName = "Васильева", phoneNumber = "999000111", endOfContract = DateOnly.FromDateTime(DateTime.Now.AddDays(1)), osId = 3, salary = 1510.12 },
                new Seller { selId = 8, firstName = "Сергей", secondName = "Соколов", phoneNumber = "222333444", endOfContract = DateOnly.FromDateTime(DateTime.Now.AddYears(8)), osId = 4, salary = 1450.12 },
                new Seller { selId = 9, firstName = "Екатерина", secondName = "Зайцева", phoneNumber = "555666777", endOfContract = DateOnly.FromDateTime(DateTime.Now.AddYears(9)), osId = 1, salary = 1130.12 },
                new Seller { selId = 10, firstName = "Александр", secondName = "Андреев", phoneNumber = "888999000", endOfContract = DateOnly.FromDateTime(DateTime.Now.AddYears(10)), osId = 2, salary = 150.12 },
                new Seller { selId = 11, firstName = "Наталья", secondName = "Николаева", phoneNumber = "111222333", endOfContract = DateOnly.FromDateTime(DateTime.Now.AddYears(11)), osId = 3, salary = 15.12 },
                new Seller { selId = 12, firstName = "Михаил", secondName = "Михайлов", phoneNumber = "444555666", endOfContract = DateOnly.FromDateTime(DateTime.Now.AddYears(12)), osId = 4, salary = 50.12 },
                new Seller { selId = 13, firstName = "Елена", secondName = "Кузнецова", phoneNumber = "777888999", endOfContract = DateOnly.FromDateTime(DateTime.Now.AddYears(13)), osId = 1, salary = 100.12 },
                new Seller { selId = 14, firstName = "Анна", secondName = "Богданова", phoneNumber = "999000111", endOfContract = DateOnly.FromDateTime(DateTime.Now.AddYears(14)), osId = 2, salary = 130.12 },
                new Seller { selId = 15, firstName = "Игорь", secondName = "Григорьев", phoneNumber = "222333444", endOfContract = DateOnly.FromDateTime(DateTime.Now.AddYears(15)), osId = 3, salary = 500.12 }
                );


            AddDbRows<Customer>(modelBuilder,
                new Customer { cuId = 1, firstName = "Иван", secondName = "Иванов", phoneNumber = "111222333" },
                new Customer { cuId = 2, firstName = "Екатерина", secondName = "Петрова", phoneNumber = "444555666" },
                new Customer { cuId = 3, firstName = "Александр", secondName = "Сидоров", phoneNumber = "777888999" },
                new Customer { cuId = 4, firstName = "Елена", secondName = "Козлова", phoneNumber = "888999000" },
                new Customer { cuId = 5, firstName = "Анна", secondName = "Иванова", phoneNumber = "333444555" },
                new Customer { cuId = 6, firstName = "Дмитрий", secondName = "Петров", phoneNumber = "666777888" },
                new Customer { cuId = 7, firstName = "Мария", secondName = "Сидорова", phoneNumber = "999000111" },
                new Customer { cuId = 8, firstName = "Павел", secondName = "Козлов", phoneNumber = "222333444" },
                new Customer { cuId = 9, firstName = "Ольга", secondName = "Иванова", phoneNumber = "555666777" },
                new Customer { cuId = 10, firstName = "Сергей", secondName = "Петров", phoneNumber = "888999000" },
                new Customer { cuId = 11, firstName = "Наталья", secondName = "Сидорова", phoneNumber = "111222333" },
                new Customer { cuId = 12, firstName = "Ирина", secondName = "Козлова", phoneNumber = "444555666" },
                new Customer { cuId = 13, firstName = "Алексей", secondName = "Иванов", phoneNumber = "777888999" },
                new Customer { cuId = 14, firstName = "Екатерина", secondName = "Петрова", phoneNumber = "888999000" },
                new Customer { cuId = 15, firstName = "Андрей", secondName = "Сидоров", phoneNumber = "222333444" }
                );

            AddDbRows<BonusCard>(modelBuilder,
                new BonusCard { bcId = 1, number = "123456789", discount = 5, cuId = 1 },
                new BonusCard { bcId = 2, number = "987654321", discount = 10, cuId = 2 },
                new BonusCard { bcId = 3, number = "111222333", discount = 7, cuId = 3 },
                new BonusCard { bcId = 4, number = "444555666", discount = 15, cuId = 4 },
                new BonusCard { bcId = 5, number = "777888999", discount = 3, cuId = 5 },
                new BonusCard { bcId = 6, number = "888999000", discount = 8, cuId = 6 },
                new BonusCard { bcId = 7, number = "333444555", discount = 12, cuId = 7 },
                new BonusCard { bcId = 8, number = "666777888", discount = 6, cuId = 8 },
                new BonusCard { bcId = 9, number = "999000111", discount = 9, cuId = 9 },
                new BonusCard { bcId = 10, number = "222333444", discount = 4, cuId = 10 },
                new BonusCard { bcId = 11, number = "555666777", discount = 11, cuId = 11 },
                new BonusCard { bcId = 12, number = "888999000", discount = 13, cuId = 12 },
                new BonusCard { bcId = 13, number = "111222333", discount = 6, cuId = 13 },
                new BonusCard { bcId = 14, number = "444555666", discount = 8, cuId = 14 },
                new BonusCard { bcId = 15, number = "777888999", discount = 10, cuId = 15 }

                );

            AddDbRows<ProductType>(modelBuilder,
                new ProductType { ptId = 1, name = "Овощи", ageLimit = null },
                new ProductType { ptId = 2, name = "Фрукты", ageLimit = null },
                new ProductType { ptId = 3, name = "Молочные продукты", ageLimit = null },
                new ProductType { ptId = 4, name = "Мясо", ageLimit = null },
                new ProductType { ptId = 5, name = "Морепродукты", ageLimit = null },
                new ProductType { ptId = 6, name = "Замороженные продукты", ageLimit = null },
                new ProductType { ptId = 7, name = "Консервированные продукты", ageLimit = null },
                new ProductType { ptId = 8, name = "Хлебобулочные изделия", ageLimit = null },
                new ProductType { ptId = 9, name = "Напитки", ageLimit = null },
                new ProductType { ptId = 10, name = "Сладости", ageLimit = null },
                new ProductType { ptId = 11, name = "Специи и пряности", ageLimit = null },
                new ProductType { ptId = 12, name = "Кофе и чай", ageLimit = null },
                new ProductType { ptId = 13, name = "Здоровое питание", ageLimit = null },
                new ProductType { ptId = 14, name = "Детское питание", ageLimit = null },
                new ProductType { ptId = 15, name = "Бытовая химия", ageLimit = null }
                );

            AddDbRows<Product>(modelBuilder,
                new Product { prId = 1, name = "Помидоры", quantity = 100, price = 50, ptId = 1 },
                new Product { prId = 2, name = "Огурцы", quantity = 120, price = 40, ptId = 1 },
                new Product { prId = 3, name = "Яблоки", quantity = 150, price = 60, ptId = 2 },
                new Product { prId = 4, name = "Бананы", quantity = 200, price = 70, ptId = 2 },
                new Product { prId = 5, name = "Молоко", quantity = 50, price = 30, ptId = 3 },
                new Product { prId = 6, name = "Сыр", quantity = 80, price = 90, ptId = 3 },
                new Product { prId = 7, name = "Курица", quantity = 40, price = 100, ptId = 4 },
                new Product { prId = 8, name = "Говядина", quantity = 30, price = 150, ptId = 4 },
                new Product { prId = 9, name = "Креветки", quantity = 20, price = 200, ptId = 5 },
                new Product { prId = 10, name = "Лосось", quantity = 15, price = 250, ptId = 5 },
                new Product { prId = 11, name = "Горошек замороженный", quantity = 50, price = 40, ptId = 6 },
                new Product { prId = 12, name = "Фасоль замороженная", quantity = 30, price = 45, ptId = 6 },
                new Product { prId = 13, name = "Консервы горошек", quantity = 100, price = 35, ptId = 7 },
                new Product { prId = 14, name = "Консервы тунец", quantity = 80, price = 60, ptId = 7 },
                new Product { prId = 15, name = "Хлеб белый", quantity = 200, price = 25, ptId = 8 }
                );

            AddDbRows<Order>(modelBuilder,
                new Order { orId = 1, orderNumber = 001, orderStatus = "Pending", quantity = 10 },
                new Order { orId = 2, orderNumber = 002, orderStatus = "Processing", quantity = 20 },
                new Order { orId = 3, orderNumber = 003, orderStatus = "Pending", quantity = 15 },
                new Order { orId = 4, orderNumber = 004, orderStatus = "Processing", quantity = 30 },
                new Order { orId = 5, orderNumber = 005, orderStatus = "Shipped", quantity = 25 },
                new Order { orId = 6, orderNumber = 006, orderStatus = "Delivered", quantity = 40 },
                new Order { orId = 7, orderNumber = 007, orderStatus = "Pending", quantity = 12 },
                new Order { orId = 8, orderNumber = 008, orderStatus = "Processing", quantity = 18 },
                new Order { orId = 9, orderNumber = 009, orderStatus = "Shipped", quantity = 22 },
                new Order { orId = 10, orderNumber = 010, orderStatus = "Delivered", quantity = 35 },
                new Order { orId = 11, orderNumber = 011, orderStatus = "Pending", quantity = 8 },
                new Order { orId = 12, orderNumber = 012, orderStatus = "Processing", quantity = 28 },
                new Order { orId = 13, orderNumber = 013, orderStatus = "Shipped", quantity = 17 },
                new Order { orId = 14, orderNumber = 014, orderStatus = "Delivered", quantity = 38 },
                new Order { orId = 15, orderNumber = 015, orderStatus = "Pending", quantity = 14 }
                );

            AddDbRows<Supplier>(modelBuilder,
                new Supplier { suId = 1, supplierName = "ООО 'Зеленые огороды'", price = 500, quantity = 100 },
                new Supplier { suId = 2, supplierName = "Фермерское хозяйство 'Солнечное поле'", price = 700, quantity = 80 },
                new Supplier { suId = 3, supplierName = "Птицефабрика 'Курочка Ряба'", price = 400, quantity = 120 },
                new Supplier { suId = 4, supplierName = "Мясной комбинат 'Большой кролик'", price = 600, quantity = 90 },
                new Supplier { suId = 5, supplierName = "Рыбопромысловый флот 'Северная волна'", price = 900, quantity = 70 },
                new Supplier { suId = 6, supplierName = "ООО 'Мороженое на любой вкус'", price = 300, quantity = 150 },
                new Supplier { suId = 7, supplierName = "Консервный завод 'Тройка'", price = 200, quantity = 200 },
                new Supplier { suId = 8, supplierName = "Хлебопекарня 'Золотой колос'", price = 150, quantity = 250 },
                new Supplier { suId = 9, supplierName = "Винодельня 'Урожайная долина'", price = 1000, quantity = 50 },
                new Supplier { suId = 10, supplierName = "Сладкий рай 'Кондитерская мечта'", price = 800, quantity = 60 },
                new Supplier { suId = 11, supplierName = "Пряносточный завод 'Восточные ароматы'", price = 50, quantity = 300 },
                new Supplier { suId = 12, supplierName = "Кофейная компания 'Ароматный мир'", price = 1200, quantity = 40 },
                new Supplier { suId = 13, supplierName = "Здоровое питание 'Органик'", price = 350, quantity = 180 },
                new Supplier { suId = 14, supplierName = "Детское питание 'Малышок'", price = 450, quantity = 130 },
                new Supplier { suId = 15, supplierName = "Магазин хозяйственных товаров 'Чистый дом'", price = 200, quantity = 220 }
                );


            AddDbRows<Deal>(modelBuilder,
                new Deal { dlId = 1, dealMoment = DateTime.Now.AddDays(-5), cuId = 1, selId = 1, prId = 1 },
                new Deal { dlId = 2, dealMoment = DateTime.Now.AddDays(-4), cuId = 2, selId = 2, prId = 2 },
                new Deal { dlId = 3, dealMoment = DateTime.Now.AddDays(-3), cuId = 3, selId = 3, prId = 3 },
                new Deal { dlId = 4, dealMoment = DateTime.Now.AddDays(-2), cuId = 4, selId = 4, prId = 4 },
                new Deal { dlId = 5, dealMoment = DateTime.Now.AddDays(-1), cuId = 5, selId = 5, prId = 5 },
                new Deal { dlId = 6, dealMoment = DateTime.Now, cuId = 6, selId = 6, prId = 6 },
                new Deal { dlId = 7, dealMoment = DateTime.Now, cuId = 7, selId = 7, prId = 7 },
                new Deal { dlId = 8, dealMoment = DateTime.Now, cuId = 8, selId = 8, prId = 8 },
                new Deal { dlId = 9, dealMoment = DateTime.Now, cuId = 9, selId = 9, prId = 9 },
                new Deal { dlId = 10, dealMoment = DateTime.Now, cuId = 10, selId = 10, prId = 10 },
                new Deal { dlId = 11, dealMoment = DateTime.Now, cuId = 11, selId = 11, prId = 11 },
                new Deal { dlId = 12, dealMoment = DateTime.Now, cuId = 12, selId = 12, prId = 12 },
                new Deal { dlId = 13, dealMoment = DateTime.Now, cuId = 13, selId = 13, prId = 13 },
                new Deal { dlId = 14, dealMoment = DateTime.Now, cuId = 14, selId = 14, prId = 14 },
                new Deal { dlId = 15, dealMoment = DateTime.Now, cuId = 15, selId = 15, prId = 15 }
                );

            AddDbRows<OrderToSupplier>(modelBuilder,
                new OrderToSupplier { orId = 1, suId = 1 },
                new OrderToSupplier { orId = 1, suId = 2 },
                new OrderToSupplier { orId = 2, suId = 3 },
                new OrderToSupplier { orId = 2, suId = 4 },
                new OrderToSupplier { orId = 3, suId = 5 },
                new OrderToSupplier { orId = 3, suId = 6 },
                new OrderToSupplier { orId = 4, suId = 7 },
                new OrderToSupplier { orId = 4, suId = 8 },
                new OrderToSupplier { orId = 5, suId = 9 },
                new OrderToSupplier { orId = 5, suId = 10 },
                new OrderToSupplier { orId = 6, suId = 11 },
                new OrderToSupplier { orId = 6, suId = 12 },
                new OrderToSupplier { orId = 7, suId = 13 },
                new OrderToSupplier { orId = 7, suId = 14 },
                new OrderToSupplier { orId = 8, suId = 15 }
                );
            AddDbRows<ProductToOrder>(modelBuilder,
                new ProductToOrder { orId = 1, prId = 1 },
                new ProductToOrder { orId = 1, prId = 2 },
                new ProductToOrder { orId = 2, prId = 3 },
                new ProductToOrder { orId = 2, prId = 4 },
                new ProductToOrder { orId = 3, prId = 5 },
                new ProductToOrder { orId = 3, prId = 6 },
                new ProductToOrder { orId = 4, prId = 7 },
                new ProductToOrder { orId = 4, prId = 8 },
                new ProductToOrder { orId = 5, prId = 9 },
                new ProductToOrder { orId = 5, prId = 10 },
                new ProductToOrder { orId = 6, prId = 11 },
                new ProductToOrder { orId = 6, prId = 12 },
                new ProductToOrder { orId = 7, prId = 13 },
                new ProductToOrder { orId = 7, prId = 14 },
                new ProductToOrder { orId = 8, prId = 15 }
                );

        }

        private static void AddDbRows<T>(ModelBuilder modelBuilder, params T[] entities) where T : class
        {
            modelBuilder.Entity<T>().HasData(entities);
        }
    }
}