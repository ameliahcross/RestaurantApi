using Microsoft.EntityFrameworkCore;
using RestaurantApi.Core.Application.Interfaces.Repositories;
using RestaurantApi.Core.Domain.Entities;
using RestaurantApi.Infrastructure.Persistence.Contexts;

namespace RestaurantApi.Infrastructure.Persistence.Repositories
{
    public class MesaRepository : GenericRepository<Mesa>, IMesaRepository
    {
        private readonly ApplicationContext _dbContext;
        public MesaRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Mesa> ChangeMesaStatus(int mesaId, EstadoMesa newStatus)
        {
            var mesa = await _dbContext.Mesas.FindAsync(mesaId);
            if (mesa == null)
            {
                throw new KeyNotFoundException($"No se encontró la mesa con ID {mesaId}.");

            }
            mesa.Estado = newStatus;
            await _dbContext.SaveChangesAsync();
            return mesa;
        }
    }
}
