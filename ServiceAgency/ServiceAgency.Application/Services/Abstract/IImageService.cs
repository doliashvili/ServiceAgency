using Microsoft.AspNetCore.Http;
using ServiceAgency.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAgency.Application.Services.Abstract
{
    public interface IImageService 
    {
        Task<string> AddImageAsync(IFormFile data);
        Task<byte[]> GetImage(string filename);
        void Delete(string filename);
    }
}
