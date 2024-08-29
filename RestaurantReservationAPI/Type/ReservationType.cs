using System;
using GraphQL.Types;
using RestaurantReservationAPI.Models;

namespace RestaurantReservationAPI.Type;

public class ReservationType : ObjectGraphType<Reservation>
{
    public ReservationType()
    {
        Field(x => x.Id);
        Field(x => x.CustomerName);
        Field(x => x.Email);
        Field(x => x.PhoneNumber);
        Field(x => x.PartySize);
        Field(x => x.SpecialRequest);
        Field(x => x.ReservationDate);
    }
}
