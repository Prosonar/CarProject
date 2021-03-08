using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Utilities.Messages.TurkishMessages
{
    public static class Messages
    {
        public static string ErrorMessage = "Beklenmedik bir hata çıktı.Lütfen daha sonra tekrar deneyiniz.";
        public static string SuccessMessage = "İşlem başarılı";
        public static string ForeignKeyMessage = "Bu veri bir yabancı anahtar olarak kullanılıyor.";
        public static string BusinessRuleError = "İş kurallarına uymayan işlem var.";

        //Car Messages
        public static string CarNotExist = "Araç bulunamadı";
        public static string CarNotAvaiblable = "Araç şuanda ya kiralanmış ya da bakımda";
        public static string CarImageCount = "Bir aracın en fazla 5 aadet resmi olabilir.";

        //User Messages
        public static string UserAlreadyExist = "Bu eposta adresiyle kayıtlı zaten bir kullanıcı mevcuttur.";
        public static string UserNotFound = "Böyle bir kullanıcı bulunamadı";
        public static string WrongData = "Kullanıcı epostası ya da şifresi hatalı.";
        public static string TokenCreated = "Kullanıcı giriş jetonu oluşturuldu.";
        public static string EmailAvailable = "Kullanıcı sisteme kayıt olabilir.";

        //Brand Messages
        //public static string BrandAlreadyUse = ""
    }
}
