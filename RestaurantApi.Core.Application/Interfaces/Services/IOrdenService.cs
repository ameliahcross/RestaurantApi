using RestaurantApi.Core.Application.ViewModels.Orden;
using RestaurantApi.Core.Domain.Entities;

namespace RestaurantApi.Core.Application.Interfaces.Services
{
    public interface IOrdenService : IGenericService<SaveOrdenViewModel, OrdenViewModel, Orden>
    {
        Task<OrdenViewModel> GetOrdenByIdWithIncludeAsync(int ordenId);
        Task<OrdenViewModel> GetOrdenByIdTableId(int tableId);
        Task<SaveOrdenViewModel> AddOrdenWithPlatos(SaveOrdenViewModel saveOrdenVm);
        Task<OrdenViewModel> UpdateOrden(UpdateOrdenViewModel viewModel, int id);
        Task<bool> DeleteOrdenWithPlatos(int orderId);
        Task<List<OrdenViewModel>> GetAllWithIncludeAsync();
    }

}
