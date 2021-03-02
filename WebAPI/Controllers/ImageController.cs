using Business.Abstract.Services;
using Entity.Concrete;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private ICarImageService _carImageService;
        private ICarService _carService;
        public ImageController(ICarImageService carImageService, ICarService carService)
        {
            _carImageService = carImageService;
            _carService = carService;
        }

        [HttpPost]
        public IActionResult UploadImage([FromForm] IFormFile file, [FromForm] int carId)
        {
            var carImages = _carImageService.GetAll(x => x.CarId == carId).Data;
            int numberOfCar = carImages.Count + 1;
            string imageName = "CarId-" + carId + "-ImageNo-" + numberOfCar + "-.jpg";
            try
            {
                string oldPath = Path.Combine(@"C:\Users\Casper\source\repos\DemoProject\WebAPI\Image\CarImage\" + file.FileName);
                string newPath = Path.Combine(@"C:\Users\Casper\source\repos\DemoProject\WebAPI\Image\CarImage\" + imageName);
                using (var stream = new FileStream(oldPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                System.IO.File.Move(oldPath, newPath);
                _carImageService.Add(new CarImage
                {
                    CarId = carId,
                    ImageName = imageName,
                    ImagePath = newPath,
                    UploadDate = DateTime.Now
                });
            }
            catch (Exception)
            {
                return BadRequest("Hata çıktı");
            }

            return Ok(file);
        }
        [HttpPost]
        public IActionResult ShowImages(int carId,int imageNumber = 0)
        {
            var carImages = _carImageService.GetAll(x => x.CarId == carId).Data;
            var images = new List<FileStream>();
            foreach (var carImage in carImages)
            {
                var image = System.IO.File.OpenRead(carImage.ImagePath);
                images.Add(image);
            }
            if(images.Count==0)
            {
                images.Add(System.IO.File.OpenRead(@"C:\Users\Casper\source\repos\DemoProject\WebAPI\Image\CarImage\default.jpg"));
            }
            if(imageNumber>images.Count)
            {
                return BadRequest("HATA VAR!");
            }
            return File(images[imageNumber], "image/jpeg");
        }
    }
}
