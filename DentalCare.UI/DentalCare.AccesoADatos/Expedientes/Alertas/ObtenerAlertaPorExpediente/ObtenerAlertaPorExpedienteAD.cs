using DentalCare.Abstraccion.AccesoADatos.Expediente.Alerta.ObtenerAlertaPorExpediente;
using DentalCare.Abstraccion.Modelo.Alertas;
using DentalCare.Abstraccion.Modelo.Expedientes;
using DentalCare.AccesoADatos.Expedientes.ObtenerTodosLosExpedientes;
using System.Linq;

namespace DentalCare.AccesoADatos.Alertas.ObtenerAlertaPorExpediente
{
    public class ObtenerAlertaPorExpedienteAD : IObtenerAlertaPorExpedienteAD
    {
        private readonly Contexto _contexto;

        public ObtenerAlertaPorExpedienteAD()
        {
            _contexto = new Contexto();
        }

        public ExpedienteDetalleDto Obtener(int idExpediente)
        {
            // Paso 1: traer expediente a memoria con tipos primitivos únicamente
            var rawExpediente = (
                from ue in _contexto.UsuarioExpedientes
                where ue.IdExpediente == idExpediente

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
                    DescConsentimiento = consentimiento.Descripcion,
                    NombreEstado = estado.NombreEstado,
                    expediente.IdEstado,
                    expediente.FechaDeCreacion,
                    expediente.IdAlerta
                }
            ).FirstOrDefault(); // <-- datos en memoria antes de llamar ExtraerCampo

            if (rawExpediente == null) return null;

            // Paso 2: traer alerta a memoria con tipos primitivos únicamente
            var rawAlerta = (
                from a in _contexto.Alertas
                where a.IdAlerta == rawExpediente.IdAlerta
                join estado in _contexto.Estados
                    on a.IdEstado equals estado.IdEstado into estadoGrupo
                from estado in estadoGrupo.DefaultIfEmpty()
                select new
                {
                    a.IdAlerta,
                    a.Descripcion,
                    a.NivelRiesgo,
                    a.IdEstado,
                    NombreEstado = estado != null ? estado.NombreEstado : ""
                }
            ).FirstOrDefault(); // <-- datos en memoria antes de llamar ExtraerCampo

            // Paso 3: mapear ExpedienteDto en memoria (ExtraerCampo funciona aquí)
            var expedienteDto = new ExpedienteDto
            {
                IdExpediente = rawExpediente.IdExpediente,
                IdPaciente = rawExpediente.IdPaciente,
                NombrePaciente = (rawExpediente.Nombre + " " + rawExpediente.PrimerApellido
                                  + " " + rawExpediente.SegundoApellido).Trim(),
                CedulaPaciente = rawExpediente.NumeroCedula,
                Identificacion = rawExpediente.NumeroCedula,
                NombreEstado = rawExpediente.NombreEstado,
                IdEstado = rawExpediente.IdEstado,
                FechaCreacion = rawExpediente.FechaDeCreacion,
                Objetivo = ObtenerTodosLosExpedientesAD.ExtraerCampo(rawExpediente.DescConsentimiento, "OBJETIVO"),
                Descripcion = ObtenerTodosLosExpedientesAD.ExtraerCampo(rawExpediente.DescConsentimiento, "DESCRIPCION"),
                Alternativas = ObtenerTodosLosExpedientesAD.ExtraerCampo(rawExpediente.DescConsentimiento, "ALTERNATIVAS"),
                Consecuencias = ObtenerTodosLosExpedientesAD.ExtraerCampo(rawExpediente.DescConsentimiento, "CONSECUENCIAS"),
                Otro = ObtenerTodosLosExpedientesAD.ExtraerCampo(rawExpediente.DescConsentimiento, "OTRO")
            };

            // Paso 4: mapear AlertaDto en memoria (ExtraerCampo funciona aquí)
            AlertaDto alertaDto = null;
            if (rawAlerta != null)
            {
                alertaDto = new AlertaDto
                {
                    IdAlerta = rawAlerta.IdAlerta,
                    IdExpediente = rawExpediente.IdExpediente,
                    NombrePaciente = expedienteDto.NombrePaciente,
                    NombreEstado = rawAlerta.NombreEstado,
                    NivelRiesgoMostrar = rawAlerta.NivelRiesgo,
                    NivelRiesgo = rawAlerta.NivelRiesgo,
                    IdEstado = rawAlerta.IdEstado,
                    AntecedentesMedicos = ExtraerCampo(rawAlerta.Descripcion, "ANTECEDENTES"),
                    Alergias = ExtraerCampo(rawAlerta.Descripcion, "ALERGIAS"),
                    MedicacionActual = ExtraerCampo(rawAlerta.Descripcion, "MEDICACION"),
                    CirugiasPrevias = ExtraerCampo(rawAlerta.Descripcion, "CIRUGIAS"),
                    Habitos = ExtraerCampo(rawAlerta.Descripcion, "HABITOS"),
                    AntecedentesOdontologicos = ExtraerCampo(rawAlerta.Descripcion, "ANT_ODONTO"),
                    MotivoConsulta = ExtraerCampo(rawAlerta.Descripcion, "MOTIVO"),
                    DiagnosticoInicial = ExtraerCampo(rawAlerta.Descripcion, "DIAGNOSTICO"),
                    PlanTratamiento = ExtraerCampo(rawAlerta.Descripcion, "PLAN")
                };
            }

            return new ExpedienteDetalleDto
            {
                Expediente = expedienteDto,
                Alerta = alertaDto
            };
        }

        // Método local para extraer campos del string concatenado
        private static string ExtraerCampo(string descripcion, string campo)
        {
            if (string.IsNullOrEmpty(descripcion)) return string.Empty;
            foreach (var parte in descripcion.Split('|'))
                if (parte.StartsWith(campo + ":"))
                    return parte.Substring((campo + ":").Length).Trim();
            return string.Empty;
        }
    }
}