using System;
using System.Collections.Generic;
using System.Linq;
using DentalCare.Abstraccion.AccesoADatos.Expediente.ObtenerTodosLosExpedientes;
using DentalCare.Abstraccion.Modelo.Expedientes;
using DentalCare.AccesoADatos.Entidades.Usuarios;

namespace DentalCare.AccesoADatos.Expedientes.ObtenerTodosLosExpedientes
{
    public class ObtenerTodosLosExpedientesAD : IObtenerTodosLosExpedientesAD
    {
        private readonly Contexto _contexto;
        private const string ROL_PACIENTE = "Paciente";   

        public ObtenerTodosLosExpedientesAD()
        {
            _contexto = new Contexto();
        }

        public List<ExpedienteDto> Obtener()
        {
            var roleId = _contexto.AspNetRoles
                .Where(r => r.Name == ROL_PACIENTE)
                .Select(r => r.Id)
                .FirstOrDefault();

            if (string.IsNullOrEmpty(roleId))
            {
                return new List<ExpedienteDto>();
            }

            var idsPacientesAspNet = _contexto.AspNetUserRoles
                .Where(ur => ur.RoleId == roleId)
                .Select(ur => ur.UserId)
                .ToList();

            var pacientesClinica = _contexto.Usuarios
                .Where(u => idsPacientesAspNet.Contains(u.ASPNET_USER_ID))
                .Select(u => u.IdUsuario)
                .ToList();

            var rawData = (
                from ue in _contexto.UsuarioExpedientes
                where pacientesClinica.Contains(ue.IdUsuario)

                join expediente in _contexto.Expedientes
                    on ue.IdExpediente equals expediente.IdExpediente

                join usuario in _contexto.Usuarios
                    on ue.IdUsuario equals usuario.IdUsuario

                join cedula in _contexto.Cedulas
                    on usuario.IdUsuario equals cedula.IdUsuario into cedulaGrupo
                from cedula in cedulaGrupo.DefaultIfEmpty()

                join consentimiento in _contexto.Consentimientos
                    on expediente.IdConsentimiento equals consentimiento.IdConsentimiento

                join estado in _contexto.Estados
                    on expediente.IdEstado equals estado.IdEstado

                select new
                {
                    expediente.IdExpediente,
                    IdPaciente = usuario.IdUsuario,
                    usuario.Nombre,
                    usuario.PrimerApellido,
                    usuario.SegundoApellido,
                    NumeroCedula = cedula != null ? cedula.NumeroCedula : "",
                    DescripcionRaw = consentimiento.Descripcion,
                    NombreEstado = estado.NombreEstado,
                    expediente.IdEstado,
                    expediente.FechaDeCreacion
                }
            ).ToList();

            var lista = rawData.Select(r => new ExpedienteDto
            {
                IdExpediente = r.IdExpediente,
                IdPaciente = r.IdPaciente,
                NombrePaciente = (r.Nombre + " " + r.PrimerApellido + " " + r.SegundoApellido).Trim(),
                CedulaPaciente = r.NumeroCedula,
                Identificacion = r.NumeroCedula,
                NombreEstado = r.NombreEstado,
                IdEstado = r.IdEstado,
                FechaCreacion = r.FechaDeCreacion,
                Objetivo = ExtraerCampo(r.DescripcionRaw, "OBJETIVO"),
                Descripcion = ExtraerCampo(r.DescripcionRaw, "DESCRIPCION"),
                Alternativas = ExtraerCampo(r.DescripcionRaw, "ALTERNATIVAS"),
                Consecuencias = ExtraerCampo(r.DescripcionRaw, "CONSECUENCIAS"),
                Otro = ExtraerCampo(r.DescripcionRaw, "OTRO")
            }).ToList();

            return lista;
        }

        public static string ExtraerCampo(string descripcion, string campo)
        {
            if (string.IsNullOrEmpty(descripcion)) return string.Empty;

            foreach (var parte in descripcion.Split('|'))
            {
                if (parte.StartsWith(campo + ":"))
                    return parte.Substring((campo + ":").Length).Trim();
            }
            return string.Empty;
        }
    }
}