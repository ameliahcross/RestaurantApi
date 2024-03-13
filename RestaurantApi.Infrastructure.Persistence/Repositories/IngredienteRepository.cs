using Microsoft.EntityFrameworkCore;
using RestaurantApi.Core.Application.Interfaces.Repositories;
using RestaurantApi.Core.Domain.Entities;
using RestaurantApi.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApi.Infrastructure.Persistence.Repositories
{
    public class IngredienteRepository : GenericRepository<Ingrediente>, IIngredienteRepository
    {
        private readonly ApplicationContext _dbContext;

        public IngredienteRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Ingrediente>> GetAllWithIncludeAsync()
        {
            return await _dbContext.Set<Ingrediente>()
                .ToListAsync();
        }
    }
}
