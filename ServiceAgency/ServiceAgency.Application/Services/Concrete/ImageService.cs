using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using ServiceAgency.Application.Dtos;
using ServiceAgency.Application.Services.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAgency.Application.Services.Concrete
{
    public class ImageService : IImageService
    {
        private readonly string folderPath;
        public ImageService(IConfiguration configuration)
        {
            folderPath = configuration["ImageFolder"];
        }
        public async Task<string> AddImageAsync(IFormFile data)
        {
            var extension = System.IO.Path.GetExtension(data.FileName).ToLower();
            string filename = System.IO.Path.GetFileNameWithoutExtension(data.FileName);
            filename = filename + "_" + Guid.NewGuid().ToString().Split('-').First();

            CreateDirectoryIfNotExists(folderPath);

            var imagePath = System.IO.Path.Combine(folderPath, filename);

            using (System.IO.FileStream output = System.IO.File.Create(imagePath + extension))
                await data.CopyToAsync(output);

            return imagePath;
        }

        public void Delete(string filename)
        {
            File.Delete(Path.Combine(folderPath, filename));
        }

        public async Task<byte[]> GetImage(string filename)
        {
            var fullPath = Path.Combine(folderPath, filename);
            return await File.ReadAllBytesAsync(fullPath);
        }

        private void CreateDirectoryIfNotExists(string dirPath)
        {
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
        }
    }
}
