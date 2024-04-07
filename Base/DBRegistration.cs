using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Base.Context;

namespace Infrastructure.Base;

public static class DBRegistration
{
    public static void AddBaseInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
           options.UseSqlServer(
               configuration.GetConnectionString("DefaultConnection"),
               b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        //services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
        //services.AddTransient<IProductRepositoryAsync, ProductRepositoryAsync>();
    }
}
