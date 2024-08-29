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

## SQL Server Docker Container

```powershell
# NOTE: START CONTAINERS FROM EXISTING IMAGES WITHOUT REBUILDING
docker compose -f docker-compose.yml up -d
# NOTE: STOP RUNNING CONTAINERS AND REMOVE CONTAINERS
docker compose -f docker-compose.yml down
```

### Create Database Migrations for Menus, Categories, Reservations

```powershell
dotnet ef migrations add InitialCreate -p RestaurantReservationAPI -s RestaurantReservationAPI -o Data\Migrations

dotnet ef migrations remove -p RestaurantReservationAPI -s RestaurantReservationAPI

dotnet ef database update -s RestaurantReservationAPI
```

## Updated GraphQL Queries

```json
query {
  categoryQuery{
    categories{
      id
      name
      imageUrl
    }
  }
}

query {
  reservationQuery{
    reservations{
      id
      customerName
      email
      phoneNumber
      partySize
      specialRequest
      reservationDate
    }
  }
}

query {
  menuQuery{
    menus{
      id
      name
      description
      price
      imageUrl
      categoryId
    }
  }
}

query{
  menuQuery{
    menu(menuId:5){
      id
      name
      description
      price
      imageUrl
      categoryId
    }
  }
}

query {
  categoryQuery{
    categories{
      id
      name
      imageUrl
      menus{
        id
        name
        description
        price
        imageUrl
      }
    }
  }
}

query{
  menuQuery{
    menu(menuId:5){
      id
      name
      description
      price
      imageUrl
      categoryId
    }
  }
}

mutation UpdateMenu($menuId:Int, $menu:MenuInput){
  menuMutation{
    updateMenu(menuId:$menuId, menu:$menu){
        id
        name
        description
        price
        imageUrl
        categoryId
      }
  }
}
query variables
{
  "menuId":5,
  "menu":{
    "id":5,
    "name":"Chocolate Brownie Sundae",
    "description": "Warm chocolate brownie with vanilla ice cream and fudge",
    "price": 7.50,
    "imageUrl": "https://example.com/chocolate-brownie-sundae.jpg",
    "categoryId": 3
  }
}


mutation AddMenu($menu:MenuInput){
  menuMutation{
    createMenu(menu:$menu){
        name
        description
        price
        imageUrl
        categoryId
      }
  }
}
query variables
{
  "menu":{
    "name":"Apple Pie",
    "description": "Warm Apple Pie with homemade crust",
    "price": 10.50,
    "imageUrl": "https://example.com/apple-pie.jpg",
    "categoryId": 3
  }
}

mutation AddReservation($reservation:ReservationInput){
  reservationMutation{
    createReservation(reservation:$reservation){
      customerName
      email
      phoneNumber
      partySize
      specialRequest
      reservationDate
    }
  }
}
query variables
{
  "reservation": {
    "customerName": "John",
    "email": "John@email.com",
    "phoneNumber": "0016035551212",
    "partySize": 5,
    "specialRequest": "Book a corner table with window view",
    "reservationDate": "2025-07-04"
  }
}
```
