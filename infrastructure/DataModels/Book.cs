namespace infrastructure.DataModels;

public class Book
{
    public string BookTitle { get; set; }
    public int BookId { get; set; }
    public string CoverImgUrl { get; set; }
    public string Author { get; set; }
    public string Publisher { get; set; }
}