# CarProject
Rent a car platform

Bu proje C# ile yazılmış bir projedir.Proje aslında farklı kaatmanlardan oluşsa da bu bir Web Api'dir.Bu api sayesinde araba kiralama ile ilgili çeşitli işlemler yapabilirsiniz.
Projenin içerisinde çeşitli varlıklar mevcut bunlar temel olarak araba,marka,renk,kullanıcı,müşteri...Aynı zamanda ikili ilişkileri gösteren varlıklar da mevcut.Bu proje tüm 
için ekleme silme güncelleme ve listeleme gibi temel veritabanı işlemleri yapabilirsiniz.Bu projenin yaptığı asıl iş araba kiralama süreçini kontrol etmektir.Bir müşteri gelir 
ve arabalardan birini kiralar daha sonra arabayı geri getirir.Bu aradaki süreçleri bu api ile kontrol edebilirsiniz.

Bu web api sayesinde arabalara resim gibi verilerde ekleyebilirsiniz.Aynı zamanda sorulamalar yapabilirsiniz.Proje SOLID kurallarına uygun bir şekilde yapıldığından istediğiniz 
şekilde kodları güncelleyebilir ya da farklı kodlar ekleyebilirsiniz.Çünkü kodlarda interface'ler kullanıldı.

Teknik olarak konuşacak olursak bu web api SOLID e uygun bir şekilde farklı tasarım desenleri kullanılarak katmanlı bir mimari ile oluşturuldu.Bu katmanlar entity,dataaccess,
business,core ve api katmanlarından oluşuyor.

Entity katmanında projenin varlıkları bulunuyor.Her katmanda olduğu gibi bu katmanda da dosyalamalara dikkat edilmiştir.

Dataaccess katmanında ayrı klasörlerde bulunmak üzere her varlık için bir interface tanımı yaptım ve bu interfaceleri ana bir interface te bağladım.Bunun mantığı kodu tek bir yerden kontrol 
altına almaktır.Dataaccess katmanında ayrıca her varlık için temel veritabanı işlemleri yapan sınıflar mevcuttur.

Business katmanında her valığın hem servis interface'i hem de manager sınıfı bulunuyor.Bu katmanın görevi verileri işlemeden önce iş kurallarına uygunluğunu test etmektir.

Core katmanı ise projenin kalbi niteliğindedir.Her katmanda kullanılabilecek projeden bağımsız kodların tümü bu katmanda bulunuyor.Yani bu katman bağımsız katman yaptığı iş ise 
diğer katmanlara yardım etmektir.Bu katmanı herhangi başka bir projeye dahil edip gerekli konfigürasyonları yapıp rahatlıkla kullanabilirsiniz.Bu katmanda generic repository'ler,
temel interfaceler,diğer katmanların kullanacağı kodlar,gerekli işlemleri yapan sınıflar mevcuttur.Örneğin bu katmanda web apiye bağlantı için güvenlik amacıyla kullanılan 
json web token ile ilgili tüm şifreleme,tuzlama,kontrol gibi kodlar araçlar klasöründe bulunuyor.Ayrıca bu katmanda aspectler bulunuyor.Bu proje aspectler ile daha sade ve daha 
okunabilir bir proje oldu.Şuanlık validasyon,cache ve güvenlik aspectleri bulunuyor fakat var olan altyapı ile rahatlıkla yeni aspectler kodlanabilir.

Web api katmanında ise mevcut olan varlıkların işlemleri yapabileceğiniz contollerlar ve action methodlar bulunuyor.Bu katmanda giriş için json web token kullanılıyor.Ayrıca araba 
nesnesine resim atayabilmek için bir altyapı da mevcut.Kısaca projede bulunan katmanlar böyle.

Veritabanı için MsSql kullandım.Veri tabanı işlemleri için ise entity framework core kullandım.Bunun sebebi ise entity framework'ün migration özelliğini kullanıp hemen veritabanı
oluşturabilmek ve projeyi kullanabilmektir.

Daha fazla bilgi almak için bana sukrusonar245@gmail.com adresinden ulaşabilirsiniz.
