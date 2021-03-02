using Core.Entity;
using Core.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Concrete
{
    public class CarImage : IEntity
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
        public DateTime UploadDate { get; set; }
    }
}
