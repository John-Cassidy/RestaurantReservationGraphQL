using System;
using RestaurantReservationAPI.Models;

namespace RestaurantReservationAPI.Interfaces;

public interface ICategoryRepository
{
    List<Category> GetAllCategory();
    Category AddCategory(Category category);
    Category UpdateCategory(int id, Category category);
    void DeleteCategory(int id);
}
