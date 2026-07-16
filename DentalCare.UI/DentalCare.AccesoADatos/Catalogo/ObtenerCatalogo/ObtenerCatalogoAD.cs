using DentalCare.Abstraccion.AccesoADatos.Catalogo.ObtenerCatalogoAD;
using DentalCare.Abstraccion.Modelo.Catalogo;
using System.Collections.Generic;
using System.Linq;

namespace DentalCare.AccesoADatos.Catalogo.ObtenerCatalogo
{
    public class ObtenerCatalogoAD : IObtenerCatalogoAD
    {
        private readonly Contexto _contexto;

        public ObtenerCatalogoAD()
        {
            _contexto = new Contexto();
        }

        public List<CatalogoDto> Obtener()
        {
            var rawData = (
                from c in _contexto.CatalogoTratamientos
                join estado in _contexto.Estados
                    on c.IdEstado equals estado.IdEstado
                orderby c.Nombre
                select new
                {
                    c.IdCatalogo,
                    c.Nombre,
                    c.Categoria,
                    c.DuracionMin,
                    c.Costo,
                    c.CostoAnterior,
                    c.FechaActualizacion,
                    c.IdEstado,
                    NombreEstado = estado.NombreEstado
                }
            ).ToList();

            return rawData.Select(c => new CatalogoDto
            {
                IdCatalogo = c.IdCatalogo,
                Nombre = c.Nombre,
                Categoria = c.Categoria,
                DuracionMin = c.DuracionMin,
                Costo = c.Costo,
                CostoAnterior = c.CostoAnterior,
                FechaActualizacion = c.FechaActualizacion,
                NombreEstado = c.NombreEstado
            }).ToList();
        }

        public CatalogoDto ObtenerPorId(int idCatalogo)
        {
            var rawData = (
                from c in _contexto.CatalogoTratamientos
                where c.IdCatalogo == idCatalogo
                join estado in _contexto.Estados
                    on c.IdEstado equals estado.IdEstado
                select new
                {
                    c.IdCatalogo,
                    c.Nombre,
                    c.Categoria,
                    c.DuracionMin,
                    c.Costo,
                    c.CostoAnterior,
                    c.FechaActualizacion,
                    NombreEstado = estado.NombreEstado
                }
            ).FirstOrDefault();

            if (rawData == null) return null;

            return new CatalogoDto
            {
                IdCatalogo = rawData.IdCatalogo,
                Nombre = rawData.Nombre,
                Categoria = rawData.Categoria,
                DuracionMin = rawData.DuracionMin,
                Costo = rawData.Costo,
                CostoAnterior = rawData.CostoAnterior,
                FechaActualizacion = rawData.FechaActualizacion,
                NombreEstado = rawData.NombreEstado
            };
        }
    }
}