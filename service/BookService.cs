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

}
