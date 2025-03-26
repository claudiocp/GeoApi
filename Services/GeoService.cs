using GeoApi.Data;
using GeoApi.DTOs;
using GeoApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GeoApi.Services
{
    public class GeoService : IGeoService
    {
        private readonly GeoDbContext _context;

        public GeoService(GeoDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResult<StateDto>> AddStateAsync(StateDto stateDto)
        {
            // Verificar se já existe um estado com o mesmo código postal
            var existingState = await _context.States
                .FirstOrDefaultAsync(s => s.StatePostalCode == stateDto.StatePostalCode);

            if (existingState != null)
            {
                return ServiceResult<StateDto>.CreateError($"Já existe um estado cadastrado com o código '{stateDto.StatePostalCode}'.");
            }

            // Criar novo estado
            var newState = new State
            {
                StatePostalCode = stateDto.StatePostalCode,
                Name = stateDto.Name,
                Capital = stateDto.Capital
            };

            await _context.States.AddAsync(newState);
            await _context.SaveChangesAsync();

            // Verificar se há cidades com nomes duplicados
            var duplicateCities = stateDto.Cities
                .GroupBy(c => c.City)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();

            if (duplicateCities.Any())
            {
                return ServiceResult<StateDto>.CreateError(
                    $"Existem cidades com nomes duplicados: {string.Join(", ", duplicateCities)}");
            }

            // Adicionar cidades
            foreach (var cityDto in stateDto.Cities)
            {
                var newCity = new City
                {
                    Name = cityDto.City,
                    Longitude = cityDto.Longitude,
                    Latitude = cityDto.Latitude,
                    StateId = newState.Id,
                    StatePostalCode = stateDto.StatePostalCode
                };

                await _context.Cities.AddAsync(newCity);
            }

            await _context.SaveChangesAsync();
            return ServiceResult<StateDto>.CreateSuccess(stateDto, "Estado cadastrado com sucesso.");
        }

        public async Task<ServiceResult<StateDto>> GetStateByCodeAsync(string statePostalCode)
        {
            var state = await _context.States
                .Include(s => s.Cities)
                .FirstOrDefaultAsync(s => s.StatePostalCode == statePostalCode);

            if (state == null)
                return ServiceResult<StateDto>.CreateError($"Estado com código {statePostalCode} não encontrado.");

            return ServiceResult<StateDto>.CreateSuccess(MapStateToDto(state));
        }

        public async Task<ServiceResult<CityDto>> GetCityAsync(string statePostalCode, string cityName)
        {
            var state = await _context.States
                .FirstOrDefaultAsync(s => s.StatePostalCode == statePostalCode);

            if (state == null)
                return ServiceResult<CityDto>.CreateError($"Estado com código {statePostalCode} não encontrado.");

            var city = await _context.Cities
                .FirstOrDefaultAsync(c => c.StateId == state.Id && 
                                     c.Name == cityName);

            if (city == null)
                return ServiceResult<CityDto>.CreateError($"Cidade {cityName} no estado {statePostalCode} não encontrada.");

            return ServiceResult<CityDto>.CreateSuccess(new CityDto
            {
                City = city.Name,
                Longitude = city.Longitude,
                Latitude = city.Latitude
            });
        }

        private StateDto MapStateToDto(State state)
        {
            var stateDto = new StateDto
            {
                StatePostalCode = state.StatePostalCode,
                Name = state.Name,
                Capital = state.Capital,
                Cities = new System.Collections.Generic.List<CityDto>()
            };

            foreach (var city in state.Cities)
            {
                stateDto.Cities.Add(new CityDto
                {
                    City = city.Name,
                    Longitude = city.Longitude,
                    Latitude = city.Latitude
                });
            }

            return stateDto;
        }
    }
} 