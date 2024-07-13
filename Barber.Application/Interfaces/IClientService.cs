using Barber.Application.DTOs;
using Barber.Application.DTOs.Register;
using Barber.Domain.Parameters;

namespace Barber.Application.Interfaces
{
    public interface IClientService
    {
        Task<bool> AddAsync(ClientRegisterDTO clientDTO);
        Task<bool> RemoveAsync(int? id);
        Task<ClientDTO> GetByIdAsync(int id);
        Task<IEnumerable<ClientDTO>> GetAllAsync(GetParametersPagination parameters);
        Task<bool> UpdateAsync(ClientDTO clientDTO, int? id);
        Task UpdatePointsAsync(int id);
    }
}
