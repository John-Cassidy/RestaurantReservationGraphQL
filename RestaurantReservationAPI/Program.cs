using GraphiQl;
using GraphQL;
using GraphQL.Types;
using RestaurantReservationAPI.Interfaces;
using RestaurantReservationAPI.Mutation;
using RestaurantReservationAPI.Query;
using RestaurantReservationAPI.Schema;
using RestaurantReservationAPI.Services;
using RestaurantReservationAPI.Type;

namespace RestaurantReservationAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddTransient<IMenuRepository, MenuRepository>();
        builder.Services.AddTransient<MenuType>();
        builder.Services.AddTransient<MenuQuery>();
        builder.Services.AddTransient<MenuMutation>();

        builder.Services.AddTransient<MenuInputType>();

        builder.Services.AddTransient<ISchema, MenuSchema>();

        builder.Services.AddGraphQL(config =>
            config.AddAutoSchema<ISchema>().AddSystemTextJson());

        builder.Services.AddControllers();

        var app = builder.Build();

        // Configure the HTTP request pipeline.

        app.UseHttpsRedirection();

        app.UseGraphiQl("/graphql"); // Add GraphiQL Playground middleware
        app.UseGraphQL<ISchema>();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
