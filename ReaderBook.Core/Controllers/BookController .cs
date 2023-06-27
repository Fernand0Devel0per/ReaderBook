using Microsoft.AspNetCore.Mvc;
using ReaderBook.Core.BLL.Interface;
using ReaderBook.Core.Dtos.Book;
using ReaderBook.Core.Helpers.Exceptions;
using ReaderBook.Core.Models.ValueObject.Book;

namespace ReaderBook.Core.Controllers;

[ApiController]
[Route("api/books")]
public class BookController : ControllerBase
{
    private readonly IBookService _bookService;

    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPagedAsync(string id, int pageNumber = 1, int pageSize = 10)
    {
        try
        {
            var paginatedBookResponse = await _bookService.GetPagedAsync(id, pageNumber, pageSize);
            return Ok(paginatedBookResponse);
        }
        catch (ArgumentException ae)
        {
            return BadRequest(new { Message = ae.Message });
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Unexpected server error." });
        }
    }

    [HttpPost]
    public async Task<IActionResult> InsertAsync([FromBody] BookCreate book)
    {
        try
        {
            var bookResponse = await _bookService.InsertAsync(book);
            return StatusCode(StatusCodes.Status201Created, bookResponse);
        }
        catch (CustomValidationException cve)
        {
            return BadRequest(new { Message = cve.Message });
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Unexpected server error." });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync([FromBody] BookCreate book, string id)
    {
        try
        {
            await _bookService.UpdateAsync(book, id);
            return NoContent();
        }
        catch (CustomValidationException cve)
        {
            return BadRequest(new { Message = cve.Message });
        }
        catch (EntityNotFoundException enf)
        {
            return NotFound(new { Message = enf.Message });
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Unexpected server error." });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(string id)
    {
        try
        {
            await _bookService.DeleteAsync(id);
            return NoContent();
        }
        catch (EntityNotFoundException enf)
        {
            return NotFound(new { Message = enf.Message });
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Unexpected server error." });
        }
    }
}