using Core.Utilities.Results.Abstract;
using Entity.Concrete;
using Entity.DTO;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Business.Abstract.Services
{
    public interface ICarService
    {
        IDataResult<List<Car>> GetCars(Expression<Func<Car, bool>> filter = null);
        IDataResult<List<Car>> GetCarsByBrand(int brandId);
        IDataResult<List<Car>> GetCarsByColor(int colorId);
        IDataResult<List<CarDetail>> GetCarWithDetails();
        IDataResult<Car> GetCarById(int carId);
        IResult Add(Car car);
        IResult Delete(Car car);
        IResult Update(Car car);
    }
}
