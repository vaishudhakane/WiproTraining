using Microsoft.EntityFrameworkCore;
using OnlineBookStore.Data;
using OnlineBookStore.Models;

namespace OnlineBookStore.Data.Repositories;
public class OrderRepository(ApplicationDbContext db) : IOrderRepository
{
    public async Task<Order> CreateAsync(Order order)
    {
        db.Orders.Add(order);
        await db.SaveChangesAsync();
        return order;
    }

    public Task<Order?> GetByIdAsync(int id, string userName)
        => db.Orders
            .Include(o => o.Items).ThenInclude(i => i.Book)
            .FirstOrDefaultAsync(o => o.Id == id && o.UserName == userName);

    public Task<List<Order>> GetForUserAsync(string userName)
        => db.Orders
            .Include(o => o.Items).ThenInclude(i => i.Book)
            .Where(o => o.UserName == userName)
            .OrderByDescending(o => o.CreatedUtc).ToListAsync();
}
