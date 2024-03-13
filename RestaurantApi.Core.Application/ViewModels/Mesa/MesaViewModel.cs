using RestaurantApi.Core.Application.ViewModels;
using RestaurantApi.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApi.Core.Application.ViewModels.Mesa
{
    public class MesaViewModel
    {
        public int Id { get; set; }
        public int Capacidad { get; set; }
        public string Descripcion { get; set; }
        public EstadoMesa Estado { get; set; }
    }
}
