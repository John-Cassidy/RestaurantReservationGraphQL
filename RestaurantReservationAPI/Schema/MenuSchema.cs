using System;
using RestaurantReservationAPI.Mutation;
using RestaurantReservationAPI.Query;

namespace RestaurantReservationAPI.Schema;

public class MenuSchema : GraphQL.Types.Schema
{
    public MenuSchema(MenuQuery menuQuery, MenuMutation menuMutation)
    {
        Query = menuQuery;
        Mutation = menuMutation;
    }
}
