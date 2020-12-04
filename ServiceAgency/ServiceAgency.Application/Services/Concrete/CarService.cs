using Domain.Extensions.Expression;
using Microsoft.EntityFrameworkCore;
using ServiceAgency.Application.Commands;
using ServiceAgency.Application.Dtos;
using ServiceAgency.Application.Queries;
using ServiceAgency.Application.Services.Abstract;
using ServiceAgency.Domain.Entities;
using ServiceAgency.Domain.Exceptions;
using ServiceAgency.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAgency.Application.Services.Concrete
{
    public class CarService : ICarService
    {
        private readonly IBaseRepository<Car> _carRepository;
        private readonly IBaseRepository<Color> _colorRepository;
        private readonly IBaseRepository<Fuel> _fuelRepository;
        private readonly IBaseRepository<Owner> _ownerRepository;

        public CarService(IBaseRepository<Car> carRepository,
                          IBaseRepository<Color> colorRepository,
                          IBaseRepository<Fuel> fuelRepository,
                          IBaseRepository<Owner> ownerRepository)
        {
            _carRepository = carRepository;
            _colorRepository = colorRepository;
            _fuelRepository = fuelRepository;
            _ownerRepository = ownerRepository;
        }

        public async Task AddCarAsync(CarInputDto carInputDto)
        {
            Color color = await AddColor(carInputDto.Color);

            var fuel = await _fuelRepository.FirstOrDefaultAsync(x => x.Name.ToLower() == carInputDto.Fuel.ToLower());
            if (fuel == null)
            {
                await _fuelRepository.AddAsync(new Fuel { Name = carInputDto.Fuel });
                fuel = await _fuelRepository.FirstOrDefaultAsync(x => x.Name.ToLower() == carInputDto.Fuel.ToLower());
            }

            var owners = new List<Owner>();

            foreach (var ownerId in carInputDto.OwnersId)
            {
                var owner = await _ownerRepository.FirstOrDefaultAsync(x => x.Id == ownerId);
                if (owner != null)
                {
                    owners.Add(owner);
                }
            }


            var carEntity = new Car
            {
                Color = color,
                Fuel = fuel,
                CreatedDate = carInputDto.CreatedDate,
                VinCode = carInputDto.VinCode,
                MarkEng = carInputDto.MarkEng,
                MarkGeo = carInputDto.MarkGeo,
                ModelEng = carInputDto.ModelEng,
                ModelGeo = carInputDto.ModelGeo,
                Owners = owners,
                Image = carInputDto.Image,
                TransportNumber = carInputDto.TransportNumber,
            };

            await _carRepository.AddAsync(carEntity);
        }

        public async Task<CarOutputDto> GetCarByIdAsync(int id)
        {
            var car = await _carRepository.Queryable()
                  .Include(x => x.Color)
                  .Include(x => x.Fuel)
                  .Include(x => x.Owners).FirstOrDefaultAsync(x => x.Id == id);

            return MapperCarToCarOutputDto(car);
        }

        public async Task<(IEnumerable<CarOutputDto>, int)> GetCarsAsync(GetCars query)
        {
            var page = query.Page ?? 1 - 1;
            var pageSize = query.PageSize ?? 10;

            var cars = await _carRepository.Queryable()
                  .Include(x => x.Color)
                  .Include(x => x.Fuel)
                  .Include(x => x.Owners)
                  .Skip(page * pageSize)
                  .Take(pageSize)
                  .AsNoTracking()
                  .ToListAsync();

            int carsCount = await _carRepository.CountAsync();

            List<CarOutputDto> carInputDtos = new List<CarOutputDto>();

            foreach (var car in cars)
            {
                carInputDtos.Add(MapperCarToCarOutputDto(car));
            }

            (IEnumerable<CarOutputDto>, int) result = (carInputDtos, carsCount);

            return result;
        }

        public async Task<(IEnumerable<CarOutputDto>, int)> GetSearchedCarsAsync(SearchCars query)
        {
            var page = query.Page ?? 1 - 1;
            var pageSize = query.PageSize ?? 10;
            var filter = BuildExpression(query);

            var cars = await _carRepository.Queryable()
                                              .Include(x => x.Color)
                                              .Include(x => x.Fuel)
                                              .Include(x => x.Owners)
                                              .Where(filter)
                                              .Skip(page * pageSize)
                                              .Take(pageSize)
                                              .AsNoTracking()
                                              .ToListAsync();

            int carsCount = await _carRepository.CountAsync(filter);

            List<CarOutputDto> carInputDtos = new List<CarOutputDto>();

            foreach (var car in cars)
            {
                carInputDtos.Add(MapperCarToCarOutputDto(car));
            }

            (IEnumerable<CarOutputDto>, int) result = (carInputDtos, carsCount);

            return result;

        }

        public async Task RemoveCarAsync(int id)
        {
            await _carRepository.DeleteByIdAsync(id);
        }

        public async Task UpdateCarColorAsync(ChangeCarColor changeCarColor)
        {
            var car = await _carRepository.Queryable()
                 .Include(x => x.Color)
                 .FirstOrDefaultAsync(c => c.Id == changeCarColor.CarId);

            if (car == null)
            {
                throw new NotFoundException($"{changeCarColor.CarId}");
            }

            var color = await AddColor(changeCarColor.Color);

            car.Color = color;
            await _carRepository.UpdateAsync(car);
        }

        public async Task UpdateCarOwnerAsync(ChangeCarOwner changeCarOwner)
        {
            var owner = await _ownerRepository.FirstOrDefaultAsync(x => x.Id == changeCarOwner.OwnerId);
            owner.IsActive = true;
            if (owner == null)
            {
                throw new NotFoundException($"{changeCarOwner.OwnerId}");
            }

            var car = await _carRepository.Queryable()
                .Include(x => x.Owners)
                .FirstOrDefaultAsync(x => x.Id == changeCarOwner.CarId);
            if (car == null)
            {
                throw new NotFoundException($"{changeCarOwner.CarId}");
            }

            car.Owners.ForEach(x =>
            {
                x.IsActive = false;
            });

            car.Owners.Add(owner);

            await _carRepository.UpdateAsync(car);
        }

        public async Task UpdateCarTransportNumberAsync(ChangeCarTransportNumber changeCarNumber)
        {
            var car = await _carRepository.GetByIdAsync(changeCarNumber.CarId);
            car.TransportNumber = changeCarNumber.TransportNumber;

            await _carRepository.UpdateAsync(car);
        }

        public async Task<(CarOwnersOutputDto, int)> GetCarOwners(GetCarOwners query)
        {
            var page = query.Page ?? 1 - 1;
            var pageSize = query.PageSize ?? 10;

            var car = await _carRepository.Queryable()
                .Include(x => x.Owners)
                .FirstOrDefaultAsync(x => x.Id == query.CarId);

            var owners = car.Owners
                 .Skip(page * pageSize)
                 .Take(pageSize);

            CarOwnersOutputDto carOwnersOutputDto = new CarOwnersOutputDto { Owners = owners.ToList() };

            (CarOwnersOutputDto carOwnersPage, int allOwnersCount) result
                = (carOwnersOutputDto, car.Owners.Count);

            return result;
        }


        #region private methods
        private Expression<Func<Car, bool>> BuildExpression(SearchCars query)
        {
            Expression<Func<Car, bool>> expression = x => x.Id != default;

            if (query.Color != null)
            {
                expression = expression.And(x => x.Color.ColorName.ToLower().Contains(query.Color.ToLower()));
            }
            if (query.Fuel != null)
            {
                expression = expression.And(x => x.Fuel.Name.ToLower().Contains(query.Fuel.ToLower()));
            }
            if (query.MarkEng != null)
            {
                expression = expression.And(x => x.MarkEng.ToLower().Contains(query.MarkEng.ToLower()));
            }
            if (query.MarkGeo != null)
            {
                expression = expression.And(x => x.MarkGeo.ToLower().Contains(query.MarkGeo.ToLower()));
            }
            if (query.ModelGeo != null)
            {
                expression = expression.And(x => x.ModelGeo.ToLower().Contains(query.ModelGeo.ToLower()));
            }
            if (query.ModelEng != null)
            {
                expression = expression.And(x => x.ModelEng.ToLower().Contains(query.ModelEng.ToLower()));
            }
            if (query.OwnerId != null)
            {
                expression = expression.And(x => x.Owners.FirstOrDefault(o => o.Id == query.OwnerId).IsActive);
            }
            if (query.TransportNumber != null)
            {
                expression = expression.And(x => x.TransportNumber.ToLower().Contains(query.TransportNumber.ToLower()));
            }
            if (query.VinCode != null)
            {
                expression = expression.And(x => x.VinCode.ToLower().Contains(query.VinCode.ToLower()));
            }

            return expression;
        }
        private CarOutputDto MapperCarToCarOutputDto(Car car)
        {
            var owner = car.Owners.FirstOrDefault(x => x.IsActive = true);

            var carOutputDto = new CarOutputDto
            {
                Image = car.Image,
                Owner = owner,
                ActiveOwnerId = owner.Id,
                Color = car.Color,
                CreatedDate = car.CreatedDate,
                Fuel = car.Fuel,
                MarkEng = car.MarkEng,
                MarkGeo = car.MarkGeo,
                ModelEng = car.ModelEng,
                ModelGeo = car.ModelGeo,
                TransportNumber = car.TransportNumber,
                VinCode = car.VinCode
            };

            return carOutputDto;
        }
        private async Task<Color> AddColor(string color)
        {
            var tryColor = await _colorRepository.FirstOrDefaultAsync(x => x.ColorName.ToLower() == color.ToLower());
            if (tryColor == null)
            {
                await _colorRepository.AddAsync(new Color { ColorName = color });
                tryColor = await _colorRepository.FirstOrDefaultAsync(x => x.ColorName.ToLower() == color.ToLower());
            }

            return tryColor;
        }

        #endregion
    }

}
