using IcePayment.API.Model.Entity;

namespace IcePayment.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Payment> Payments { get; set; } = null!;
    }
}
