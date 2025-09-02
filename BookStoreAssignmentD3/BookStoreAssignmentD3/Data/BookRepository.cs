using Microsoft.Data.SqlClient;
using BookStoreAssignmentD3.Models;

namespace BookStoreAssignmentD3.Data;

public class BookRepository
{
    private readonly string _connStr;
    public BookRepository(IConfiguration config)
    {
        _connStr = config.GetConnectionString("DefaultConnection")!;
    }

    // Get all books
    public List<Book> GetAll()
    {
        var list = new List<Book>();
        using var conn = new SqlConnection(_connStr);
        using var cmd = new SqlCommand("SELECT * FROM Books", conn);
        conn.Open();
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            list.Add(new Book
            {
                BookId = (int)reader["BookId"],
                Title = reader["Title"].ToString()!,
                Author = reader["Author"].ToString()!,
                Price = (decimal)reader["Price"]
            });
        }
        return list;
    }

    // Get by Id
    public Book? GetById(int id)
    {
        using var conn = new SqlConnection(_connStr);
        using var cmd = new SqlCommand("SELECT * FROM Books WHERE BookId=@id", conn);
        cmd.Parameters.AddWithValue("@id", id);
        conn.Open();
        using var reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            return new Book
            {
                BookId = (int)reader["BookId"],
                Title = reader["Title"].ToString()!,
                Author = reader["Author"].ToString()!,
                Price = (decimal)reader["Price"]
            };
        }
        return null;
    }

    // Insert
    public void Add(Book book)
    {
        using var conn = new SqlConnection(_connStr);
        using var cmd = new SqlCommand(
            "INSERT INTO Books (Title, Author, Price) VALUES (@t, @a, @p)", conn);
        cmd.Parameters.AddWithValue("@t", book.Title);
        cmd.Parameters.AddWithValue("@a", book.Author);
        cmd.Parameters.AddWithValue("@p", book.Price);
        conn.Open();
        cmd.ExecuteNonQuery();
    }

    // Update
    public void Update(Book book)
    {
        using var conn = new SqlConnection(_connStr);
        using var cmd = new SqlCommand(
            "UPDATE Books SET Title=@t, Author=@a, Price=@p WHERE BookId=@id", conn);
        cmd.Parameters.AddWithValue("@t", book.Title);
        cmd.Parameters.AddWithValue("@a", book.Author);
        cmd.Parameters.AddWithValue("@p", book.Price);
        cmd.Parameters.AddWithValue("@id", book.BookId);
        conn.Open();
        cmd.ExecuteNonQuery();
    }

    // Delete
    public void Delete(int id)
    {
        using var conn = new SqlConnection(_connStr);
        using var cmd = new SqlCommand("DELETE FROM Books WHERE BookId=@id", conn);
        cmd.Parameters.AddWithValue("@id", id);
        conn.Open();
        cmd.ExecuteNonQuery();
    }
}
