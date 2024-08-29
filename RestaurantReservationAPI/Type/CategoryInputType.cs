using System;
using GraphQL.Types;

namespace RestaurantReservationAPI.Type;

public class CategoryInputType : InputObjectGraphType
{
    public CategoryInputType()
    {
        Field<IntGraphType>("id");
        Field<StringGraphType>("name");
        Field<StringGraphType>("imageUrl");
    }
}
