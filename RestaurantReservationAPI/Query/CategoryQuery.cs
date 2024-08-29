using System;
using GraphQL.Types;
using RestaurantReservationAPI.Interfaces;
using RestaurantReservationAPI.Type;

namespace RestaurantReservationAPI.Query;

public class CategoryQuery : ObjectGraphType
{
    public CategoryQuery(ICategoryRepository categoryRepository)
    {
        Field<ListGraphType<CategoryType>>("Categories")
            .Resolve(context => categoryRepository.GetAllCategory());
    }
}
