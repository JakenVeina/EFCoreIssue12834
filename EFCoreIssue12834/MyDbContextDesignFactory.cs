using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EFCoreIssue12834
{
    public class MyDbContextDesignFactory : IDesignTimeDbContextFactory<MyDbContext>
    {
        public MyDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MyDbContext>();
            optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=EFCoreIssue12834;User Id=X;Password=X;");
            return new MyDbContext(optionsBuilder.Options);
        }
    }
}
