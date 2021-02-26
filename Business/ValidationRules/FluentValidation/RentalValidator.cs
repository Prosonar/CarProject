using Entity.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class RentalValidator : AbstractValidator<Rental>
    {
        public RentalValidator()
        {
            RuleFor(r => r.CarId).NotNull().WithMessage("Araba id değeri boş bırakılamaz");
            RuleFor(r => r.CustomerId).NotNull().WithMessage("Müşteri id değeri boş bırakılamaz");
            RuleFor(r => r.RentDate).NotNull().WithMessage("Kiralama tarihi boş bırakılamaz");
        }
    }
}
