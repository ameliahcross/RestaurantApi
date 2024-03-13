using Microsoft.EntityFrameworkCore;
using RestaurantApi.Core.Application.Interfaces.Repositories;
using RestaurantApi.Core.Application.ViewModels.Plato;
using RestaurantApi.Core.Domain.Entities;
using RestaurantApi.Infrastructure.Persistence.Contexts;

namespace RestaurantApi.Infrastructure.Persistence.Repositories
{
    public class PlatoRepository : GenericRepository<Plato>, IPlatoRepository
    {
        private readonly ApplicationContext _dbContext;
        public PlatoRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Plato>> GetAllWithIncludeAsync() 
        {
            return await _dbContext.Set<Plato>()
                .Include(p => p.Ingredientes)
                .Include(p => p.Ordenes)
                .ToListAsync();
        }

        public async Task<Plato> AddPlatoWithIngredients(Plato plato, IEnumerable<int> ingredientesIds)
        {
            foreach (var ingredienteId in ingredientesIds)
            {
                var ingrediente = await _dbContext.Ingredientes.FindAsync(ingredienteId);

                if (ingrediente != null)
                {
                    plato.Ingredientes.Add(ingrediente);
                }
                else
                {
                    throw new InvalidOperationException($"No se puede realizar la operación porque el ingrediente con el ID {ingredienteId} no fue encontrado.");
                }
            }

            await _dbContext.Platos.AddAsync(plato);
            await _dbContext.SaveChangesAsync();

            return plato;
        }

        public async Task<Plato> GetByIdWithIngredientsAsync(int id)
        {
            return await _dbContext.Set<Plato>()
                .Include(p => p.Ingredientes)
                .FirstOrDefaultAsync(p => p.Id == id);
        }


    }
}
