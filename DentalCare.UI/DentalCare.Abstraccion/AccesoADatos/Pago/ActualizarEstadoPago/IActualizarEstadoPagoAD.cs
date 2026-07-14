using DentalCare.Abstraccion.Modelo.Pagos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.Abstraccion.AccesoADatos.Gastos.ActualizarEstadoPago
{
    public interface IActualizarEstadoPagoAD
    {
        void Actualizar(int idContabilidad, int idEstado);
        bool ExistePago(int idContabilidad);
        bool EstaPagado(int idContabilidad);
        PagoDto ObtenerPorId(int idContabilidad);
    }
}
