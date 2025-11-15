using System.Reflection;
using FinCashly.Application.Users.GetUsersList;
using FinCashly.Infrastructure.DataBase;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinCashly.API.Configurations; 

public static class ConnectionDataBase
{
    public static IServiceCollection ConnectionWithDataBase(this IServiceCollection services, string connectionString)
    {
         services.AddDbContext<DataBaseContext>(connection =>
        {
            connection.UseNpgsql(connectionString, npg =>
            {
                npg.CommandTimeout(60);
            });
        });
        return services; 
    }
    
}