using System.ComponentModel.DataAnnotations;
using api.CustomDataAnnotations;
using api.Filters;
using api.TransferModels;
using infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using service;

namespace library.Controllers;


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
    public ResponseDto Get()
    {
        return new ResponseDto()
        {
            MessageToClient = "Successfully fetched",
            ResponseData = _bookService.GetBooksForFeed()
        };
    }

    [HttpPost]
    [ValidateModel]
    [Route("/api/books")]
    public ResponseDto Post([FromBody] CreateBookRequestDto dto)
    {
        HttpContext.Response.StatusCode = StatusCodes.Status201Created;
        return new ResponseDto()
        {
            MessageToClient = "Successfully created a book",
            ResponseData = _bookService.CreateBook(dto.BookTitle, dto.Publisher, dto.CoverImgUrl, dto.Author)
        };
    }

    [HttpPut]
    [ValidateModel]
    [Route("/api/books/{bookId}")]
    public ResponseDto Put([FromRoute] int bookId,
        [FromBody] UpdateBookRequestDto dto)
    {
        HttpContext.Response.StatusCode = 201;
        return new ResponseDto()
        {
            MessageToClient = "Successfully updated",
            ResponseData =
                _bookService.UpdateBook(dto.BookTitle, dto.BookId, dto.Publisher, dto.CoverImgUrl, dto.Author)
        };

    }

    [HttpDelete]
    [Route("/api/books/{bookId}")]
    public ResponseDto Delete([FromRoute] int bookId)
    {
        _bookService.DeleteBook(bookId);
        return new ResponseDto()
        {
            MessageToClient = "Succesfully deleted"
        };

    }
}


