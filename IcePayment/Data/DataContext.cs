using IcePaymentAPI.Model.Entity;
using Microsoft.EntityFrameworkCore;

namespace IcePayment.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Payment> Payments { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
    }
}
