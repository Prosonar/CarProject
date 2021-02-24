using System;
using System.Collections.Generic;
using System.Text;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entity.Concrete;

namespace DataAccess.Concrete.EntityFramework.EntityOperations
{
    public class EfCustomerDal : EfEntityRepositoryBase<Customer,CarProjectContext>,ICustomerDal
    {
    }
}
