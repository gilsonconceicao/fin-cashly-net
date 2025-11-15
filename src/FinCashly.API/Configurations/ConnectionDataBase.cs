using FinCashly.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;

namespace FinCashly.API.Configurations; 

public static class ConnectionDataBase
{
    public static IServiceCollection ConnectionWithDataBase(this IServiceCollection services, string connectionString)
    {
         services.AddDbContext<ApplicationDbContext>(connection =>
        {
            connection.UseNpgsql(connectionString, npg =>
            {
                npg.CommandTimeout(60);
            });
        });
        return services; 
    }
    
}