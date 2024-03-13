using RestaurantApi.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApi.Core.Application.Interfaces.Repositories
{
    public interface IPlatoRepository : IGenericRepository<Plato>
    {
        Task<List<Plato>> GetAllWithIncludeAsync();
        Task<Plato> AddPlatoWithIngredients(Plato plato, IEnumerable<int> ingredientesIds);
        Task<Plato> GetByIdWithIngredientsAsync(int id);
    }
}
