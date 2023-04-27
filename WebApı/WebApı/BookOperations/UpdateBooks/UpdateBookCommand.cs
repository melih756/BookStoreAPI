using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApı.DbOperation;

namespace WebApı.BookOperations.UpdateBooks
{
    public class UpdateBookModel
    {
        private readonly BookStoreDbContext _context;
        public int BookId { get; set; }
        public UpdateBookViewModel Model { get; set; }

        public UpdateBookModel(BookStoreDbContext context)
        {
            _context = context;
        }
        public void Handle()
        {

            var book = _context.Books.SingleOrDefault(x => x.Id == BookId);
            if (book is not null)
                throw new InvalidOperationException("Güncellenecek kitap bulunamadı");

            book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId; //güncellmeler kitabın propertyleri üzerinden kontrol edildi ve güncellme işlemi uygulandı
            //book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
            //book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;
            book.Title = Model.Title != default ? Model.Title : book.Title;
            _context.SaveChanges();
        }
        public class UpdateBookViewModel
        {
            public string Title { get; set; }
            public int GenreId { get; set; }

            public static implicit operator UpdateBookViewModel(UpdateBookModel v)
            {
                throw new NotImplementedException();
            }
        }
    }
}
