using Core.Utilities.Results.Abstract;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Business.Abstract.Services
{
    public interface IUserService
    {
        IResult Add(User user);
        IResult Update(User user);
        IResult Delete(User user);
        IDataResult<List<User>> GetAll(Expression<Func<User, bool>> filter = null);
        IDataResult<User> GetById(int userId);
    }
}
