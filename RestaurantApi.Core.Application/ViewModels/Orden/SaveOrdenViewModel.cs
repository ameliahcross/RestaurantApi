using RestaurantApi.Core.Application.ViewModels.Plato;
using RestaurantApi.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApi.Core.Application.ViewModels.Orden
{
    public class SaveOrdenViewModel
    {
        public int Id { get; set; }
        public StatusOrden Estado { get; set; }
        public decimal Subtotal { get; set; }
        public ICollection<PlatoViewModel> Platos { get; set; } = new List<PlatoViewModel>();
        public string Descripcion { get; set; }

        //Foreing key de la tabla Mesa
        public int MesaId { get; set; }
    }
}
