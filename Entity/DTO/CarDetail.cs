using Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.DTO
{
    public class CarDetail : IDto
    {
        public int Id { get; set; }
        public string CarName { get; set; }
        public string BrandName { get; set; }
        public string ColorName { get; set; }
        public decimal DailyPrice { get; set; }
        public string Description { get; set; }
        //public DateTime ModelYear { get; set; }
    }
}
