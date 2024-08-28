using RestaurantReservationAPI.Models;

namespace RestaurantReservationAPI.Interfaces;

public interface IMenuRepository {
    List<Menu> GetAllMenu();
    Menu GetMenuById(int id);
    Menu AddMenu(Menu menu);
    Menu UpdateMenu(Menu menu);
    void DeleteMenu(int id);
}
