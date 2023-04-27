using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApı.BookOperations.GetBookDetail
{
    public class GetBookDetailValidator:AbstractValidator<GetBookDetailQuery>
    {
        public GetBookDetailValidator()
        {
            RuleFor(query => query.BookId).GreaterThan(0);
        }
    }
}
