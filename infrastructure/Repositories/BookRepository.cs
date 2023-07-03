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

    public IEnumerable<BookFeedItem> GetBooksForFeed()
    {
        string sql = $@"
SELECT book_id as {nameof(BookFeedItem.BookId)},
       book_title as {nameof(BookFeedItem.BookTitle)},
        author as {nameof(BookFeedItem.Author)},
        cover_img_url as {nameof(BookFeedItem.CoverImgUrl)}
FROM library_app.books;
";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Query<BookFeedItem>(sql);
        }
    }


    public Book CreateBook(string bookTitle, string coverImgUrl, string publisher, string author)
    {
        return new Book();
    }
}

