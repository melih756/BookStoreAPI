using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApı.Common;
using WebApı.DbOperation;

namespace WebApı.BookOperations.GetBooks
{
    public class GetBooksQuery
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetBooksQuery(BookStoreDbContext dbContext,IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public List<BookViewModel> Handle()
        {
            var bookList = _dbContext.Books.OrderBy(x => x.Id).ToList<Book>();
            List<BookViewModel> vm = _mapper.Map<List<BookViewModel>>(bookList);/*new List<BookViewModel>();*/
            //foreach (var book in bookList)
            //{
            //    vm.Add(new BookViewModel()
            //    {
            //        Title = book.Title,
            //        Genre = ((GenreEnum)book.GenreId).ToString(),
            //        PublishDate = book.PublishDate.Date.ToString("dd//MM/yyy"),
            //        PageCount = book.PageCount
            //    });
            return vm;
        }
           
        }
    }
    public class BookViewModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get;  set; }
    }
       

