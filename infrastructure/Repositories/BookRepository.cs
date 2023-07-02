using Dapper;
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


}

public class BookFeedQuery
{
    public string BookTitle { get; set; }
    public int BookId { get; set; }
    public string CoverImgUrl { get; set; }
    public string Author { get; set; }

}

