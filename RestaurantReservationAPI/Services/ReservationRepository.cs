using System;
using RestaurantReservationAPI.Data;
using RestaurantReservationAPI.Interfaces;
using RestaurantReservationAPI.Models;

namespace RestaurantReservationAPI.Services;

public class ReservationRepository : IReservationRepository
{
    private readonly RestaurantDbContext _restaurantDbContext;

    public ReservationRepository(RestaurantDbContext restaurantDbContext)
    {
        _restaurantDbContext = restaurantDbContext;
    }

    public Reservation AddReservation(Reservation reservation)
    {
        _restaurantDbContext.Add(reservation);
        _restaurantDbContext.SaveChanges();
        return reservation;
    }

    public List<Reservation> GetAllReservation()
    {
        return _restaurantDbContext.Reservations.ToList();
    }
}
