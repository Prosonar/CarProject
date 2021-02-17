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
            Console.ReadLine();
        }

        private static void BrandManagerTest()
        {
            BrandManager brandManager = new BrandManager(new EfBrandDal());
            brandManager.Add(new Brand { Name = "Mercedes" });
            brandManager.Add(new Brand { Name = "Ford" });
            brandManager.Add(new Brand { Name = "BMW" });
            brandManager.Add(new Brand { Name = "Ferrari" });
            brandManager.Add(new Brand { Name = "Volvo" });
            foreach (var brand in brandManager.GetBrands())
            {
                Console.WriteLine(brand.Name);
            }
        }

        private static void ColorManagerTest()
        {
            ColorManager colorManager = new ColorManager(new EfColorDal());
            colorManager.Add(new Color { Name = "Mavi" });
            colorManager.Add(new Color { Name = "Siyah" });
            colorManager.Add(new Color { Name = "Kırmızı" });
            colorManager.Add(new Color { Name = "Beyaz" });
            foreach (var color in colorManager.GetColors())
            {
                Console.WriteLine(color.Name);
            }
        }

        private static void CarManagerTest()
        {
            CarManager carManager = new CarManager(new EfCarDal());
            carManager.Add(new Car { ColorId = 1, BrandId = 1, Name = "Araba1", DailyPrice = 250 });
            carManager.Add(new Car { ColorId = 2, BrandId = 2, Name = "Araba2", DailyPrice = 200 });
            carManager.Add(new Car { ColorId = 3, BrandId = 3, Name = "Araba3", DailyPrice = 300 });
            carManager.Add(new Car { ColorId = 4, BrandId = 4, Name = "Araba4", DailyPrice = 225 });
            foreach (var car in carManager.GetCarWithDetails())
            {
                Console.WriteLine("{0} | {1} | {2} ", car.CarName, car.BrandName, car.ColorName);
            }
        }
    }
}
