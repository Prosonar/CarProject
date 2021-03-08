using Business.Abstract.Services;
using Business.BusinessAspects.Autofac;
using Business.Utilities.Messages.TurkishMessages;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Entity.Concrete;
using Core.Utilities.ExceptionHandle;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Business.Concrete.Managers
{
    public class UserOperationClaimManager : IUserOperationClaimService
    {
        private IUserOperationClaimDal _userOperationClaimDal;
        public UserOperationClaimManager(IUserOperationClaimDal userOperationClaimDal)
        {
            _userOperationClaimDal = userOperationClaimDal;
        }

        [ValidationAspect(typeof(UserOperationClaimValidator), Priority = 4)]
        [AuthAspect("admin",Priority = 5)]
        [CacheRemoveAspect("IUserOperationClaimService.Get", Priority = 3)]
        public IResult Add(UserOperationClaim userOperationClaim)
        {
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _userOperationClaimDal.Add(userOperationClaim);
            });
            if(result)
            {
                return new ErrorResult(Messages.ErrorMessage);
            }
            return new SuccessResult(Messages.SuccessMessage);
        }
        [AuthAspect("admin",Priority = 5)]
        [CacheAspect]
        public IDataResult<List<UserOperationClaim>> GetAll(Expression<Func<UserOperationClaim, bool>> filter = null)
        {
            var result = ExceptionHandler.HandleWithReturn<Expression<Func<UserOperationClaim,bool>>,List<UserOperationClaim>>((f) =>
            {
                return _userOperationClaimDal.GetAll(f);
            },filter);
            if (!result.Success)
            {
                return new ErrorDataResult<List<UserOperationClaim>>(Messages.ErrorMessage);
            }
            return new SuccessDataResult<List<UserOperationClaim>>(result.Data, Messages.SuccessMessage);
        }
    }
}
