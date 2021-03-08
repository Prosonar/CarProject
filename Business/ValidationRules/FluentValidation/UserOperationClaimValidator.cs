using Core.Entity.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class UserOperationClaimValidator : AbstractValidator<UserOperationClaim>
    {
        public UserOperationClaimValidator()
        {
            RuleFor(x => x.OperationClaimId).NotNull();
            RuleFor(x => x.UserId).NotNull();
        }
    }
}
