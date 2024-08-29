using RestaurantReservationAPI.Data;
using RestaurantReservationAPI.Interfaces;
using RestaurantReservationAPI.Models;

namespace RestaurantReservationAPI.Services;

public class MenuRepository : IMenuRepository
{
    private readonly RestaurantDbContext _restaurantDbContext;

    public MenuRepository(RestaurantDbContext restaurantDbContext)
    {
        _restaurantDbContext = restaurantDbContext;
    }


    public Menu AddMenu(Menu menu)
    {
        _restaurantDbContext.Add(menu);
        _restaurantDbContext.SaveChanges();
        return menu;
    }

    public void DeleteMenu(int id)
    {
        var menuToDelete = _restaurantDbContext.Menus.FirstOrDefault(x => x.Id == id);

        if (menuToDelete != null)
        {
            _restaurantDbContext.Remove(menuToDelete);
            _restaurantDbContext.SaveChanges();
        }
    }

    public List<Menu> GetAllMenu()
    {
        return _restaurantDbContext.Menus.ToList();
    }

    public Menu GetMenuById(int id)
    {
        return _restaurantDbContext.Menus.FirstOrDefault(x => x.Id == id);
    }

    public Menu UpdateMenu(int id, Menu menu)
    {
        // get the menu by id
        var menuToUpdate = _restaurantDbContext.Menus.FirstOrDefault(x => x.Id == id);

        // if menueToUpdate is not null, update the menu
        if (menuToUpdate != null)
        {
            menuToUpdate.Name = menu.Name;
            menuToUpdate.Description = menu.Description;
            menuToUpdate.Price = menu.Price;

            _restaurantDbContext.SaveChanges();
        }

        return menu;
    }
}
