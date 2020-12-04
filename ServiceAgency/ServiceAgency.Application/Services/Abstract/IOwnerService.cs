using ServiceAgency.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAgency.Application.Services.Abstract
{
    public interface IOwnerService 
    {
        Task AddOwnerAsync(OwnerInputDto ownerInputDto);
        Task DeleteOwnerAsync(int id);
    }
}
