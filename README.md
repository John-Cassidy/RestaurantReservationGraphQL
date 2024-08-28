# RestaurantReservationGraphQL

Basic Restaurant Reservation System using GraphQL API

## Overview

The .NET 8 WebApi project uses GraphQL using the following nuget packages: GraphQL, GraphiQL, and GraphQL.Server.Transports.AspNetCore.

It configures the service in Program.cs to support GraphQL endpoints.

It demonstrates how to set up a data layer using EF Core as well as Model Design, use of Repository pattern, GraphQL Types, and GraphQL Schema and Query.

It also demonstrates the necessity to setup mutations when handling CRUD operations in GraphQL and how this can play well with CQRS pattern.

[GraphiQL.NET and graphiql Nuget Package Documentation](https://github.com/JosephWoodward/graphiql-dotnet)

## GraphQL Queries

```json

query GetAllMenu{
  menus{
    id
    name
    price
    description
  }
}

query GetMenuById{
  menu(menuId:5){
    id
    name
    price
    description
  }
}

mutation AddMenu($menu:MenuInput){
  createMenu(menu:$menu){
    id
    name
    price
    description
  }
}
query variable
{
  "menu":{
    "id":6,
    "name":"Green Tea",
    "price": 8.5,
    "description": "Sugar free green tea with lemon zest"
  }
}

mutation UpdateMenu($menuId:Int, $menu:MenuInput){
  updateMenu(menuId:$menuId, menu:$menu){
    id
    name
    price
    description
  }
}
query variables
{
  "menuId":6,
  "menu":{
    "id":6,
    "name":"Black Tea",
    "price": 9.5,
    "description": "Sugar free black tea"
  }
}

mutation DeleteMenu($menuId:Int){
  deleteMenu(menuId:$menuId)
}
query variable
{
  "menuId":6
}
```
