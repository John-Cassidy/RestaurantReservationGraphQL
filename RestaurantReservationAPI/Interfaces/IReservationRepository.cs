using System;
using RestaurantReservationAPI.Models;

namespace RestaurantReservationAPI.Interfaces;

public interface IReservationRepository
{
    List<Reservation> GetAllReservation();
    Reservation AddReservation(Reservation reservation);
}
