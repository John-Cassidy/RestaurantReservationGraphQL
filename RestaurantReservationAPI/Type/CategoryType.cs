using System;
using GraphQL.Types;
using RestaurantReservationAPI.Models;

namespace RestaurantReservationAPI.Type;

public class CategoryType : ObjectGraphType<Category>
{
    public CategoryType()
    {
        Field(x => x.Id);
        Field(x => x.Name);
        Field(x => x.ImageUrl);
    }
}
