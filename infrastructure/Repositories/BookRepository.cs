using Dapper;
using infrastructure.DataModels;
using infrastructure.QueryModels;
using Npgsql;

namespace infrastructure.Repositories;

public class BookRepository
{
    private NpgsqlDataSource _dataSource;

    public BookRepository(NpgsqlDataSource datasource)
    {
        _dataSource = datasource;
    }

    public IEnumerable<BookFeedQuery> GetBooksForFeed()
    {
        string sql = $@"
SELECT book_id as {nameof(BookFeedQuery.BookId)},
       book_title as {nameof(BookFeedQuery.BookTitle)},
        author as {nameof(BookFeedQuery.Author)},
        cover_img_url as {nameof(BookFeedQuery.CoverImgUrl)}
FROM library_app.books;
";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Query<BookFeedQuery>(sql);
        }
    }


    public Book UpdateBook(string bookTitle, int bookId, string publisher, string coverImgUrl, string author)
    {
        var sql = $@"
UPDATE library_app.books SET book_title = @bookTitle, publisher = @publisher, cover_img_url = @coverImgUrl, author = @author
WHERE book_id = @bookId
RETURNING book_id as {nameof(Book.BookId)},
       book_title as {nameof(Book.BookTitle)},
        author as {nameof(Book.Author)},
        cover_img_url as {nameof(Book.CoverImgUrl)},
    publisher as {nameof(Book.Publisher)};
";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<Book>(sql, new { bookTitle, bookId, publisher, coverImgUrl, author });
        }
    }

    public Book CreateBook(string bookTitle, string publisher, string coverImgUrl, string author)
    {
        var sql = $@"
INSERT INTO library_app.books (book_title, publisher, author, cover_img_url) 
VALUES (@bookTitle, @publisher, @author, @coverImgUrl)
RETURNING book_id as {nameof(Book.BookId)},
       book_title as {nameof(Book.BookTitle)},
        author as {nameof(Book.Author)},
        cover_img_url as {nameof(Book.CoverImgUrl)},
        publisher as {nameof(Book.Publisher)};
";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<Book>(sql, new { bookTitle, publisher, author, coverImgUrl });
        }
    }

    public bool DeleteBook(int bookId)
    {
        var sql = @"DELETE FROM library_app.books WHERE book_id = @bookId;";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Execute(sql, new { bookId }) == 1;
        }
    }

    public bool DoesBooktWithTitleExist(string bookTitle)
    {
        var sql = @"SELECT COUNT(*) FROM library_app.books WHERE book_title = @bookTitle;";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.ExecuteScalar<int>(sql, new { bookTitle }) == 1;
        }
    }
}