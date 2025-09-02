using OnlineBookStore.Models;

public interface IBookRepository
{
    Task<List<Book>> GetAllAsync();
    Task<Book?> GetByIdAsync(int id);
    Task AddAsync(Book book);
    Task UpdateAsync(Book book);
    Task DeleteAsync(int id);
    Task<bool> IsIsbnTakenAsync(string isbn, int? ignoreId = null);
}
