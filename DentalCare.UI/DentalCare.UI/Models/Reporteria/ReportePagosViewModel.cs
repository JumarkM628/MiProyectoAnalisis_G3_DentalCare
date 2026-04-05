using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DentalCare.UI.Models.Reporteria
{
    public class ReportePagosViewModel
    {
        public DateTime Fecha { get; set; }
        public string Paciente { get; set; }
        public decimal Monto { get; set; }
    }
}