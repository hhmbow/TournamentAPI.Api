using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TournamentAPI.Api.Data.Data;
using TournamentAPI.Data;



// Inkludera andra using-direktiv som behövs för din databascontext eller dataseeding-logik

namespace TournamentAPI.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static async Task SeedDataAsync(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<TournamentAPIApiContext>();

                // Kalla på din seed-metod här
                await SeedData.SeedTournamentsAsync(context);
            }
        }
    }
}
