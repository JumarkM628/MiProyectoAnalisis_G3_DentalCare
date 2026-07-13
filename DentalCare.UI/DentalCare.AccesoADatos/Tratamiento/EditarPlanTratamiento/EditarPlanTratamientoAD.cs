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
                Id = entidad.IdTratamiento,
                Diagnostico = entidad.Descripcion,
                Tratamiento = entidad.Descripcion,
                Estado = entidad.IdEstado == 1 ? "Activo" : "Inactivo",
                FechaInicio = entidad.FechaInicio ?? DateTime.MinValue
            };
        }

        public bool GuardarCambios(PlanTratamientoDto dto)
        {
            var entidad = _contexto.PlanesTratamiento.Find(dto.Id);
            if (entidad == null) return false;

            entidad.Descripcion = dto.Diagnostico;

            _contexto.SaveChanges();
            return true;
        }
    }
}
