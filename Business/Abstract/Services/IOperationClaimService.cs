using Core.Entity.Concrete;
using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Business.Abstract.Services
{
    public interface IOperationClaimService 
    {
        IResult Add(OperationClaim operationClaim);
        IDataResult<List<OperationClaim>> GetAll(Expression<Func<OperationClaim, bool>> filter = null);
    }
}
