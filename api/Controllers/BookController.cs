using System.ComponentModel.DataAnnotations;
using infrastructure.QueryModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
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
        return new ResponseDto("Success")
        {
            ResponseData = _bookService.GetBooksForFeed()
        };
    }


    [HttpPost]
    [ValidateModelFilter]
    [Route("/api/books")]
    public ResponseDto Post([FromBody]CreateBookRequestDto dto)
    {
        return new ResponseDto("Successfully create a new book");
    }


    
}

public class ValidateModelFilter : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ModelState.IsValid) return;
        
        string errorMessages = context.ModelState
            .Values
            .SelectMany(i => i.Errors.Select(e => e.ErrorMessage))
            .Aggregate((i, j) => i + "," + j);
        context.Result = new JsonResult(
            new ResponseDto(errorMessages))
        {
            StatusCode = 400
        };
    }
}

public class ResponseDto
{
    public string MessageToClient { get; set; }
    public Object? ResponseData { get; set; }

    public ResponseDto(string messageToClient)
    {
        MessageToClient = messageToClient;
    }
}

public class CreateBookRequestDto
{
    [MinLength(5)]
    public string BookTitle { get; set; }
    public string Author { get; set; }
    public string CoverImgUrl { get; set; }
    [IsValueOneOf(new string[] {"publisher_1", "publisher_2"}, "must be one of values ....")]
    public string Publisher { get; set; }
}


public class IsValueOneOf : ValidationAttribute
{

    private readonly string[] _validStrings;
    private readonly string _erorrMessage;
    
    public IsValueOneOf(string[] validStrings, string errorMessage)
    {
        _validStrings = validStrings;
        _erorrMessage = errorMessage;
    }

    protected override ValidationResult IsValid(object? givenString, ValidationContext ctx)
    {
        if (_validStrings.Contains(givenString))
        {
            return ValidationResult.Success;
        }

        return new ValidationResult(_erorrMessage);
    }
}
