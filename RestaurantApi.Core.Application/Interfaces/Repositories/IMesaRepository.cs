using RestaurantApi.Core.Domain.Entities;

namespace RestaurantApi.Core.Application.Interfaces.Repositories
{
    public interface IMesaRepository : IGenericRepository<Mesa>
    {
        Task<Mesa> ChangeMesaStatus(int mesaId, EstadoMesa newStatus);
    }
}
