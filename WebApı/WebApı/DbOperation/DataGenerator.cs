using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApı.DbOperation
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if (context.Books.Any())
                {
                    return;
                }
                context.Books.AddRange(new Book
                {
                    //Id = 1,
                    Tittle = "Çalıkuşu",
                    GenreId = 1,
                    PageCount = 200,
                    Author = "Reşat Nuri Güntekin",
                    PublishDate = new DateTime(2000, 06, 12)
                },
            new Book
            {
                //Id = 2,
                Tittle = "Aşkı Memnu",
                GenreId = 2,
                PageCount = 328,
                Author = "Halit Ziya Uşaklıgil",
                PublishDate = new DateTime(1901, 12, 5)
            });
                context.SaveChanges();
            }
            
        }

        internal static void Initialize(object services)
        {
            throw new NotImplementedException();
        }
    }
}
