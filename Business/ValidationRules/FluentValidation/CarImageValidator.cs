using Entity.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class CarImageValidator : AbstractValidator<CarImage>
    {
        public CarImageValidator()
        {
            RuleFor(x => x.CarId).NotNull().WithMessage("Araç numarası bölümü boş bırakılamaz.");
            RuleFor(x => x.ImageName).NotNull().WithMessage("Resim adı boş bırakılamaz.");
            RuleFor(x => x.ImageName).MaximumLength(25).WithMessage("Resim adı en fazla 25 karakter olabilir.");
            RuleFor(x => x.ImagePath).MaximumLength(250).WithMessage("Resmin yolu en fazla 250 karakter olabilir.");
            RuleFor(x => x.UploadDate).NotNull().WithMessage("Resmin yüklenme tarihi boş bırakılamaz.");
        }
    }
}
