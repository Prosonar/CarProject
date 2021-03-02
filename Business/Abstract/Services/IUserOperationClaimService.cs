using Core.Entity.Concrete;
using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Business.Abstract.Services
{
    public interface IUserOperationClaimService
    {
        IResult Add(UserOperationClaim operationClaim);
        IDataResult<List<UserOperationClaim>> GetAll(Expression<Func<UserOperationClaim, bool>> filter = null);
    }
}
