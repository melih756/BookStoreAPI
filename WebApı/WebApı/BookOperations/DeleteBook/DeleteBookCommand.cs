using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApı.DbOperation;

namespace WebApı.BookOperations.DeleteBook
{
    public class DeleteBookCommand
    {
        private readonly BookStoreDbContext _dbContext;
        public int BookId { get; set; }
        public DeleteBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Id ==BookId);

            if (book is null)
                throw new InvalidOperationException("silinecek kitap bulunamadı");
            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();
        }
       
    }
}
