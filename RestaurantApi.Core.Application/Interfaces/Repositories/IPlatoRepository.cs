using RestaurantApi.Core.Domain.Entities;

namespace RestaurantApi.Core.Application.Interfaces.Repositories
{
    public interface IPlatoRepository : IGenericRepository<Plato>
    {
        Task<List<Plato>> GetAllWithIncludeAsync();
        Task<Plato> AddPlatoWithIngredients(Plato plato, IEnumerable<int> ingredientesIds);
        Task<Plato> GetByIdWithIngredientsAsync(int id);
    }
}
