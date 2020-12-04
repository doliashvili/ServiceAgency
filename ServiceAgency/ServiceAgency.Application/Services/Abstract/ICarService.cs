using ServiceAgency.Application.Commands;
using ServiceAgency.Application.Dtos;
using ServiceAgency.Application.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAgency.Application.Services.Abstract
{
    public interface ICarService
    {
        Task AddCarAsync(CarInputDto carInputDto);
        Task UpdateCarColorAsync(ChangeCarColor changeCarColor);
        Task UpdateCarOwnerAsync(ChangeCarOwner changeCarOwner);
        Task UpdateCarTransportNumberAsync(ChangeCarTransportNumber changeCarNumber);
        Task RemoveCarAsync(int id);
        Task<CarOutputDto> GetCarByIdAsync(int id,string lang);
        Task<(IEnumerable<CarOutputDto>, int)> GetSearchedCarsAsync(SearchCars query,string lang);
        Task<(IEnumerable<CarOutputDto>, int)> GetCarsAsync(GetCars query,string lang);
        Task<(CarOwnersOutputDto, int)> GetCarOwners(GetCarOwners query);
    }
}
