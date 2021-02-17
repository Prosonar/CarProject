using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework.EntityOperations
{
    public class EfColorDal : EfEntityRepositoryBase<Color, CarProjectContext>,IColorDal
    {
    }
}
