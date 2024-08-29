using System;
using GraphQL;
using GraphQL.Types;
using RestaurantReservationAPI.Interfaces;
using RestaurantReservationAPI.Models;
using RestaurantReservationAPI.Type;

namespace RestaurantReservationAPI.Mutation;

public class ReservationMutation : ObjectGraphType
{
    public ReservationMutation(IReservationRepository reservationRepository)
    {
        Field<ReservationType>("CreateReservation")
            .Arguments(new QueryArguments(new QueryArgument<ReservationInputType> { Name = "reservation" }))
            .Resolve(context =>
            {
                var reservation = context.GetArgument<Reservation>("reservation");
                return reservationRepository.AddReservation(reservation);
            });
    }
}
