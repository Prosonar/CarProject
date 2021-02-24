using Core.Utilities.Results.Abstract;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Business.Abstract.Services
{
    public interface IRentalService 
    {
        IResult Add(Rental rental);
        IResult Update(Rental rental);
        IResult Delete(Rental rental);
        IDataResult<List<Rental>> GetAll(Expression<Func<Rental, bool>> filter = null);
        IDataResult<Rental> GetById(int rentalId);
        IResult ReturnCar(Rental rental);
    }
}
