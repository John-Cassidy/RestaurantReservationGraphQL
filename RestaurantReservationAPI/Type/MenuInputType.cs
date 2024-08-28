using System;
using GraphQL.Types;

namespace RestaurantReservationAPI.Type;

public class MenuInputType : InputObjectGraphType
{
    public MenuInputType()
    {
        Field<IntGraphType>("id");
        Field<StringGraphType>("name");
        Field<StringGraphType>("description");
        Field<FloatGraphType>("price");
    }
}
