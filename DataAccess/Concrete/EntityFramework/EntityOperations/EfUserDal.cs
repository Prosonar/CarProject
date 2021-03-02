using Core.DataAccess.EntityFramework;
using Core.Entity.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework.EntityOperations
{
    public class EfUserDal : EfEntityRepositoryBase<User, CarProjectContext>, IUserDal
    {
        public List<OperationClaim> GetClaims(User user)
        {
            using (var context = new CarProjectContext())
            {
                var result = from claim in context.OperationClaims
                             join userClaim in context.UserOperationClaims
                             on claim.Id equals userClaim.UserId
                             where userClaim.Id == user.Id
                             select new OperationClaim { Id = user.Id, Name = claim.Name };
                return result.ToList();
            }
        }
    }
}
