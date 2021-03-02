using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Core.Utilities.Security.Hashing
{
    public class HashingHelper
    {
        /// <summary>
        /// Şifreleri hashlemeye yarar.passwordHash ve passwordSalt verileri boş gönderiliyor.
        /// </summary>
        /// <param name="password">Hashlenecek şifre</param>
        /// <param name="passwordHash">Şifre hashlendikten sonra bu byte dizisine atanıyor.</param>
        /// <param name="passwordSalt">Şifre hashlenmeden önce bu byte dizisiyle tuzlanıyor ve bu değişkene atanıyor.</param>
        public static void CreatePasswordHash(string password,out byte[] passwordHash,out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
        /// <summary>
        /// Kullanıcı giriş yaparken girdiği şifre hashlenip sistemdeki 
        /// daha önceden hashlenmiş şifresiyle kıyaslanıyor ve doğru girilip girilmediği söylüyor.
        /// </summary>
        /// <param name="password">Giriş yapılırken girilen şifre</param>
        /// <param name="passwordHash">Kullanıcının sistemdeki hashlenmiş şifresi</param>
        /// <returns>Kullanıcı sisteme girerken doğru şifreyi girdiyse true aksi halde false döner.</returns>
        public static bool VerifyPasswordHash(string password,byte[] passwordHash,byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computeHash.Length; i++)
                {
                    if(computeHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
