using System;
using RestaurantReservationAPI.Query;

namespace RestaurantReservationAPI.Schema;

public class MenuSchema : GraphQL.Types.Schema
{
    public MenuSchema(MenuQuery menuQuery)
    {
        Query = menuQuery;
    }
}
