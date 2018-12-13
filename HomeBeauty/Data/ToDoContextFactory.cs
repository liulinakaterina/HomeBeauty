using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HomeBeauty.Data
{
    public class ToDoContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<DataContext>();
            builder.UseSqlServer("Server = (localdb)\\mssqllocaldb; Database = CleanerDb; Trusted_Connection = True; MultipleActiveResultSets = true");
            return new DataContext(builder.Options);
        }
    }
}
