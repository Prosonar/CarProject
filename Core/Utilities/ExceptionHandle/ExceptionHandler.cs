using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.ExceptionHandle
{
    public static class ExceptionHandler
    {
        /// <summary>
        /// Değer almayan ve geriye bir şey döndürmeyen fonksiyonlar için hata kontrolü yapar.
        /// </summary>
        /// <param name="action">Çalışacak fonksiyonun kendisi</param>
        /// <returns></returns>
        public static bool HandleWithNoReturn(Action action)
        {
            try
            {
                action.Invoke();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// Parametre alan ve geriye birdeğer döndüren fonksiyonlar için hata kontrolü yapar.
        /// </summary>
        /// <typeparam name="T1">Çalışacak fonksiyonun alacağı verinin türü</typeparam>
        /// <typeparam name="T2">Çalışacak fonksiyonun geri dönüş değeri</typeparam>
        /// <param name="action">Çalışacak fonksiyonun kendisi</param>
        /// <param name="args">Çalışacak fonksiyonun parametresi</param>
        /// <returns></returns>
        public static ExceptionHandlerObject<T2> HandleWithReturn<T1,T2>(Func<T1, T2> action,T1 args)
        {
            var result = new ExceptionHandlerObject<T2>();
            try
            {
                var data = action(args);
                result.Data = data;
                result.Success = true;
                return result;
            }
            catch (Exception)
            {
                result.Success = false;
                return result;
            }
        }
        /// <summary>
        /// Parametresi olmayan ama geriye değer döndüren fonksiyonların hata kontrolünü yapar.
        /// </summary>
        /// <typeparam name="T">Çalışacak fonksiyonun geriye döndürdüğü veri tipi</typeparam>
        /// <param name="action">Çalışacak olan fonksiyonun kendisi.</param>
        /// <returns></returns>
        public static ExceptionHandlerObject<T> HandleWithReturnNoParameter<T>(Func<T> action)
        {
            var result = new ExceptionHandlerObject<T>();
            try
            {
                var data = action();
                result.Data = data;
                result.Success = true;
                return result;
            }
            catch (Exception)
            {
                result.Success = false;
                return result;
            }
        }
    }
}
