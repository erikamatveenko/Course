using AccountingOfVehicles.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingOfVehicles.Data
{
    public class CarExternalFile
    {
        private IHostingEnvironment _environment;
        private IConfiguration _iconfiguration;
        public CarExternalFile()
        {

        }
        public CarExternalFile(IHostingEnvironment environment, IConfiguration iconfiguration)
        {
            _environment = environment;
            _iconfiguration = iconfiguration;

        }

        public async Task<Car> UploadRectorWithPhoto(Car car, IFormFile upload)
        {
            string relativeFileName = "";
            string absoluteFileName = "";
            if (upload != null)
            {
                relativeFileName = _iconfiguration.GetSection("Paths").GetSection("PathToPhotos").Value + car.CarID.ToString() + upload.FileName;
                car.CarPhoto = relativeFileName;
                absoluteFileName = _environment.WebRootPath + relativeFileName;
                using (var fileStream = new FileStream(absoluteFileName, FileMode.Create))
                {
                    await upload.CopyToAsync(fileStream);
                }

            }
            return car;
        }

    }
}
