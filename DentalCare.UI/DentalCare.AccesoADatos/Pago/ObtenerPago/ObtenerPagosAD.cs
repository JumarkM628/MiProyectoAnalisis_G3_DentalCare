using DentalCare.Abstraccion.AccesoADatos.Pago.ObtenerPago;
using DentalCare.Abstraccion.Modelo.Pagos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.AccesoADatos.Pago.ObtenerPago
{
    public class ObtenerPagosAD : IObtenerPagosAD
    {
        private readonly Contexto _contexto;

        public ObtenerPagosAD()
        {
            _contexto = new Contexto();
        }

        /// <summary>
        /// Escenario 5: obtiene el historial financiero completo con
        /// paciente, cita, monto, método de pago y fecha.
        /// </summary>
        public List<PagoDto> Obtener()
        {
            var rawData = (
                from cont in _contexto.Contabilidades

                join cc in _contexto.ContabilidadCitas
                    on cont.IdContabilidad equals cc.IdContabilidad

                join cp in _contexto.ContabilidadPacientes
                    on cont.IdContabilidad equals cp.IdContabilidad into cpGrupo
                from cp in cpGrupo.DefaultIfEmpty()

                join paciente in _contexto.Usuarios
                    on cp.IdUsuario equals paciente.IdUsuario into pacienteGrupo
                from paciente in pacienteGrupo.DefaultIfEmpty()

                join metodo in _contexto.MetodosPago
                    on cont.IdMetodoPago equals metodo.IdMetodoPago

                join estado in _contexto.Estados
                    on cont.IdEstado equals estado.IdEstado

                select new
                {
                    cont.IdContabilidad,
                    cont.Monto,
                    cont.Fecha,
                    IdCita = cc.IdCita,
                    NombreMetodo = metodo.Descripcion,
                    NombreEstado = estado.NombreEstado,
                    NombrePaciente = paciente != null
                        ? paciente.Nombre + " " + paciente.PrimerApellido
                        : "Sin paciente"
                }
            ).ToList();

            return rawData.Select(r => new PagoDto
            {
                IdContabilidad = r.IdContabilidad,
                Monto = r.Monto ?? 0,
                FechaPago = r.Fecha,
                IdCita = r.IdCita,
                NombreMetodoPago = r.NombreMetodo,
                NombreEstado = r.NombreEstado,
                NombrePaciente = r.NombrePaciente
            })
            .OrderByDescending(p => p.FechaPago)
            .ToList();
        }
    }
}
