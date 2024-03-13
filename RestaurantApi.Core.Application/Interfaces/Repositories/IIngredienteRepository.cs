using RestaurantApi.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApi.Core.Application.Interfaces.Repositories
{
    public interface IIngredienteRepository : IGenericRepository<Ingrediente>
    {
        Task<List<Ingrediente>> GetAllWithIncludeAsync();
    }
}
