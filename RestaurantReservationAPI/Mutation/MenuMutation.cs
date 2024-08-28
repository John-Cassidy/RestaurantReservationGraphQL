using System;
using GraphQL;
using GraphQL.Types;
using RestaurantReservationAPI.Interfaces;
using RestaurantReservationAPI.Models;
using RestaurantReservationAPI.Type;

namespace RestaurantReservationAPI.Mutation;

public class MenuMutation : ObjectGraphType
{
    public MenuMutation(IMenuRepository menuRepository)
    {
        Field<MenuType>("CreateMenu")
            .Arguments(new QueryArguments(new QueryArgument<MenuInputType> { Name = "menu" }))
            .Resolve(context =>
            {
                var menu = context.GetArgument<Menu>("menu");
                return menuRepository.AddMenu(menu);
            });

        Field<MenuType>("UpdateMenu")
            .Arguments(new QueryArguments(new QueryArgument<IntGraphType> { Name = "menuId" },
                new QueryArgument<MenuInputType> { Name = "menu" }
            ))
            .Resolve(context =>
            {
                var id = context.GetArgument<int>("menuId");
                var menu = context.GetArgument<Menu>("menu");
                return menuRepository.UpdateMenu(id, menu);
            });

        Field<StringGraphType>("DeleteMenu")
            .Arguments(new QueryArguments(new QueryArgument<IntGraphType> { Name = "menuId" }))
            .Resolve(context =>
            {
                var id = context.GetArgument<int>("menuId");
                menuRepository.DeleteMenu(id);
                return $"The menu with the id: {id} has been successfully deleted.";
            });
    }
}
