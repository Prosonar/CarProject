using Core.DataAccess.BaseRepositories;
using Entity.Concrete;
using Entity.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface ICarDal : IEntityRepository<Car>
    {
        List<CarDetail> GetCarsWithDetails();
    }
}
