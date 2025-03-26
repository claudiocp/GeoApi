using GeoApi.DTOs;
using System.Threading.Tasks;

namespace GeoApi.Services
{
    public interface IGeoService
    {
        Task<ServiceResult<StateDto>> AddStateAsync(StateDto stateDto);
        Task<ServiceResult<StateDto>> GetStateByCodeAsync(string statePostalCode);
        Task<ServiceResult<CityDto>> GetCityAsync(string statePostalCode, string cityName);
    }
} 