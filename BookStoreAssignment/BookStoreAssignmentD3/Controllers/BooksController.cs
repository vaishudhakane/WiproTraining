using Microsoft.AspNetCore.Mvc;
using BookStoreAssignmentD3.Data;
using BookStoreAssignmentD3.Models;

namespace BookStoreMvc.Controllers;

public class BooksController : Controller
{
    private readonly BookRepository _repo;
    public BooksController(BookRepository repo) => _repo = repo;

    public IActionResult Index()
    {
        var books = _repo.GetAll();
        return View(books);
    }

    public IActionResult Details(int id)
    {
        var book = _repo.GetById(id);
        if (book == null) return NotFound();
        return View(book);
    }

    public IActionResult Create() => View();

    [HttpPost]
    public IActionResult Create(Book book)
    {
        if (ModelState.IsValid)
        {
            _repo.Add(book);
            return RedirectToAction("Index");
        }
        return View(book);
    }

    public IActionResult Edit(int id)
    {
        var book = _repo.GetById(id);
        if (book == null) return NotFound();
        return View(book);
    }

    [HttpPost]
    public IActionResult Edit(Book book)
    {
        if (ModelState.IsValid)
        {
            _repo.Update(book);
            return RedirectToAction("Index");
        }
        return View(book);
    }

    public IActionResult Delete(int id)
    {
        var book = _repo.GetById(id);
        if (book == null) return NotFound();
        return View(book);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        _repo.Delete(id);
        return RedirectToAction("Index");
    }
}
