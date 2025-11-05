using Microsoft.EntityFrameworkCore;

namespace MarketHup.Models.MVVM
{
    public class Context:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            var configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration["ConnectionStrings:ContextConnection"]);
        }
        public DbSet<Category>? Category { get; set; }
        public DbSet<Comment>? Comment { get; set; }
        public DbSet<Order>? Order { get; set; }
        public DbSet<Product>? Product { get; set; }
        public DbSet<Setting>? Setting { get; set; }
        public DbSet<Status>? Status { get; set; }
        public DbSet<Supplier>? Supplier { get; set; }
        public DbSet<User>? User { get; set; }
        public DbSet<Message>? Message { get; set; }
        public DbSet<Vw_MyOrder>? Vw_MyOrders { get; set; }
        public DbSet<Sp_Search>? Sp_Searches{ get; set; }
        public DbSet<AboutUs>? AboutUs { get; set; }
        public DbSet<Communication> Communication { get; set; }
    }
}
