using Microsoft.EntityFrameworkCore;
using RestaurantApi.Core.Application.Interfaces.Repositories;
using RestaurantApi.Core.Domain.Entities;
using RestaurantApi.Infrastructure.Persistence.Contexts;

namespace RestaurantApi.Infrastructure.Persistence.Repositories
{
    public class OrdenRepository : GenericRepository<Orden>, IOrdenRepository
    {
        private readonly ApplicationContext _dbContext;
        public OrdenRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Orden>> GetAllWithIncludeAsync()
        {
            return await _dbContext.Set<Orden>()
                .Include(o => o.Platos)
                    .ThenInclude(p => p.Ingredientes)
                .Include(o => o.Mesa)
                .ToListAsync();
        }

        public async Task<Orden> GetOrdenByTableId(int tableId)
        {
            return await _dbContext.Set<Orden>()
                .Where(o => o.Id == tableId)
                .Include(o => o.Platos) 
                    .ThenInclude(p => p.Ingredientes)
                .FirstOrDefaultAsync();
        }

        public async Task<Orden> GetOrdenById(int id)
        {
            return await _dbContext.Set<Orden>()
                .Where(o => o.Id == id)
                .Include(o => o.Platos)
                     .ThenInclude(p => p.Ingredientes)
                .FirstOrDefaultAsync();
        }

        public async Task<Orden> AddOrdenWithPlatos(Orden orden, IEnumerable<int> platosIds)
        {
            var platos = await _dbContext.Platos
                .Where(plato => platosIds.Contains(plato.Id))
                .ToListAsync();

            foreach (var plato in platos)
            {
                orden.Platos.Add(plato);
            }

            await _dbContext.Ordenes.AddAsync(orden);
            await _dbContext.SaveChangesAsync();
            return orden;
        }

        public async Task<Orden> UpdateOrdenWithPlatos(int id, Orden updatedOrden, ICollection<int> platosIds)
        {
            var orden = await _dbContext.Ordenes
                .Include(o => o.Platos)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (orden == null)
            {
                throw new KeyNotFoundException($"Orden con ID {id} no encontrada.");
            }

            _dbContext.Entry(orden).CurrentValues.SetValues(updatedOrden);

            orden.Platos.Clear();
            foreach (var platoId in platosIds)
            {
                var plato = await _dbContext.Platos.FindAsync(platoId);
                if (plato != null)
                {
                    orden.Platos.Add(plato);
                }
            }

            await _dbContext.SaveChangesAsync();
            return orden;
        }

        public async Task DeleteOrdenWithPlatos(int orderId)
        {
            var orden = await _dbContext.Ordenes
               .Include(o => o.Platos)
               .FirstOrDefaultAsync(o => o.Id == orderId);

            if (orden != null)
            {
                foreach (var plato in orden.Platos.ToList())
                {
                    orden.Platos.Remove(plato); 
                }

                _dbContext.Ordenes.Remove(orden);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("No se encontró la orden con el ID especificado.");
            }
        }

    }
}
