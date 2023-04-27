using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApı.DbOperation;

namespace WebApı.BookOperations.CreateBook
{
    public class CreateBookCommand
    {
        public CreateBookModel Model { get; set; }
        private readonly BookStoreDbContext _dbcontext;
        private readonly IMapper _mapper;
        public CreateBookCommand(BookStoreDbContext dbContext,IMapper mapper)
        {
            _dbcontext = dbContext;
            _mapper = mapper;
        }
        public void Handle()
        {
            var book = _dbcontext.Books.SingleOrDefault(x => x.Title == Model.Title);
            if (book is not null)
                throw new InvalidOperationException("kitap mevcut");
            book = _mapper.Map<Book>(Model);/*new Book();*/
            //book.Title = Model.Title;
            //book.PublishDate = Model.PublishDate;
            //book.PageCount = Model.PageCount;
            //book.GenreId = Model.PageCount;

            _dbcontext.Books.Add(book);
            _dbcontext.SaveChanges();
        }

        public class CreateBookModel
        {
            public string Title { get; set; }
            public int GenreId { get; set; }
            public int PageCount { get; set; }
            public DateTime PublishDate { get; set; }
        }
    }
}
