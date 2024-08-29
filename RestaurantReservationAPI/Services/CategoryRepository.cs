using System;
using RestaurantReservationAPI.Data;
using RestaurantReservationAPI.Interfaces;
using RestaurantReservationAPI.Models;

namespace RestaurantReservationAPI.Services;

public class CategoryRepository : ICategoryRepository
{
    private readonly RestaurantDbContext _restaurantDbContext;

    public CategoryRepository(RestaurantDbContext restaurantDbContext)
    {
        _restaurantDbContext = restaurantDbContext;
    }

    public Category AddCategory(Category category)
    {
        _restaurantDbContext.Add(category);
        _restaurantDbContext.SaveChanges();
        return category;
    }

    public void DeleteCategory(int id)
    {
        var categoryToDelete = _restaurantDbContext.Categories.FirstOrDefault(x => x.Id == id);

        if (categoryToDelete != null)
        {
            _restaurantDbContext.Remove(categoryToDelete);
            _restaurantDbContext.SaveChanges();
        }
    }

    public List<Category> GetAllCategory()
    {
        return _restaurantDbContext.Categories.ToList();
    }

    public Category UpdateCategory(int id, Category category)
    {
        var categoryToUpdate = _restaurantDbContext.Categories.FirstOrDefault(x => x.Id == id);

        if (categoryToUpdate != null)
        {
            categoryToUpdate.Name = category.Name;
            categoryToUpdate.ImageUrl = category.ImageUrl;

            _restaurantDbContext.SaveChanges();
        }

        return category;
    }
}
