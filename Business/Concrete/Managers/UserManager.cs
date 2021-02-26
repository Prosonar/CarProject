using Business.Abstract.Services;
using Core.Utilities.ExceptionHandle;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Business.Concrete.Managers
{
    public class UserManager : IUserService
    {
        private IUserDal _userDal;
        private ICustomerService _customerService;

        public UserManager(IUserDal userDal,ICustomerService customerService)
        {
            _userDal = userDal;
            _customerService = customerService;
        }

        public IResult Add(User user)
        {
            var result = ExceptionHandler.HandleWithNoReturn(() => 
            {
                _userDal.Add(user);
            });
            if(!result)
            {
                return new ErrorResult("Beklenmedik bir hata çıktı.Lütfen daha sonra tekrar deneyiniz.");
            }
            return new SuccessResult("İşlem başarılı");
        }

        public IResult Delete(User user)
        {
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                var listOfCustomers = _customerService.GetAll(c => c.UserId == user.Id).Data.Select(c => c.Id).ToList();
                if(listOfCustomers.Count>0)
                {
                    throw new Exception("Bu kullanıcı aynı zamanda bir müşteri.Silme başarısız!");
                }
                _userDal.Delete(user);
            });
            if (!result)
            {
                return new ErrorResult("Beklenmedik bir hata çıktı.Lütfen daha sonra tekrar deneyiniz.");
            }
            return new SuccessResult("İşlem başarılı");
        }

        public IDataResult<List<User>> GetAll(Expression<Func<User, bool>> filter = null)
        {
            var result = ExceptionHandler.HandleWithReturn<Expression<Func<User,bool>>, List<User>>((filter) =>
             {
                 return _userDal.GetAll(filter);
             },filter);
            if(!result.Success)
            {
                return new ErrorDataResult<List<User>>("Beklenmedik bir hata çıktı.Lütfen daha sonra tekrar deneyiniz.");
            }
            return new SuccessDataResult<List<User>>(result.Data, "İşlem başarılı");
        }

        public IDataResult<User> GetById(int userId)
        {
            var result = ExceptionHandler.HandleWithReturn<int,User>((userId) =>
            {
                return _userDal.GetById(u => u.Id == userId);
            },userId);
            if (!result.Success)
            {
                return new ErrorDataResult<User>("Beklenmedik bir hata çıktı.Lütfen daha sonra tekrar deneyiniz.");
            }
            return new SuccessDataResult<User>(result.Data,"İşlem başarılı");
        }

        public IResult Update(User user)
        {
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _userDal.Update(user);
            });
            if (!result)
            {
                return new ErrorResult("Beklenmedik bir hata çıktı.Lütfen daha sonra tekrar deneyiniz.");
            }
            return new SuccessResult("İşlem başarılı");
        }
    }
}
