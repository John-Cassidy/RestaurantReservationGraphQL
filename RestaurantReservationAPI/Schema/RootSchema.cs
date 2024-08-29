using System;
using RestaurantReservationAPI.Query;

namespace RestaurantReservationAPI.Schema;

public class RootSchema : GraphQL.Types.Schema
{
    public RootSchema(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        Query = serviceProvider.GetRequiredService<RootQuery>();
    }
}
