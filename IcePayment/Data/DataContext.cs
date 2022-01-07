using IcePaymentAPI.Model.Entity;

namespace IcePaymentAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Payment> Payments { get; set; } = null!;
    }
}
