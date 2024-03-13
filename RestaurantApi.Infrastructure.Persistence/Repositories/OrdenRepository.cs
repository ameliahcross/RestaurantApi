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
    public class OrdenRepository : GenericRepository<Orden>, IOrdenRepository
    {
        private readonly ApplicationContext _dbContext;
        public OrdenRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Orden>> GetAllWithIncludeAsync(int ordenId)
        {
            return await _dbContext.Set<Orden>()
                .Where(o => o.Id == ordenId)
                .Include(o => o.Platos)
                .Include(o => o.Mesa)
                .ToListAsync();
        }
    }
}
