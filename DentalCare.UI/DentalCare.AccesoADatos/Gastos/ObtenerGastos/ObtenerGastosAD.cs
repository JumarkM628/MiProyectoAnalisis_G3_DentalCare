using DentalCare.Abstraccion.AccesoADatos.Gastos.ObtenerGastos;
using DentalCare.Abstraccion.Modelo.Gasto;
using System.Collections.Generic;
using System.Linq;

namespace DentalCare.AccesoADatos.Gastos.ObtenerGastos
{
    public class ObtenerGastosAD : IObtenerGastosAD
    {
        private readonly Contexto _contexto;

        public ObtenerGastosAD()
        {
            _contexto = new Contexto();
        }

        public List<GastoDto> Obtener()
        {
            var rawData = (
                from g in _contexto.Gastos
                where g.IdGasto != 1 // Excluir el placeholder
                join estado in _contexto.Estados
                    on g.IdEstado equals estado.IdEstado
                select new
                {
                    g.IdGasto,
                    g.Descripcion,
                    g.Monto,
                    g.Fecha,
                    NombreEstado = estado.NombreEstado
                }
            ).ToList();

            return rawData.Select(g => new GastoDto
            {
                IdGasto = g.IdGasto,
                Categoria = ExtraerCampo(g.Descripcion, "TIPO"),
                DescripcionMostrar = ExtraerCampo(g.Descripcion, "CONCEPTO"),
                MontoMostrar = g.Monto ?? 0,
                FechaMostrar = g.Fecha,
                NombreEstado = g.NombreEstado
            })
            .OrderByDescending(g => g.FechaMostrar)
            .ToList();
        }

        private static string ExtraerCampo(string descripcion, string campo)
        {
            if (string.IsNullOrEmpty(descripcion)) return string.Empty;
            foreach (var parte in descripcion.Split('|'))
                if (parte.StartsWith(campo + ":"))
                    return parte.Substring((campo + ":").Length).Trim();
            return descripcion; // Si no tiene formato, mostrar tal cual
        }
    }
}