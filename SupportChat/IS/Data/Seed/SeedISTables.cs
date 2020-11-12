using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IS.Configs;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IS.Data.Seed
{
    public static class SeedISTables
    {
        public static void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

                var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

                context.Database.Migrate();


                context.Clients.RemoveRange(context.Clients);
                foreach (var client in Config.Clients)
                {
                    context.Clients.Add(client.ToEntity());
                }
                context.SaveChanges();


                context.IdentityResources.RemoveRange(context.IdentityResources);
                foreach (var resource in Config.IdentityResources)
                {
                    context.IdentityResources.Add(resource.ToEntity());
                }
                context.SaveChanges();


                context.ApiResources.RemoveRange(context.ApiResources);
                foreach (var resource in Config.ApiResources)
                {
                    context.ApiResources.Add(resource.ToEntity());
                }
                context.SaveChanges();


                context.ApiScopes.RemoveRange(context.ApiScopes);
                foreach (var scope in Config.ApiScopes)
                {
                    context.ApiScopes.Add(scope.ToEntity());
                }
                context.SaveChanges();
            }
        }
    }
}
