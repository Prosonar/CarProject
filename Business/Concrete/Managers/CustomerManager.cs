using Business.Abstract.Services;
using Core.Utilities.ExceptionHandle;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Business.Concrete.Managers
{
    public class CustomerManager : ICustomerService
    {
        private ICustomerDal _customerDal;

        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }

        public IResult Add(Customer customer)
        {
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _customerDal.Add(customer);
            });
            if (!result)
            {
                return new ErrorResult("Beklenmedik bir hata çıktı.Lütfen daha sonra tekrar deneyiniz.");
            }
            return new SuccessResult("İşlem başarılı");
        }

        public IResult Delete(Customer customer)
        {
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _customerDal.Delete(customer);
            });
            if (!result)
            {
                return new ErrorResult("Beklenmedik bir hata çıktı.Lütfen daha sonra tekrar deneyiniz.");
            }
            return new SuccessResult("İşlem başarılı");
        }

        public IDataResult<List<Customer>> GetAll(Expression<Func<Customer, bool>> filter = null)
        {
            var result = ExceptionHandler.HandleWithReturn<Expression<Func<Customer, bool>>, List<Customer>>((userId) =>
            {
                return _customerDal.GetAll(filter);
            }, filter);
            if (!result.Success)
            {
                return new ErrorDataResult<List<Customer>>("Beklenmedik bir hata çıktı.Lütfen daha sonra tekrar deneyiniz.");
            }
            return new SuccessDataResult<List<Customer>>(result.Data, "İşlem başarılı");
        }

        public IDataResult<Customer> GetById(int customerId)
        {
            var result = ExceptionHandler.HandleWithReturn<int, Customer>((customerId) =>
            {
                return _customerDal.GetById(u => u.Id == customerId);
            }, customerId);
            if (!result.Success)
            {
                return new ErrorDataResult<Customer>("Beklenmedik bir hata çıktı.Lütfen daha sonra tekrar deneyiniz.");
            }
            return new SuccessDataResult<Customer>(result.Data, "İşlem başarılı");
        }

        public IResult Update(Customer customer)
        {
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _customerDal.Update(customer);
            });
            if (!result)
            {
                return new ErrorResult("Beklenmedik bir hata çıktı.Lütfen daha sonra tekrar deneyiniz.");
            }
            return new SuccessResult("İşlem başarılı");
        }
    }
}
