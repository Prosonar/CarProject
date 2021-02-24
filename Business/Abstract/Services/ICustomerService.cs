using Core.Utilities.Results.Abstract;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Business.Abstract.Services
{
    public interface ICustomerService
    {
        IResult Add(Customer customer);
        IResult Update(Customer customer);
        IResult Delete(Customer customer);
        IDataResult<List<Customer>> GetAll(Expression<Func<Customer, bool>> filter = null);
        IDataResult<Customer> GetById(int customerId);
    }
}
