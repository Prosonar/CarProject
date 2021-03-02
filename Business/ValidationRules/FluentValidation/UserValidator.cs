using Core.Entity.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(u => u.FirstName).NotNull().WithMessage("İsim boş bırakılamaz");
            RuleFor(u => u.FirstName).Length(2,50).WithMessage("İsim çok uzun.");
            RuleFor(u => u.LastName).NotNull().WithMessage("Soyadı boş bırakılamaz");
            RuleFor(u => u.LastName).Length(2,50).WithMessage("Soyadı çok uzun");
            RuleFor(u => u.Email).NotNull().WithMessage("Email boş bırakılamaz");
            RuleFor(u => u.Email).Length(8,50).WithMessage("Email uygun değildir.");
            //RuleFor(u => u.Password).NotNull().WithMessage("Şifre boş bırakılamaz");
            //RuleFor(u => u.Password).Length(8,20).WithMessage("Şifre 8-20 karakter arasında olmalıdır.");
        }
    }
}
