using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApı.BookOperations.UpdateBooks
{
    public class UpdateBookCommandValidator: AbstractValidator<UpdateBooksCommand>
    {
        public UpdateBookCommandValidator()
        {
            RuleFor(command => command.BookId).GreaterThan(0);
            RuleFor(command => command.Model.GenreId).GreaterThan(0);
            RuleFor(command => command.Model.Title).NotEmpty().MinimumLength(4); //post sırasında yapılan kuralların uygulanması sağlıklıdır
        }
    }
}
