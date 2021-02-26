using Core.DataAccess.BaseRepositories;
using Entity.Concrete;
using Entity.DTO;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Abstract
{
    public interface ICarDal : IEntityRepository<Car>
    {
        List<CarDetail> GetCarsWithDetails(Expression<Func<Car, bool>> filter = null);
    }
}
