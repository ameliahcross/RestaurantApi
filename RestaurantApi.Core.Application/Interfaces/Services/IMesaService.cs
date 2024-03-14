using RestaurantApi.Core.Application.ViewModels.Mesa;
using RestaurantApi.Core.Domain.Entities;

namespace RestaurantApi.Core.Application.Interfaces.Services
{
    public interface IMesaService : IGenericService<SaveMesaViewModel, MesaViewModel, Mesa>
    {
        Task<MesaViewModel> ChangeMesaStatusAsync(int mesaId, EstadoMesa newStatus);
    }
}
