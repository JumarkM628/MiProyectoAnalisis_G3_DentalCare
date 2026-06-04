using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using DentalCare.Abstraccion.Modelo.Tratamiento;

namespace DentalCare.AccesoADatos.Tratamiento.EditarPlanTratamiento
{
    public class EditarPlanTratamientoAD
    {
        private readonly Contexto _contexto;

        public EditarPlanTratamientoAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public PlanTratamientoDto ObtenerPlanPorId(int id)
        {
            var entidad = _contexto.PlanesTratamiento.Find(id);
            if (entidad == null) return null;

            return new PlanTratamientoDto
            {
                Id = entidad.ID_TRATAMIENTO,
                Diagnostico = entidad.DESCRIPCION,
                Tratamiento = entidad.DESCRIPCION,
                Estado = entidad.ID_ESTADO == 1 ? "Activo" : "Inactivo",
                FechaInicio = entidad.FECHA_INICIO ?? DateTime.MinValue
            };
        }

        public bool GuardarCambios(PlanTratamientoDto dto)
        {
            var entidad = _contexto.PlanesTratamiento.Find(dto.Id);
            if (entidad == null) return false;

            entidad.DESCRIPCION = dto.Diagnostico;

            _contexto.SaveChanges();
            return true;
        }
    }
}
