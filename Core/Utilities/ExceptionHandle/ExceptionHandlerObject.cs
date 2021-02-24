using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.ExceptionHandle
{
    public class ExceptionHandlerObject<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
    }
}
