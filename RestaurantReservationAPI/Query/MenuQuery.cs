using System;
using GraphQL;
using GraphQL.Types;
using RestaurantReservationAPI.Interfaces;
using RestaurantReservationAPI.Type;

namespace RestaurantReservationAPI.Query;

public class MenuQuery : ObjectGraphType
{
    public MenuQuery(IMenuRepository menuRepository)
    {
        Field<ListGraphType<MenuType>>("Menus")
            .Resolve(context => menuRepository.GetAllMenu());

        Field<MenuType>("Menu")
        .Arguments(new QueryArguments(new QueryArgument<IntGraphType> { Name = "menuId" }))
        .Resolve(context => menuRepository.GetMenuById(context.GetArgument<int>("menuId")));
    }

}
