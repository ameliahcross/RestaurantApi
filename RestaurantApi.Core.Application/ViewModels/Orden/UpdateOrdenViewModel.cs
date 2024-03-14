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
    public class UpdateOrdenViewModel
    {
        public int Id { get; set; }
        public StatusOrden Estado { get; set; }
        public decimal Subtotal { get; set; }
        public ICollection<int> PlatosIds { get; set; } = new List<int>();
        public string Descripcion { get; set; }
        public int MesaId { get; set; }
    }
}
