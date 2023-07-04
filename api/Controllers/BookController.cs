using infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using service;

namespace library.Controllers;

[ApiController]
public class BookController : ControllerBase
{

    private readonly ILogger<BookController> _logger;
    private readonly BookService _bookService;

    public BookController(ILogger<BookController> logger,
        BookService bookService)
    {
        _logger = logger;
        _bookService = bookService;
    }



    [HttpGet]
    [Route("/api/books")]
    public IEnumerable<BookFeedQuery> Get()
    {
        return _bookService.GetBooksForFeed();
    }
}


public class Book
{
    public string Title { get; }

    public Book(string title)
    {
        Title = title;
    }
}