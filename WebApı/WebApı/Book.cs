using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApı
{
    public class Book
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //ıd alanı auto ıncrement hale getirildi
        public int Id { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }

    }
}
