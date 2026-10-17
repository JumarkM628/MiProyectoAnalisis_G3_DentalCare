using System;
using System.Collections.Generic;
using System.Linq;

namespace DentalCare.UI.Models.Contabilidad
{
    public class CostoTratamientoItem
    {
        public int IdPlan { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string Estado { get; set; }
        public int? IdCita { get; set; }
        public decimal? Monto { get; set; }
    }

    public class CostosTratamientoViewModel
    {
        public List<CostoTratamientoItem> Items { get; set; } = new List<CostoTratamientoItem>();
        public decimal Total => Items.Sum(i => i.Monto ?? 0m);
        public decimal Promedio => Items.Count == 0 ? 0m : Total / Items.Count;
        public int? IdCita { get; set; }
        public DateTime? Desde { get; set; }
        public DateTime? Hasta { get; set; }
        public string Error { get; set; }
    }
}