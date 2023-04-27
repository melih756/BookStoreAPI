using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApı.BookOperations.CreateBook
{
    public class CreateBookCommandValidate:AbstractValidator<CreateBookCommand> //cretae book command sınıfına validasyon işlemi yapıldı
    {
        public CreateBookCommandValidate()
        {
            RuleFor(command => command.Model.GenreId).GreaterThan(0); //0 dan daha büyük degerleri kabul eder küçükleri etmez
            RuleFor(command => command.Model.PageCount).GreaterThan(0);
            RuleFor(command => command.Model.PublishDate.Date).NotEmpty().LessThan(DateTime.Now.Date); //date(gün) bugün olamaz
            RuleFor(command => command.Model.Title).NotEmpty().MinimumLength(4);

        }
    }
}
