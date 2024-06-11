using Microsoft.EntityFrameworkCore;

namespace Library.Models;



public class PaginationResponse
{
  
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int RemainingPages { get; set; }
    public int RemainingEntries {get; set; }
    public Book[]? Books { get; set; }

}