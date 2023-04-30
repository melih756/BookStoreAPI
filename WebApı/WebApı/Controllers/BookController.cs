using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
using FluentValidation;
using static WebApı.BookOperations.UpdateBooks.UpdateBooksCommand;

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
                GetBookDetailValidator validations = new GetBookDetailValidator();
                validations.ValidateAndThrow(query);
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

           
                command.Model = newBook;
                CreateBookCommandValidate validations = new CreateBookCommandValidate(); //validasyon işlemi yapıldı ve istenilen bilgiler korundu ve sistem hataları fırlattı
                validations.ValidateAndThrow(command); //validasyon hatasını yakaamaya yarayan method hatayı bulur ve fırlatır


                //if (!result.IsValid)
                //{
                //    foreach (var item in result.Errors)
                //    {
                //        Console.WriteLine("Özellik : " + item.PropertyName + "error message :" + item.ErrorMessage);
                //    }
                //}
                //else
                //{
                //    command.Handle();
                //}
               
                
           

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookViewModel updatedBook)
        {
           
            try
            {
                UpdateBooksCommand command = new UpdateBooksCommand(_context);
                command.BookId = id;
                command.Model = updatedBook;

                UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
                validator.ValidateAndThrow(command);
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

                DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
                validator.ValidateAndThrow(deleteBookCommand);

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
