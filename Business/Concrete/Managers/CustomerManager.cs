using Business.Abstract.Services;
using Business.BusinessAspects.Autofac;
using Business.Utilities.Messages.TurkishMessages;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
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

        [ValidationAspect(typeof(CustomerValidator), Priority = 4)]
        [AuthAspect("admin",Priority = 5)]
        [CacheRemoveAspect("ICustomerService.Get", Priority = 3)]
        public IResult Add(Customer customer)
        {
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _customerDal.Add(customer);
            });
            if (!result)
            {
                return new ErrorResult(Messages.ErrorMessage);
            }
            return new SuccessResult(Messages.SuccessMessage);
        }
        [AuthAspect("admin",Priority = 5)]
        [CacheRemoveAspect("ICustomerService.Get", Priority = 3)]
        public IResult Delete(Customer customer)
        {
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _customerDal.Delete(customer);
            });
            if (!result)
            {
                return new ErrorResult(Messages.ErrorMessage);
            }
            return new SuccessResult(Messages.SuccessMessage);
        }
        [AuthAspect("admin",Priority = 5)]
        [CacheAspect]
        public IDataResult<List<Customer>> GetAll(Expression<Func<Customer, bool>> filter = null)
        {
            var result = ExceptionHandler.HandleWithReturn<Expression<Func<Customer, bool>>, List<Customer>>((userId) =>
            {
                return _customerDal.GetAll(filter);
            }, filter);
            if (!result.Success)
            {
                return new ErrorDataResult<List<Customer>>(Messages.ErrorMessage);
            }
            return new SuccessDataResult<List<Customer>>(result.Data, Messages.SuccessMessage);
        }
        [AuthAspect("admin",Priority = 5)]
        [CacheAspect]
        public IDataResult<Customer> GetById(int customerId)
        {
            var result = ExceptionHandler.HandleWithReturn<int, Customer>((customerId) =>
            {
                return _customerDal.GetById(u => u.Id == customerId);
            }, customerId);
            if (!result.Success)
            {
                return new ErrorDataResult<Customer>(Messages.ErrorMessage);
            }
            return new SuccessDataResult<Customer>(result.Data, Messages.SuccessMessage);
        }

        [ValidationAspect(typeof(CustomerValidator), Priority = 4)]
        [AuthAspect("admin",Priority = 5)]
        [CacheRemoveAspect("ICustomerService.Get", Priority = 3)]
        public IResult Update(Customer customer)
        {
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _customerDal.Update(customer);
            });
            if (!result)
            {
                return new ErrorResult(Messages.ErrorMessage);
            }
            return new SuccessResult(Messages.SuccessMessage);
        }
    }
}
