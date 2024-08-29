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
        builder.Services.AddTransient<MenuInputType>();
        builder.Services.AddTransient<CategoryInputType>();
        builder.Services.AddTransient<ReservationInputType>();

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

            // Seed Categories
            if (!context.Categories.Any())
            {
                context.Categories.AddRange(new List<Category>
                {
                    new Category { Name = "Appetizers", ImageUrl = "https://example.com/categories/appetizers.jpg" },
                    new Category { Name = "Main Course", ImageUrl = "https://example.com/categories/main-course.jpg" },
                    new Category { Name = "Desserts", ImageUrl = "https://example.com/categories/desserts.jpg" }
                });
                await context.SaveChangesAsync();
            }

            // Seed Menus
            if (!context.Menus.Any())
            {
                context.Menus.AddRange(new List<Menu>
                {
                    new Menu { Name = "Classic Burger", Description = "A juicy chicken burger with lettuce and cheese", Price = 8.99, ImageUrl="https://example.com/classic-burger.jpg", CategoryId = 2 },
                    new Menu { Name = "Margherita Pizza", Description = "Tomato, mozzarella, and basil pizza", Price = 10.50, ImageUrl="https://example.com/margherita-pizza.jpg", CategoryId = 2 },
                    new Menu { Name = "Grilled Chicken Salad", Description = "Fresh garden salad with grilled chicken", Price = 7.95, ImageUrl="https://example.com/grilled-chicken-salad.jpg", CategoryId = 2 },
                    new Menu { Name = "Pasta Alfredo", Description = "Creamy Alfredo sauce with fettuccine pasta", Price = 12.75, ImageUrl="https://example.com/pasta-alfredo.jpg", CategoryId = 2 },
                    new Menu { Name = "Chocolate Brownie Sundae", Description = "Warm chocolate brownie with ice cream and fudge", Price = 6.99, ImageUrl="https://example.com/chocolate-brownie-sundae.jpg", CategoryId = 3 },
                    new Menu { Name = "Chicken Wings", Description = "Spicy chicken wings served with blue cheese dip.", Price = 9.99, ImageUrl = "https://example.com/chicken-wings.jpg", CategoryId = 1 },
                    new Menu { Name = "Steak", Description = "Grilled steak with mashed potatoes and vegetables.", Price = 24.50, ImageUrl = "https://example.com/steak.jpg", CategoryId = 2 },
                    new Menu { Name = "Chocolate Cake", Description = "Decadent chocolate cake with a scoop of vanilla ice cream.", Price = 6.95, ImageUrl = "https://example.com/chocolate-cake.jpg", CategoryId = 3 }
                });
                await context.SaveChangesAsync();
            }

            // Seed Reservations
            if (!context.Reservations.Any())
            {
                context.Reservations.AddRange(new List<Reservation>
                {
                    new Reservation { CustomerName = "John Doe", Email = "johndoe@example.com", PhoneNumber = "555-123-4567", PartySize = 2, SpecialRequest = "No nuts in the dishes, please.", ReservationDate = DateTime.Now.AddDays(7) },
                    new Reservation { CustomerName = "Jane Smith", Email = "janesmith@example.com", PhoneNumber = "555-987-6543", PartySize = 4, SpecialRequest = "Gluten-free options required.", ReservationDate = DateTime.Now.AddDays(10) },
                    new Reservation { CustomerName = "Michael Johnson", Email = "michaeljohnson@example.com", PhoneNumber = "555-789-0123", PartySize = 6, SpecialRequest = "Celebrating a birthday.", ReservationDate = DateTime.Now.AddDays(14) }
                });
                await context.SaveChangesAsync();
            }
        }
    }
}
