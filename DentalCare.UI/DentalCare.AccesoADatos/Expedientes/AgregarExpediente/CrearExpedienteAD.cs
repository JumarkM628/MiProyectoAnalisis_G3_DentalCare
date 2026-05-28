using DentalCare.Abstraccion.AccesoADatos.Expediente.CrearExpediente;
using DentalCare.Abstraccion.Modelo.Expedientes;
using DentalCare.AccesoADatos.Entidades.Expedientes;
using System;
using System.Linq;

namespace DentalCare.AccesoADatos.Expedientes.CrearExpediente
{
    public class CrearExpedienteAD : ICrearExpedienteAD
    {
        private readonly Contexto _contexto;

        public CrearExpedienteAD()
        {
            _contexto = new Contexto();
        }

        public void Crear(ExpedienteDto dto)
        {
            using (var transaccion = _contexto.Database.BeginTransaction())
            {
                try
                {

                    var cedula = _contexto.Cedulas
                        .FirstOrDefault(c => c.NumeroCedula == dto.Identificacion);

                    if (cedula == null)
                        throw new Exception("No se encontró un paciente con esa identificación.");

                    int idPaciente = cedula.IdUsuario;

                    int nuevoIdConsentimiento = _contexto.Consentimientos.Any()
                        ? _contexto.Consentimientos.Max(c => c.IdConsentimiento) + 1 : 1;

                    int nuevoIdExpediente = _contexto.Expedientes.Any()
                        ? _contexto.Expedientes.Max(e => e.IdExpediente) + 1 : 1;

                    int nuevoIdNota = _contexto.Database
                        .SqlQuery<int>("SELECT ISNULL(MAX(ID_NOTA), 0) + 1 FROM FIDE_NOTA_CLINICA_TB")
                        .FirstOrDefault();

                    int nuevoIdAlerta = _contexto.Database
                        .SqlQuery<int>("SELECT ISNULL(MAX(ID_ALERTA), 0) + 1 FROM FIDE_ALERTA_MEDICA_TB")
                        .FirstOrDefault();

                    int nuevoIdArchivo = _contexto.Database
                        .SqlQuery<int>("SELECT ISNULL(MAX(ID_ARCHIVO), 0) + 1 FROM FIDE_ARCHIVO_TB")
                        .FirstOrDefault();

                    int nuevoIdPieza = _contexto.Database
                        .SqlQuery<int>("SELECT ISNULL(MAX(ID_PIEZA), 0) + 1 FROM FIDE_PIEZA_DENTAL_TB")
                        .FirstOrDefault();

                    int nuevoIdOdontograma = _contexto.Database
                        .SqlQuery<int>("SELECT ISNULL(MAX(ID_ODONTOLOGIA), 0) + 1 FROM FIDE_ODONTOGRAMA_TB")
                        .FirstOrDefault();

                    int nuevoIdCita = _contexto.Database
                        .SqlQuery<int>("SELECT ISNULL(MAX(ID_CITA), 0) + 1 FROM FIDE_CITAS_TB")
                        .FirstOrDefault();

                    int nuevoIdTratamiento = _contexto.Database
                        .SqlQuery<int>("SELECT ISNULL(MAX(ID_TRATAMIENTO), 0) + 1 FROM FIDE_TRATAMIENTO_TB")
                        .FirstOrDefault();

                    int nuevoIdProcedimiento = _contexto.Database
                        .SqlQuery<int>("SELECT ISNULL(MAX(ID_PROCEDIMIENTO), 0) + 1 FROM FIDE_PROCEDIMIENTO_TB")
                        .FirstOrDefault();

                    string descripcionConcatenada =
                        $"OBJETIVO:{dto.Objetivo}|" +
                        $"DESCRIPCION:{dto.Descripcion ?? string.Empty}|" +
                        $"ALTERNATIVAS:{dto.Alternativas}|" +
                        $"CONSECUENCIAS:{dto.Consecuencias}|" +
                        $"OTRO:{dto.Otro ?? string.Empty}";

                    _contexto.Database.ExecuteSqlCommand(
                        "INSERT INTO FIDE_CONSENTIMIENTO_TB (ID_CONSENTIMIENTO, DESCRIPCION, FECHA, ID_ESTADO) VALUES (@p0, @p1, @p2, @p3)",
                        nuevoIdConsentimiento, descripcionConcatenada, DateTime.Now, dto.IdEstado);

                    _contexto.Database.ExecuteSqlCommand(
                        "INSERT INTO FIDE_NOTA_CLINICA_TB (ID_NOTA, DESCRIPCION, FECHA, ID_ESTADO) VALUES (@p0, @p1, @p2, @p3)",
                        nuevoIdNota, "Nota inicial", DateTime.Now, dto.IdEstado);

                    _contexto.Database.ExecuteSqlCommand(
                        "INSERT INTO FIDE_ALERTA_MEDICA_TB (ID_ALERTA, DESCRIPCION, NIVEL_RIESGO, ID_ESTADO) VALUES (@p0, @p1, @p2, @p3)",
                        nuevoIdAlerta, "Sin alertas", "Bajo", dto.IdEstado);

                    _contexto.Database.ExecuteSqlCommand(
                        "INSERT INTO FIDE_ARCHIVO_TB (ID_ARCHIVO, ID_TIPO_ARCHIVO, RUTA_ARCHIVO, FECHA, ID_ESTADO) VALUES (@p0, @p1, @p2, @p3, @p4)",
                        nuevoIdArchivo, 1, "/archivos/pendiente", DateTime.Now, dto.IdEstado);

                    _contexto.Database.ExecuteSqlCommand(
                        "INSERT INTO FIDE_PIEZA_DENTAL_TB (ID_PIEZA, NUMERO_PIEZA, ID_ESTADO) VALUES (@p0, @p1, @p2)",
                        nuevoIdPieza, "11", dto.IdEstado);

                    _contexto.Database.ExecuteSqlCommand(
                        "INSERT INTO FIDE_ODONTOGRAMA_TB (ID_ODONTOLOGIA, FECHA, ID_PIEZA, ID_ESTADO) VALUES (@p0, @p1, @p2, @p3)",
                        nuevoIdOdontograma, DateTime.Now, nuevoIdPieza, dto.IdEstado);

                    _contexto.Database.ExecuteSqlCommand(
                        "INSERT INTO FIDE_CITAS_TB (ID_CITA, FECHA, ID_MOTIVO, ID_CANCELACION, ID_ESTADO) VALUES (@p0, @p1, @p2, @p3, @p4)",
                        nuevoIdCita, DateTime.Now, 1, 1, dto.IdEstado);

                    _contexto.Database.ExecuteSqlCommand(
                        "INSERT INTO FIDE_TRATAMIENTO_TB (ID_TRATAMIENTO, DESCRIPCION, FECHA_INICIO, FECHA_FIN, ID_ESTADO) VALUES (@p0, @p1, @p2, @p3, @p4)",
                        nuevoIdTratamiento, "Tratamiento inicial", DateTime.Now, null, dto.IdEstado);

                    _contexto.Database.ExecuteSqlCommand(
                        "INSERT INTO FIDE_PROCEDIMIENTO_TB (ID_PROCEDIMIENTO, ID_CITA, ID_TRATAMIENTO, DESCRIPCION, FECHA, OBSERVACIONES, ID_ESTADO) VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6)",
                        nuevoIdProcedimiento, nuevoIdCita, nuevoIdTratamiento, "Procedimiento inicial", DateTime.Now, "Sin observaciones", dto.IdEstado);

                    _contexto.Database.ExecuteSqlCommand(
                        @"INSERT INTO FIDE_EXPEDIENTE_TB 
                          (ID_EXPEDIENTE, FECHA_DE_CREACION, ID_PROCEDIMIENTO, ID_NOTA, ID_ARCHIVO, ID_ALERTA, ID_CONSENTIMIENTO, ID_ODONTOGRAMA, ID_ESTADO)
                          VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8)",
                        nuevoIdExpediente, DateTime.Now, nuevoIdProcedimiento, nuevoIdNota,
                        nuevoIdArchivo, nuevoIdAlerta, nuevoIdConsentimiento, nuevoIdOdontograma, dto.IdEstado);

                    _contexto.Database.ExecuteSqlCommand(
                        "INSERT INTO FIDE_USUARIO_EXPEDIENTE_TB (ID_USUARIO, ID_EXPEDIENTE) VALUES (@p0, @p1)",
                        idPaciente, nuevoIdExpediente);

                    transaccion.Commit();
                }
                catch
                {
                    transaccion.Rollback();
                    throw;
                }
            }
        }

        public bool ExisteExpedientePorCedula(string numeroCedula)
        {
            var cedula = _contexto.Cedulas
                .FirstOrDefault(c => c.NumeroCedula == numeroCedula);

            if (cedula == null) return false;

            return _contexto.UsuarioExpedientes
                .Any(ue => ue.IdUsuario == cedula.IdUsuario);
        }
    }
}
