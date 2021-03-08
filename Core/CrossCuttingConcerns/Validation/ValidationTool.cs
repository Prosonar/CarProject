//using Core.Entity;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Validation
{
    public static class ValidationTool
    {
        //public static void Validate(IValidator validator,object entity)
        //{
        //    var result = validator.Validate((IValidationContext)entity);
        //    if(result.Errors.Count > 0)
        //    {
        //        throw new ValidationException(result.Errors);
        //    }
        //}
        public static void Validate(IValidator validator, object entity)
        {
            var context = new ValidationContext<object>(entity);
            var result = validator.Validate(context);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
        }
    }
}
