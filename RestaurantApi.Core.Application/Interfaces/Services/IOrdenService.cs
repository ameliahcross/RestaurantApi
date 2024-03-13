using RestaurantApi.Core.Application.ViewModels.Orden;
using RestaurantApi.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApi.Core.Application.Interfaces.Services
{
    public interface IOrdenService : IGenericService<SaveOrdenViewModel, OrdenViewModel, Orden>
    {
        Task<List<OrdenViewModel>> GetAllOrdenesByIdWithIncludeAsync(int ordenId);
    }
}
