using System;
using GraphQL.Types;
using RestaurantReservationAPI.Interfaces;
using RestaurantReservationAPI.Models;

namespace RestaurantReservationAPI.Type;

public class CategoryType : ObjectGraphType<Category>
{
    public CategoryType(IMenuRepository menuRepository)
    {
        Field(x => x.Id);
        Field(x => x.Name);
        Field(x => x.ImageUrl);
        Field<ListGraphType<MenuType>>("Menus")
            .Resolve(context => menuRepository.GetAllMenu());
    }
}
