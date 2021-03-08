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

        [ValidationAspect(typeof(UserValidator), Priority = 4)]
        [AuthAspect("admin",Priority = 5)]
        [CacheRemoveAspect("IUserService.Get", Priority = 3)]
        public IResult Add(User user)
        {
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _userDal.Add(user);
            });
            if (!result)
            {
                return new ErrorResult(Messages.ErrorMessage);
            }
            return new SuccessResult(Messages.SuccessMessage);
        }
        [AuthAspect("admin",Priority = 5)]
        [CacheRemoveAspect("IUserService.Get", Priority = 3)]
        public IResult Delete(User user)
        {
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                var listOfCustomers = _customerService.GetAll(c => c.UserId == user.Id).Data.Select(c => c.Id).ToList();
                if(listOfCustomers.Count>0)
                {
                    throw new Exception(Messages.ForeignKeyMessage);
                }
                _userDal.Delete(user);
            });
            if (!result)
            {
                return new ErrorResult(Messages.ErrorMessage);
            }
            return new SuccessResult(Messages.SuccessMessage);
        }
        [AuthAspect("admin",Priority = 5)]
        [CacheAspect]
        public IDataResult<List<User>> GetAll(Expression<Func<User, bool>> filter = null)
        {
            var result = ExceptionHandler.HandleWithReturn<Expression<Func<User,bool>>, List<User>>((filter) =>
             {
                 return _userDal.GetAll(filter);
             },filter);
            if(!result.Success)
            {
                return new ErrorDataResult<List<User>>(Messages.ErrorMessage);
            }
            return new SuccessDataResult<List<User>>(result.Data, Messages.SuccessMessage);
        }
        [AuthAspect("admin",Priority = 5)]
        [CacheAspect]
        public IDataResult<User> GetByEmail(string email)
        {
            var result = ExceptionHandler.HandleWithReturn<string, User>((e) =>
            {
                return _userDal.GetAll(u => u.Email == e)[0];
            }, email);
            if (!result.Success)
            {
                return new ErrorDataResult<User>(Messages.ErrorMessage);
            }
            return new SuccessDataResult<User>(result.Data, Messages.SuccessMessage);
        }
        [AuthAspect("admin",Priority = 5)]
        [CacheAspect]
        public IDataResult<User> GetById(int userId)
        {
            var result = ExceptionHandler.HandleWithReturn<int,User>((userId) =>
            {
                return _userDal.GetById(u => u.Id == userId);
            },userId);
            if (!result.Success)
            {
                return new ErrorDataResult<User>(Messages.ErrorMessage);
            }
            return new SuccessDataResult<User>(result.Data, Messages.SuccessMessage);
        }
        [AuthAspect("admin",Priority = 5)]
        [CacheAspect]
        public IDataResult<List<OperationClaim>> GetClaims(User user)
        {
            var result = ExceptionHandler.HandleWithReturn<User, List<OperationClaim>>((u) =>
            {
                return _userDal.GetClaims(u);
            }, user);
            if (!result.Success)
            {
                return new ErrorDataResult<List<OperationClaim>>(Messages.ErrorMessage);
            }
            return new SuccessDataResult<List<OperationClaim>>(result.Data, Messages.SuccessMessage);
        }

        [ValidationAspect(typeof(UserValidator), Priority = 4)]
        [AuthAspect("admin",Priority = 5)]
        [CacheRemoveAspect("IUserService.Get", Priority = 3)]
        public IResult Update(User user)
        {
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _userDal.Update(user);
            });
            if (!result)
            {
                return new ErrorResult(Messages.ErrorMessage);
            }
            return new SuccessResult(Messages.SuccessMessage);
        }
    }
}
