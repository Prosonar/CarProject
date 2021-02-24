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
    public class RentalManager : IRentalService
    {
        private IRentalDal _rentalDal;

        public RentalManager(IRentalDal rentalDal)
        {
            _rentalDal = rentalDal;
        }

        public IResult Add(Rental rental)
        {
            
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                List<Rental> rentals = _rentalDal.GetAll(r => r.CarId == rental.CarId);
                if (rentals.Count != 0)
                {
                    Rental rental2 = rentals[rentals.Count - 1];
                    if (rental2.IsAvaible == false)
                    {
                        throw new Exception("Araba şuanda kiralanamaz!");
                    }
                }
                _rentalDal.Add(rental);
            });
            if (!result)
            {
                return new ErrorResult("Beklenmedik bir hata çıktı.Lütfen daha sonra tekrar deneyiniz.");
            }
            return new SuccessResult("İşlem başarılı");
        }

        public IResult Delete(Rental rental)
        {
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _rentalDal.Delete(rental);
            });
            if (!result)
            {
                return new ErrorResult("Beklenmedik bir hata çıktı.Lütfen daha sonra tekrar deneyiniz.");
            }
            return new SuccessResult("İşlem başarılı");
        }

        public IDataResult<List<Rental>> GetAll(Expression<Func<Rental, bool>> filter = null)
        {
            var result = ExceptionHandler.HandleWithReturn<Expression<Func<Rental, bool>>, List<Rental>>((userId) =>
            {
                return _rentalDal.GetAll(filter);
            }, filter);
            if (!result.Success)
            {
                return new ErrorDataResult<List<Rental>>("Beklenmedik bir hata çıktı.Lütfen daha sonra tekrar deneyiniz.");
            }
            return new SuccessDataResult<List<Rental>>(result.Data, "İşlem başarılı");
        }

        public IDataResult<Rental> GetById(int rentalId)
        {
            var result = ExceptionHandler.HandleWithReturn<int, Rental>((userId) =>
            {
                return _rentalDal.GetById(u => u.Id == rentalId);
            }, rentalId);
            if (!result.Success)
            {
                return new ErrorDataResult<Rental>("Beklenmedik bir hata çıktı.Lütfen daha sonra tekrar deneyiniz.");
            }
            return new SuccessDataResult<Rental>(result.Data, "İşlem başarılı");
        }

        public IResult ReturnCar(Rental rental)
        {
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _rentalDal.Update(new Rental 
                { 
                    Id = rental.Id, 
                    IsAvaible = true ,
                    ReturnDate=DateTime.Now,
                    RentDate=rental.RentDate,
                    CarId=rental.CarId,
                    CustomerId=rental.CustomerId
                });
            });
            if(!result)
            {
                return new ErrorResult("Beklenmedik bir hata çıktı.Lütfen daha sonra deneyiniz.");
            }
            return new SuccessResult("İşlem başarılı.");
        }

        public IResult Update(Rental rental)
        {
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _rentalDal.Update(rental);
            });
            if (!result)
            {
                return new ErrorResult("Beklenmedik bir hata çıktı.Lütfen daha sonra tekrar deneyiniz.");
            }
            return new SuccessResult("İşlem başarılı");
        }
    }
}
