using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DentalCare.Abstraccion.Modelo.Expediente;

namespace DentalCare.AccesoADatos.Reporteria.Expediente
{
    public class ReporteExpedienteAD
    {
        private readonly Contexto _contexto;

        public ReporteExpedienteAD(Contexto contexto)
        {
            _contexto = contexto;
        }
        public List<ProcedimientoDto> ObtenerProcedimientosPorExpediente(int idExpediente, DateTime? desde = null, DateTime? hasta = null, int? idTratamiento = null)
        {
            var query = from proc in _contexto.Procedimientos
                        join uc in _contexto.UsuarioCitas on proc.ID_CITA equals uc.IdCita into ucGrp
                        from uc in ucGrp.DefaultIfEmpty()
                        join ue in _contexto.UsuarioExpedientes on uc.IdUsuario equals ue.IdUsuario into ueGrp
                        from ue in ueGrp.DefaultIfEmpty()
                        join cita in _contexto.Citas on proc.ID_CITA equals cita.IdCita into citaGrp
                        from cita in citaGrp.DefaultIfEmpty()
                        join doctor in _contexto.Usuarios on cita.IdDoctor equals doctor.IdUsuario into docGrp
                        from doctor in docGrp.DefaultIfEmpty()
                        join trat in _contexto.PlanesTratamiento on proc.ID_TRATAMIENTO equals trat.IdTratamiento into tratGrp
                        from trat in tratGrp.DefaultIfEmpty()
                        where ue != null && ue.IdExpediente == idExpediente
                        select new ProcedimientoDto
                        {
                            IdProcedimiento = proc.ID_PROCEDIMIENTO,
                            IdCita = proc.ID_CITA,
                            IdTratamiento = proc.ID_TRATAMIENTO,
                            Descripcion = proc.DESCRIPCION,
                            Fecha = proc.FECHA,
                            Observaciones = proc.OBSERVACIONES,
                            NombreDoctor = doctor != null ? doctor.Nombre + " " + doctor.PrimerApellido : "-",
                            NombreTratamiento = trat != null ? trat.Descripcion : "-"
                        };
            if (desde.HasValue)
                query = query.Where(p => p.Fecha.HasValue && p.Fecha.Value >= desde.Value.Date);

            if (hasta.HasValue)
                query = query.Where(p => p.Fecha.HasValue && p.Fecha.Value <= hasta.Value.Date);

            if (idTratamiento.HasValue)
                query = query.Where(p => p.IdTratamiento == idTratamiento.Value);

            return query.OrderByDescending(p => p.Fecha).ToList();
        }
    }
}
