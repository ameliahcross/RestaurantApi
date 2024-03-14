using RestaurantApi.Core.Domain.Entities;

namespace RestaurantApi.Core.Application.Interfaces.Repositories
{
    public interface IOrdenRepository : IGenericRepository<Orden>
    {
        Task<List<Orden>> GetAllWithIncludeAsync();
        Task<Orden> GetOrdenByTableId(int tableId);
        Task<Orden> UpdateOrdenWithPlatos(int id, Orden updatedOrden, ICollection<int> platosIds);
        Task<Orden> AddOrdenWithPlatos(Orden orden, IEnumerable<int> platosIds);
        Task<Orden> GetOrdenById(int id);
        Task DeleteOrdenWithPlatos(int orderId);
    }
}
