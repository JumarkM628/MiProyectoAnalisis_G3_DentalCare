using DentalCare.Abstraccion.AccesoADatos.Expediente.Alerta.GuardarAlerta;
using DentalCare.Abstraccion.Modelo.Alertas;
using System.Linq;

namespace DentalCare.AccesoADatos.Alertas.GuardarAlerta
{
    public class GuardarAlertaAD : IGuardarAlertaAD
    {
        private readonly Contexto _contexto;

        public GuardarAlertaAD()
        {
            _contexto = new Contexto();
        }

        public void Guardar(int idExpediente, AlertaDto dto)
        {
            using (var transaccion = _contexto.Database.BeginTransaction())
            {
                try
                {
                    var expediente = _contexto.Expedientes
                        .First(e => e.IdExpediente == idExpediente);

                    var alerta = _contexto.Alertas
                        .First(a => a.IdAlerta == expediente.IdAlerta);

                    alerta.Descripcion =
                        $"ANTECEDENTES:{dto.AntecedentesMedicos}|" +
                        $"ALERGIAS:{dto.Alergias ?? string.Empty}|" +
                        $"MEDICACION:{dto.MedicacionActual ?? string.Empty}|" +
                        $"CIRUGIAS:{dto.CirugiasPrevias ?? string.Empty}|" +
                        $"HABITOS:{dto.Habitos ?? string.Empty}|" +
                        $"ANT_ODONTO:{dto.AntecedentesOdontologicos ?? string.Empty}|" +
                        $"MOTIVO:{dto.MotivoConsulta}|" +
                        $"DIAGNOSTICO:{dto.DiagnosticoInicial}|" +
                        $"PLAN:{dto.PlanTratamiento}";

                    alerta.NivelRiesgo = dto.NivelRiesgo;
                    alerta.IdEstado = dto.IdEstado;

                    _contexto.SaveChanges();
                    transaccion.Commit();
                }
                catch
                {
                    transaccion.Rollback();
                    throw;
                }
            }
        }
    }
}
