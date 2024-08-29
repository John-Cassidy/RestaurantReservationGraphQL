using System;
using GraphQL.Types;

namespace RestaurantReservationAPI.Type;

public class ReservationInputType : InputObjectGraphType
{
    public ReservationInputType()
    {
        // create fields for the ReservationInputType based on Reservation model
        Field<IntGraphType>("id");
        Field<StringGraphType>("customerName");
        Field<StringGraphType>("email");
        Field<StringGraphType>("phoneNumber");
        Field<IntGraphType>("partySize");
        Field<StringGraphType>("specialRequest");
        Field<DateTimeGraphType>("reservationDate");
    }
}
