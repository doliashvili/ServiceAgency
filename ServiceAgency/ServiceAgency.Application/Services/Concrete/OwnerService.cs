using ServiceAgency.Application.Dtos;
using ServiceAgency.Application.Services.Abstract;
using ServiceAgency.Domain.Entities;
using ServiceAgency.Domain.Exceptions;
using ServiceAgency.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAgency.Application.Services.Concrete
{
    public class OwnerService : IOwnerService
    {
        private readonly IBaseRepository<Owner> _baseRepository;

        public OwnerService(IBaseRepository<Owner> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task AddOwnerAsync(OwnerInputDto ownerInputDto)
        {
            var ownerEntity = new Owner
            {
                FirstName = ownerInputDto.FirstName,
                IsActive = ownerInputDto.IsActive,
                LastName = ownerInputDto.LastName,
                PrivateNumber = ownerInputDto.PrivateNumber
            };

            await _baseRepository.AddAsync(ownerEntity);
        }

        public async Task DeleteOwnerAsync(int id)
        {
            await _baseRepository.DeleteByIdAsync(id);
        }
    }
}
