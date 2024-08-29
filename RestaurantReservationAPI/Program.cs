using GraphiQl;
using GraphQL;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;
using Polly;
using RestaurantReservationAPI.Data;
using RestaurantReservationAPI.Interfaces;
using RestaurantReservationAPI.Models;
using RestaurantReservationAPI.Mutation;
using RestaurantReservationAPI.Query;
using RestaurantReservationAPI.Schema;
using RestaurantReservationAPI.Services;
using RestaurantReservationAPI.Type;

namespace RestaurantReservationAPI;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddTransient<IReservationRepository, ReservationRepository>();
        builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
        builder.Services.AddTransient<IMenuRepository, MenuRepository>();

        builder.Services.AddTransient<MenuType>();
        builder.Services.AddTransient<CategoryType>();
        builder.Services.AddTransient<ReservationType>();

        builder.Services.AddTransient<MenuQuery>();
        builder.Services.AddTransient<CategoryQuery>();
        builder.Services.AddTransient<ReservationQuery>();
        builder.Services.AddTransient<RootQuery>();

        // builder.Services.AddTransient<MenuMutation>();
        // builder.Services.AddTransient<MenuInputType>();

        builder.Services.AddTransient<ISchema, RootSchema>();

        builder.Services.AddGraphQL(config =>
            config.AddAutoSchema<ISchema>().AddSystemTextJson());

        builder.Services.AddDbContext<RestaurantDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("RestaurantConnectionString")));

        builder.Services.AddControllers();

        var app = builder.Build();

        // Ensure migrations are applied and database is seeded.
        app.Lifetime.ApplicationStarted.Register(async () =>
        {
            await Policy.Handle<TimeoutException>()
                .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(10))
                .ExecuteAndCaptureAsync(async () => await ApplyMigrationsAndSeedDatabaseAsync(app));
        });

        // Configure the HTTP request pipeline.

        app.UseHttpsRedirection();

        app.UseGraphiQl("/graphql"); // Add GraphiQL Playground middleware
        app.UseGraphQL<ISchema>();

        app.UseAuthorization();

        app.MapControllers();

        await app.RunAsync();

    }

    private static async Task ApplyMigrationsAndSeedDatabaseAsync(WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<RestaurantDbContext>();

            // Apply migrations
            await context.Database.MigrateAsync();

            // Seed database
            if (!context.Menus.Any())
            {
                context.Menus.AddRange(new List<Menu>
                {
                    new Menu { Name = "Classic Burger", Description = "A juicy chicken burger with lettuce and cheese", Price = 8.99 },
                    new Menu { Name = "Margherita Pizza", Description = "Tomato, mozzarella, and basil pizza", Price = 10.50 },
                    new Menu { Name = "Grilled Chicken Salad", Description = "Fresh garden salad with grilled chicken", Price = 7.95 },
                    new Menu { Name = "Pasta Alfredo", Description = "Creamy Alfredo sauce with fettuccine pasta", Price = 12.75 },
                    new Menu { Name = "Chocolate Brownie Sundae", Description = "Warm chocolate brownie with ice cream and fudge", Price = 6.99 }
                });
                await context.SaveChangesAsync();
            }
        }
    }
}
