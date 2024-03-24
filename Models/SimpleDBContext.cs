using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace SimpleCRUD.Models
{
    public class SimpleDBContext:DbContext
    {
        public SimpleDBContext(DbContextOptions<SimpleDBContext> options) : base(options)
        {

        }

        public DbSet<DataEntries> DataEntries { get; set; }
    }
}
