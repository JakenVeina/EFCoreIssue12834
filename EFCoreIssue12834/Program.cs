using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Newtonsoft.Json;

namespace EFCoreIssue12834
{
    public class Program
    {
        public static async Task Main()
        {
            using (var serviceScope = BuildServiceScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<MyDbContext>();

                dbContext.Database.Migrate();

                await EnsureDbContextIsPopulated(dbContext);

                var results = new
                {
                    Primary = await dbContext.PrimaryEntities
                        .Select(MyQueryResult.FromPrimaryEntityProjection)
                        .ToArrayAsync(),
                    Secondary = await dbContext.SecondaryEntities
                        .Select(MyQueryResult.FromSecondaryEntityProjection)
                        .ToArrayAsync()
                };

                Console.WriteLine(JsonConvert.SerializeObject(results, Formatting.Indented));
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        private static IServiceScope BuildServiceScope()
        {
            var services = new ServiceCollection();

            services.AddDbContext<MyDbContext>(options =>
            {
                options.UseNpgsql("Server=localhost;Port=5432;Database=EFCoreIssue12834;User Id=X;Password=X;");
            });

            return services.BuildServiceProvider().CreateScope();
        }

        private static async Task EnsureDbContextIsPopulated(MyDbContext dbContext)
        {
            if (await dbContext.PrimaryEntities.AnyAsync())
                return;

            foreach(PrimaryEntityType primaryType in Enum.GetValues(typeof(PrimaryEntityType)))
                foreach(SecondaryEntityType secondaryType in Enum.GetValues(typeof(SecondaryEntityType)))
                {
                    var entity = new MyPrimaryEntity()
                    {
                        Type = primaryType,
                        SecondaryEntity = new MySecondaryEntity()
                        {
                            Type = secondaryType
                        }
                    };

                    await dbContext.PrimaryEntities.AddAsync(entity);
                    await dbContext.SaveChangesAsync();

                    entity.SecondaryEntity.PrimaryEntityId = entity.Id;
                    await dbContext.SaveChangesAsync();
                }
        }
    }
}
