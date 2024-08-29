using System;
using GraphQL.Types;
using RestaurantReservationAPI.Interfaces;
using RestaurantReservationAPI.Type;

namespace RestaurantReservationAPI.Query;

public class ReservationQuery : ObjectGraphType
{
    public ReservationQuery(IReservationRepository reservationRepository)
    {
        Field<ListGraphType<ReservationType>>("Reservations")
            .Resolve(context => reservationRepository.GetAllReservation());
    }
}
