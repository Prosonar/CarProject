using Core.Utilities.Results.Abstract;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Business.Abstract.Services
{
    public interface IColorService
    {
        IDataResult<List<Color>> GetColors(Expression<Func<Color, bool>> filter = null);
        IDataResult<Color> GetById(int colorId);
        IResult Add(Color color);
        IResult Delete(Color color);
        IResult Update(Color color);
    }
}
