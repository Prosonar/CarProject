using Entity.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class CarValidator : AbstractValidator<Car>
    {
        public CarValidator()
        {
            RuleFor(c => c.BrandId).NotNull().WithMessage("Marka alanı boş bırakılamaz");
            RuleFor(c => c.ColorId).NotNull().WithMessage("Renk alanı boş bırakılamaz");
            RuleFor(c => c.DailyPrice).NotNull().WithMessage("Günlük fiyat bilgisi boş bırakılamaz");
            RuleFor(c => c.Name).NotNull().WithMessage("İsim alanı boş bırakılamaz");
            RuleFor(c => c.Name).MaximumLength(50).WithMessage("İsim karakter aşımı.");
            RuleFor(c => c.Description).MaximumLength(500).WithMessage("Açıklama çok uzun");
            RuleFor(c => c.BrandId).NotNull().WithMessage("Marka alanı boş bırakılamaz");

        }
    }
}
