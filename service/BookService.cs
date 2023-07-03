using System.ComponentModel.DataAnnotations;
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

    public IEnumerable<BookFeedQuery> GetBooksForFeed()
    {
        return _bookRepository.GetBooksForFeed();
    }

    public Book CreateBook(string bookTitle,string publisher, string coverImgUrl, string author)
    {
        var doesBookExist = _bookRepository.DoesBooktWithTitleExist( bookTitle);
        if (!doesBookExist)
        {
            throw new ValidationException("Book already exists with title " + bookTitle);
        }
        
        return _bookRepository.CreateBook(bookTitle, publisher, coverImgUrl, author);
    }

    public Book UpdateBook(string bookTitle, int bookId, string publisher, string coverImgUrl, string author)
    {
        return _bookRepository.UpdateBook(bookTitle, bookId, publisher, coverImgUrl, author);
    }

    public void DeleteBook(int bookId)
    {
        
        var result = _bookRepository.DeleteBook(bookId);
        if (!result)
        {
            throw new Exception("Could not insert book");
        }
    }
}
