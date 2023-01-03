using WebshopAPI;
using WebshopAPI.data;
using WebshopAPI.data.views;

namespace WebshopAPI.lib.Database
{
    public class SQL : DbContext
    {
        public SQL() : base(new DbContextOptionsBuilder()
            .UseSqlServer(Program.CONNECTION_STRING).Options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>().HasKey("UserID", "RoleID");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles {get; set; }
        public DbSet<Role> Roles {get; set; }
        public DbSet<Order> Orders {get; set; }
        public DbSet<Product> Products {get; set; }
        public DbSet<Log> Logs {get; set; }
        public DbSet<v_OrderData> v_OrderDatas {get; set; }
        public DbSet<v_User> v_Users {get; set; }
    }
}
