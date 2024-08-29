using System;
using RestaurantReservationAPI.Mutation;
using RestaurantReservationAPI.Query;

namespace RestaurantReservationAPI.Schema;

public class RootSchema : GraphQL.Types.Schema
{
    public RootSchema(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        Query = serviceProvider.GetRequiredService<RootQuery>();
        Mutation = serviceProvider.GetRequiredService<RootMutation>();
    }
}
