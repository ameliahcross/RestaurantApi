﻿using RestaurantApi.Core.Application.ViewModels.Plato;
using RestaurantApi.Core.Domain.Entities;

namespace RestaurantApi.Core.Application.ViewModels.Orden
{
    public class SaveOrdenViewModel
    {
        public int Id { get; set; }
        public decimal Subtotal { get; set; }
        public ICollection<int> PlatosIds { get; set; } = new List<int>();
        public string Descripcion { get; set; }
        public int MesaId { get; set; }
    }
}
