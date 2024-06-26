using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Library.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Azure;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;


namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryController : ControllerBase
    {
        private readonly BookContext _context;

        public LibraryController(BookContext context)
        {
            _context = context;
        }
        public enum SortableField
        {
            Author,
            Title,
            Publisher,
            PublishDate,
            PublishDateDesc,
        }

        // GET: api/Library
        [HttpGet]
        public async Task<ActionResult<PaginationResponse>> GetBooks(int pageNumber = 1, int pageSize = 10, SortableField? sortable = null ,string? Author = null, string? Title = null, string? Publisher = null, DateOnly? startDate = null, DateOnly? endDate = null)
        {
          if (_context.Books == null)
          {
              return NotFound();
          }

            var query =  _context.Books.AsQueryable();
        
            if (Author is not null)
                query = query.Where(e => e.Author == Author);

            if (Title is not null)
                query = query.Where(e => e.Title == Title);
            
            if (Publisher is not null)
                query = query.Where(e => e.Publisher == Publisher);
            
            if (startDate is not null)
                query = query.Where(e => e.PublishDate >= startDate);
            
            if (endDate is not null)
                query = query.Where(e => e.PublishDate <= endDate);


            if (sortable.HasValue)
            {
            switch (sortable.Value)
            {
                case SortableField.Author:
                    query = query.OrderBy(x => x.Author);
                    break;

                case SortableField.Title:
                    query = query.OrderBy(x => x.Title);
                    break;

                case SortableField.Publisher:
                    query = query.OrderBy(x => x.Publisher);
                    break;

                case SortableField.PublishDate:
                    query = query.OrderBy(x => x.PublishDate);
                    break;

                case SortableField.PublishDateDesc:
                    query = query.OrderByDescending(x => x.PublishDate);
                    break;
            }
            }


            var data = await query
                           
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToArrayAsync();


            var response = new PaginationResponse() {
                Page = pageNumber,
                PageSize = pageSize,
                RemainingEntries = query.Count() - data.Length,
                RemainingPages = (query.Count() -1)  / pageSize,
                Books = data
            };

            return response;
        }

        // GET: api/Library/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(long id)
        {
          if (_context.Books == null)
          {
              return NotFound();
          }
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        // PUT: api/Library/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(long id, Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Library
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
          if (_context.Books == null)
          {
              return Problem("Entity set 'BookContext.Books'  is null.");
          }
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBook", new { id = book.Id }, book);
        }

        // DELETE: api/Library/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(long id)
        {
            if (_context.Books == null)
            {
                return NotFound();
            }
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookExists(long id)
        {
            return (_context.Books?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}
