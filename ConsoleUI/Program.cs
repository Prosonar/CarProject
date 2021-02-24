using Business.Concrete.Managers;
using DataAccess.Concrete.EntityFramework.EntityOperations;
using Entity.Concrete;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            //CarManagerTest();
            //ColorManagerTest();
            //BrandManagerTest();
            //UserManagerTest();
            //CustomerManagerTest();
            //RentalManagerTest();
            Console.ReadLine();
        }

        private static void RentalManagerTest()
        {
            RentalManager rentalManager = new RentalManager(new EfRentalDal());
            rentalManager.Add(new Rental { CarId = 1, CustomerId = 1, RentDate = DateTime.Now, IsAvaible = false });
            rentalManager.Add(new Rental { CarId = 2, CustomerId = 2, RentDate = DateTime.Now, IsAvaible = false });
            var result = rentalManager.Add(new Rental { CarId = 1, CustomerId = 1, RentDate = DateTime.Now, IsAvaible = false });
            var result2 = rentalManager.ReturnCar(rentalManager.GetById(2).Data);
            rentalManager.ReturnCar(rentalManager.GetById(4).Data);
            var result3 = rentalManager.Add(new Rental { CarId = 2, CustomerId = 3, RentDate = DateTime.Now, IsAvaible = false });
            Console.WriteLine(result3.Message);
        }

        private static void CustomerManagerTest()
        {
            CustomerManager customerManager = new CustomerManager(new EfCustomerDal());
            customerManager.Add(new Customer { UserId = 1, CompanyName = "Microsoft" });
            customerManager.Add(new Customer { UserId = 2, CompanyName = "Google" });
            customerManager.Add(new Customer { UserId = 3, CompanyName = "Yahoo" });
            customerManager.Add(new Customer { UserId = 4, CompanyName = "Dropbox" });
            customerManager.Add(new Customer { UserId = 5, CompanyName = "Intel" });
            foreach (var item in customerManager.GetAll().Data)
            {
                Console.WriteLine(item.CompanyName);
            }
        }

        private static void UserManagerTest()
        {
            UserManager userManager = new UserManager(new EfUserDal());
            //userManager.Add(new User { FirstName = "Ali", LastName = "Veli", Email = "asdasd@gmail.com", Password = "ASDASDASD" });
            //userManager.Add(new User { FirstName = "Ali2", LastName = "Veli2", Email = "2asdasd@gmail.com", Password = "ASDASDASD" });
            //userManager.Add(new User { FirstName = "Ali3", LastName = "Veli3", Email = "3asdasd@gmail.com", Password = "ASDASDASD" });
            //userManager.Add(new User { FirstName = "Ali4", LastName = "Veli4", Email = "4asdasd@gmail.com", Password = "ASDASDASD" });
            //userManager.Add(new User { FirstName = "Ali5", LastName = "Veli5", Email = "5asdasd@gmail.com", Password = "ASDASDASD" });
            User user = userManager.GetById(3).Data;
            //Console.WriteLine(user.FirstName);
            foreach (var item in userManager.GetAll().Data)
            {
                Console.WriteLine(item.FirstName);
            }
        }

        private static void BrandManagerTest()
        {
            BrandManager brandManager = new BrandManager(new EfBrandDal());
            brandManager.Add(new Brand { Name = "Mercedes" });
            brandManager.Add(new Brand { Name = "Ford" });
            brandManager.Add(new Brand { Name = "BMW" });
            brandManager.Add(new Brand { Name = "Ferrari" });
            brandManager.Add(new Brand { Name = "Volvo" });
            //foreach (var brand in brandManager.GetBrands())
            //{
            //    Console.WriteLine(brand.Name);
            //}
        }

        private static void ColorManagerTest()
        {
            ColorManager colorManager = new ColorManager(new EfColorDal());
            colorManager.Add(new Color { Name = "Mavi" });
            colorManager.Add(new Color { Name = "Siyah" });
            colorManager.Add(new Color { Name = "Kırmızı" });
            colorManager.Add(new Color { Name = "Beyaz" });
            //foreach (var color in colorManager.GetColors())
            //{
            //    Console.WriteLine(color.Name);
            //}
        }

        private static void CarManagerTest()
        {
            CarManager carManager = new CarManager(new EfCarDal());
            carManager.Add(new Car { ColorId = 1, BrandId = 1, Name = "Araba1", DailyPrice = 250 });
            carManager.Add(new Car { ColorId = 2, BrandId = 2, Name = "Araba2", DailyPrice = 200 });
            carManager.Add(new Car { ColorId = 3, BrandId = 3, Name = "Araba3", DailyPrice = 300 });
            carManager.Add(new Car { ColorId = 4, BrandId = 4, Name = "Araba4", DailyPrice = 225 });
            //foreach (var car in carManager.GetCarWithDetails())
            //{
            //    Console.WriteLine("{0} | {1} | {2} ", car.CarName, car.BrandName, car.ColorName);
            //}
        }
    }
}
