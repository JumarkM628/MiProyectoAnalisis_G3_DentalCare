using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DentalCare.Abstraccion.AccesoADatos.Expediente.CerrarExpediente;
using DentalCare.Abstraccion.LogicaDeNegocio.Expedientes.CerrarExpediente;
using DentalCare.Abstraccion.LogicaDeNegocio.Expedientes.CrearExpediente;
using DentalCare.Abstraccion.Modelo.Bitacora;
using DentalCare.Abstraccion.Modelo.Expedientes;

namespace DentaCare.LogicaDeNegocio.Expedientes.CerrarExpediente
{
    public class CerrarExpedienteLN : ICerrarExpedienteLN
    {
        private readonly ICerrarExpedienteAD _cerrarExpedienteAD;

        public CerrarExpedienteLN(ICerrarExpedienteAD cerrarExpedienteAD)
        {
            _cerrarExpedienteAD = cerrarExpedienteAD;
        }

        public ExpedienteDto ObtenerExpedientePorId(int id)
        {
            return _cerrarExpedienteAD.ObtenerExpedientePorId(id);
        }

        public string CerrarExpediente(int id, string nombreDoctora)
        {
            var expediente = _cerrarExpedienteAD.ObtenerExpedientePorId(id);

            if (expediente == null)
                return "El expediente no fue encontrado.";

            if (expediente.IdEstado == 2)
                return "El expediente ya se encuentra cerrado y no puede modificarse.";

            bool cerrado = _cerrarExpedienteAD.CerrarExpediente(id);
            if (!cerrado)
                return "Ocurrió un error al cerrar el expediente.";

            var bitacora = new BitacoraDto
            {
                Modulo = "Expediente",
                Accion = "Cierre",
                Descripcion = $"Se cerró el expediente ID {id}.",
                NombreUsuario = nombreDoctora,
                FechaHora = DateTime.Now
            };

            _cerrarExpedienteAD.RegistrarCierreEnBitacora(bitacora);

            return null;
        }
    }
}