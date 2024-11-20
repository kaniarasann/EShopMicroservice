using Microsoft.EntityFrameworkCore;

namespace DiscountGRPC.Data
{
    public static class Extension
    {
        public static IApplicationBuilder SeedData(this IApplicationBuilder app) { 
        
            using var scope = app.ApplicationServices.CreateScope();
            using var dbcontext = scope.ServiceProvider.GetRequiredService<DiscountContext>();
            dbcontext.Database.MigrateAsync();
            return app;
        }
    }
}
