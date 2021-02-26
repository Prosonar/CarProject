using Core.DataAccess.BaseRepositories;
using Entity.Concrete;
using Entity.DTO;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IRentalDal : IEntityRepository<Rental>
    {
        List<RentalDetail> GetRentalWithDetail(Expression<Func<Rental, bool>> filter = null);
    }
}
