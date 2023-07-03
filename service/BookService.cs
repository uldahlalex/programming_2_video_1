using System.ComponentModel.DataAnnotations;
using infrastructure.Repositories;

namespace service;
public class BookService
{
    private readonly BookRepository _bookRepository;
    
    public BookService(BookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public IEnumerable<BookFeedQuery> GetBooksForFeed()
    {
        return _bookRepository.GetBooksForFeed();
    }

    public object CreateBook(string title)
    {
        
        
        if (true)
        {
            throw new ValidationException("Book with title " + title + " already exists");
        }
        return new { title = title };
    }
}
