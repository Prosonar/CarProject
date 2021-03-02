using Core.Entity;
using Core.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Concrete
{
    public class Car : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BrandId { get; set; }
        public int ColorId { get; set; }
        public decimal DailyPrice { get; set; }
        public string Description { get; set; }
        //public DateTime ModelYear { get; set; }
        public bool IsAvailable { get; set; }
    }
}
