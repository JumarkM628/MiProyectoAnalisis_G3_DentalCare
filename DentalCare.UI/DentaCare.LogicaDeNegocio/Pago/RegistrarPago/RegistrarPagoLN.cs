using DentalCare.Abstraccion.AccesoADatos.Pago.RegistrarPago;
using DentalCare.Abstraccion.LogicaDeNegocio.Pago.RegistrarPago;
using DentalCare.Abstraccion.Modelo.Pagos;
using DentalCare.AccesoADatos.Pago.RegistroPago;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentaCare.LogicaDeNegocio.Pago.RegistrarPago
{
    public class RegistrarPagoLN : IRegistrarPagoLN
    {
        private readonly IRegistrarPagoAD _registrarAD;

        public RegistrarPagoLN()
        {
            _registrarAD = new RegistrarPagoAD();
        }

        public string Registrar(PagoDto dto)
        {
            // Verificar que la cita esté finalizada
            if (!_registrarAD.ExisteCitaFinalizada(dto.IdCitaForm))
                return "Solo se pueden registrar pagos de citas finalizadas.";

            // Evitar pago duplicado
            if (_registrarAD.YaTienePago(dto.IdCitaForm))
                return "Esta cita ya tiene un pago registrado.";

            _registrarAD.Registrar(dto);
            return null;
        }
    }
}