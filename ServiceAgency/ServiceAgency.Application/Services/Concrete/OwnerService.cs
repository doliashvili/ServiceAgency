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

        public async Task<int> AddOwnerAsync(OwnerInputDto ownerInputDto)
        {
            var ownerEntity = new Owner
            {
                FirstName = ownerInputDto.FirstName,
                LastName = ownerInputDto.LastName,
                PrivateNumber = ownerInputDto.PrivateNumber
            };

            try
            {
                await _baseRepository.AddAsync(ownerEntity);
            }
            catch (Exception e)
            {
                if (e.InnerException.Message.ToLower().Contains("Cannot insert duplicate key row in object".ToLower()))
                    throw new PrivateNumberException("PrivateNumber is already exist !!!");
            }
            return ownerEntity.Id;
        }

        public async Task DeleteOwnerAsync(int id)
        {
            await _baseRepository.DeleteByIdAsync(id);
        }

        public async Task<Owner> GetOwnerByPrivateNumber(string privateNumber)
        {
            return await _baseRepository.FirstOrDefaultAsync(x => x.PrivateNumber.ToLower() == privateNumber.ToLower());
        }
    }
}
