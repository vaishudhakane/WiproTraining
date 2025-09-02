using Microsoft.EntityFrameworkCore;
using OnlineBookStore.Data;
using OnlineBookStore.Models;
using System.Linq.Expressions; //newly added

namespace OnlineBookStore.Data.Repositories;
public class BookRepository(ApplicationDbContext db) : IBookRepository
{
    public Task<List<Book>> GetAllAsync() => db.Books.OrderBy(b => b.Title).ToListAsync();

    public Task<Book?> GetByIdAsync(int id) => db.Books.FirstOrDefaultAsync(b => b.Id == id);

    public async Task AddAsync(Book book)
    {
        db.Books.Add(book);
        await db.SaveChangesAsync();
    }

    public async Task UpdateAsync(Book book)
    {
        db.Books.Update(book);
        await db.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await db.Books.FindAsync(id);
        if (entity is null) return;
        db.Books.Remove(entity);
        await db.SaveChangesAsync();
    }

    public Task<bool> IsIsbnTakenAsync(string isbn, int? ignoreId = null)
        => db.Books.AnyAsync(b => b.ISBN == isbn && (ignoreId == null || b.Id != ignoreId));
}
