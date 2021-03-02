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
    public class UserOperationClaimManager : IUserOperationClaimService
    {
        private IUserOperationClaimDal _userOperationClaimDal;
        public UserOperationClaimManager(IUserOperationClaimDal userOperationClaimDal)
        {
            _userOperationClaimDal = userOperationClaimDal;
        }
        public IResult Add(UserOperationClaim userOperationClaim)
        {
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _userOperationClaimDal.Add(userOperationClaim);
            });
            if(result)
            {
                return new ErrorResult("Beklenmedik bir hata oluştu.Lütfen daha sonra tekrar deneyiniz.");
            }
            return new SuccessResult("İşlem başarılı");
        }

        public IDataResult<List<UserOperationClaim>> GetAll(Expression<Func<UserOperationClaim, bool>> filter = null)
        {
            var result = ExceptionHandler.HandleWithReturn<Expression<Func<UserOperationClaim,bool>>,List<UserOperationClaim>>((f) =>
            {
                return _userOperationClaimDal.GetAll(f);
            },filter);
            if (!result.Success)
            {
                return new ErrorDataResult<List<UserOperationClaim>>("Beklenmedik bir hata oluştu.Lütfen daha sonra tekrar deneyiniz.");
            }
            return new SuccessDataResult<List<UserOperationClaim>>(result.Data,"İşlem başarılı");
        }
    }
}
