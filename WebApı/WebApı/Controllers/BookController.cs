using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApı.BookOperations.CreateBook;
using WebApı.BookOperations.DeleteBook;
using WebApı.BookOperations.GetBookDetail;
using WebApı.BookOperations.GetBooks;
using WebApı.BookOperations.UpdateBooks;
using WebApı.DbOperation;
using static WebApı.BookOperations.CreateBook.CreateBookCommand;
using static WebApı.BookOperations.GetBookDetail.GetBookDetailQuery;

namespace WebApı.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : Controller
    {

        private readonly BookStoreDbContext _context; //readonly uyglama içerisinden değiştirilemez sadece constructorda değiştirlebbilir
        private readonly IMapper _mapper;

        public BookController(BookStoreDbContext context)
        {
            _context = context;
        }

       
        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery getBooksQuery = new GetBooksQuery(_context,_mapper);
            var result = getBooksQuery.Handle();
            return Ok(result);
        }

        //doğru yaklaşım route kullanrak id getirmektir 
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            BookDetailViewModel result;
          
            try
            {
                GetBookDetailQuery query = new GetBookDetailQuery(_context,_mapper);
                query.BookId = id;
                result=query.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(result);

        }

        //[HttpGet] //QUERY STRING İLE ID GETİRME doğru yaklaşım route ile yapmaktır 
        //public Book Get([FromQuery] string id)
        //{
        //    var book = BookList.Where(book => book.Id == Convert.ToInt32(id)).SingleOrDefault();
        //    return book;
        //}

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context,_mapper);

            try
            {
                command.Model = newBook;
                command.Handle();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
        {
           
            try
            {
                UpdateBookModel command = new UpdateBookModel(_context);
                command.BookId = id;
                command.Model = updatedBook;
                command.Handle();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            try 
            {
                DeleteBookCommand deleteBookCommand = new DeleteBookCommand(_context);
                deleteBookCommand.BookId = id;
                deleteBookCommand.Handle();
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
            return Ok();

        }
    } 

}
