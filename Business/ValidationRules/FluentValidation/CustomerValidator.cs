using Entity.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(c => c.UserId).NotNull().WithMessage("UserId boş bırakılamaz.");
            RuleFor(c => c.CompanyName).NotNull().WithMessage("Şirket ismi boş bırakılamaz");
            RuleFor(c => c.CompanyName).MaximumLength(250).WithMessage("Şirket ismi çok uzun");
        }
    }
}
