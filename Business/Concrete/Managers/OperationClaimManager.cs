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
    public class OperationClaimManager : IOperationClaimService
    {
        private IOperationClaimDal _operationClaimDal;

        public OperationClaimManager(IOperationClaimDal operationClaimDal)
        {
            _operationClaimDal = operationClaimDal;
        }

        [ValidationAspect(typeof(OperationClaimValidator), Priority = 4)]
        [AuthAspect("admin",Priority = 5)]
        [CacheRemoveAspect("IOperationClaimService.Get", Priority = 3)]
        public IResult Add(OperationClaim operationClaim)
        {
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _operationClaimDal.Add(operationClaim);
            });
            if (result)
            {
                return new ErrorResult(Messages.ErrorMessage);
            }
            return new SuccessResult(Messages.SuccessMessage);
        }
        [AuthAspect("admin",Priority = 5)]
        [CacheAspect]
        public IDataResult<List<OperationClaim>> GetAll(Expression<Func<OperationClaim, bool>> filter = null)
        {
            var result = ExceptionHandler.HandleWithReturn<Expression<Func<OperationClaim, bool>>, List<OperationClaim>>((f) =>
            {
                return _operationClaimDal.GetAll(f);
            }, filter);
            if (!result.Success)
            {
                return new ErrorDataResult<List<OperationClaim>>(Messages.ErrorMessage);
            }
            return new SuccessDataResult<List<OperationClaim>>(result.Data, Messages.SuccessMessage);
        }
    }
}
