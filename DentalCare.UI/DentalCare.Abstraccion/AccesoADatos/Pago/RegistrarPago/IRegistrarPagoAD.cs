using DentalCare.Abstraccion.Modelo.Pagos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.Abstraccion.AccesoADatos.Pago.RegistrarPago
{
    public interface IRegistrarPagoAD
    {
        void Registrar(PagoDto dto);
        bool ExisteCitaFinalizada(int idCita);
        bool YaTienePago(int idCita);
    }
}
