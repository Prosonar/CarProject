using Entity.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;

using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class ColorValidator : AbstractValidator<Color>
    {
        public ColorValidator()
        {
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Name).NotNull();
            RuleFor(c => c.Name).Length(2, 50);
        }
    }
}
