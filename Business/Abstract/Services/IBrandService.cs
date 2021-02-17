using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Business.Abstract.Services
{
    public interface IBrandService
    {
        List<Brand> GetBrands(Expression<Func<Brand, bool>> filter = null);
        Brand GetById(int brandId);
        void Add(Brand brand);
        void Delete(Brand brand);
        void Update(Brand brand);
    }
}
