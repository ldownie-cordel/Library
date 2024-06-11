namespace Library.Models;

public class Book
{
    public long Id { get; set; }
    public string? Title { get; set; }
    public string? Author { get; set; }
    public string? Publisher { get; set; }
    public DateOnly? PublishDate { get; set; }
    public string? Blurb { get; set; }
    public int? NumPages { get; set; }
}