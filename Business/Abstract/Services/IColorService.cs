using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Business.Abstract.Services
{
    public interface IColorService
    {
        List<Color> GetColors(Expression<Func<Color, bool>> filter = null);
        Color GetById(int colorId);
        void Add(Color color);
        void Delete(Color color);
        void Update(Color color);
    }
}
