using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceAgency.Application.Services.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceAgency.Api.Controllers
{
    public class FileController : ControllerBase
    {
        private readonly IImageService _imageService;

        public FileController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile formFile)
        {
            var str = await _imageService.AddImageAsync(formFile);
            return Ok(str);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetImage(string filename)
        {
            var extension = Path.GetExtension(filename);
            var contentType = $"image/{extension}";
            var bytes= await _imageService.GetImage(filename);
            return File(bytes, contentType);
        }
    }
}
