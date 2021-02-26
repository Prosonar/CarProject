using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entity.Concrete;
using Entity.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework.EntityOperations
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, CarProjectContext>, IRentalDal
    {
        public List<RentalDetail> GetRentalWithDetail(Expression<Func<Rental, bool>> filter = null)
        {
            List<Rental> rentals = this.GetAll(filter);
            using (CarProjectContext context = new CarProjectContext())
            {
                var result = from c in context.Cars
                             join b in context.Brands
                             on c.BrandId equals b.Id
                             join r in context.Colors
                             on c.ColorId equals r.Id
                             select new 
                             {
                                 Id = c.Id,
                                 CarName = c.Name,
                                 BrandName = b.Name,
                                 ColorName = r.Name,
                             };
                var rentDetails = from rental in rentals
                                  join car in result
                                  on rental.CarId equals car.Id
                                  join customer in context.Customers
                                  on rental.CustomerId equals customer.Id
                                  select new RentalDetail
                                  {
                                      Id = rental.Id,
                                      CarName = car.CarName,
                                      BrandName = car.BrandName,
                                      ColorName = car.ColorName,
                                      CompanyName = customer.CompanyName,
                                      RentDate = rental.RentDate,
                                      ReturnDate = rental.ReturnDate
                                  };
                return rentDetails.ToList();
            }
            
        }
    }
}
