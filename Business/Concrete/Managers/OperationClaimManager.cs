using Business.Abstract.Services;
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

        public IResult Add(OperationClaim operationClaim)
        {
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _operationClaimDal.Add(operationClaim);
            });
            if (result)
            {
                return new ErrorResult("Beklenmedik bir hata oluştu.Lütfen daha sonra tekrar deneyiniz.");
            }
            return new SuccessResult("İşlem başarılı");
        }

        public IDataResult<List<OperationClaim>> GetAll(Expression<Func<OperationClaim, bool>> filter = null)
        {
            var result = ExceptionHandler.HandleWithReturn<Expression<Func<OperationClaim, bool>>, List<OperationClaim>>((f) =>
            {
                return _operationClaimDal.GetAll(f);
            }, filter);
            if (!result.Success)
            {
                return new ErrorDataResult<List<OperationClaim>>("Beklenmedik bir hata oluştu.Lütfen daha sonra tekrar deneyiniz.");
            }
            return new SuccessDataResult<List<OperationClaim>>(result.Data, "İşlem başarılı");
        }
    }
}
