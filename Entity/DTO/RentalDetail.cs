using Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.DTO
{
    public class RentalDetail : IDto
    {
        public int Id { get; set; }
        public int MyProperty { get; set; }
    }
}
