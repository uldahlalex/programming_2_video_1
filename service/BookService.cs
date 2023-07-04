using infrastructure.DataModels;
using infrastructure.QueryModels;
using infrastructure.Repositories;

namespace service;
public class BookService
{
    private readonly BookRepository _bookRepository;
    
    public BookService(BookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public IEnumerable<BookFeedItem> GetBooksForFeed()
    {
        return _bookRepository.GetBooksForFeed();
    }

    public Book CreateBook(string bookTitle, string coverImgUrl, string publisher, string author)
    {
        return _bookRepository.CreateBook(bookTitle, coverImgUrl, publisher, author);

    }
}
