using Business.Abstract.Services;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Business.Concrete.Managers
{
    public class ColorManager : IColorService
    {
        private IColorDal _colorDal;

        public ColorManager(IColorDal colorDal)
        {
            _colorDal = colorDal;
        }

        public IResult Add(Color color)
        {
            _colorDal.Add(color);
            return new SuccessResult("Renk eklendi!");
        }

        public IResult Delete(Color color)
        {
            _colorDal.Delete(color);
            return new SuccessResult("Renk silindi!");
        }

        public IDataResult<Color> GetById(int colorId)
        {
            return new SuccessDataResult<Color>(_colorDal.GetById(c => c.Id == colorId),"Renk bulundu.");
        }

        public IDataResult<List<Color>> GetColors(Expression<Func<Color, bool>> filter = null)
        {
            return new SuccessDataResult<List<Color>>(_colorDal.GetAll(filter),"Renkler listelendi.");
        }

        public IResult Update(Color color)
        {
            _colorDal.Update(color);
            return new SuccessResult("Renk güncellendi!");
        }
    }
}
