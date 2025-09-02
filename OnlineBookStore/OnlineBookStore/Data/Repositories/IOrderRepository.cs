using OnlineBookStore.Models;

public interface IOrderRepository
{
    Task<Order> CreateAsync(Order order);
    Task<Order?> GetByIdAsync(int id, string userName);
    Task<List<Order>> GetForUserAsync(string userName);
}
