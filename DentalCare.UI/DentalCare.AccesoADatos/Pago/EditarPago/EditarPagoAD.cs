using System.Linq;
using DentalCare.Abstraccion.AccesoADatos.Pago.EditarPago;

namespace DentalCare.AccesoADatos.Pago.EditarPago
{
    public class EditarPagoAD : IEditarPagoAD
    {
        public bool Existe(int idContabilidad)
        {
            using (var ctx = new Contexto())
                return ctx.Contabilidades.Any(c => c.IdContabilidad == idContabilidad);
        }

        public bool EstaPagado(int idContabilidad)
        {
            using (var ctx = new Contexto())
            {
                return (from c in ctx.Contabilidades
                        join e in ctx.Estados on c.IdEstado equals e.IdEstado
                        where c.IdContabilidad == idContabilidad && e.NombreEstado == "Activo"
                        select c.IdContabilidad).Any();
            }
        }

        public void Actualizar(int idContabilidad, decimal monto, int idMetodoPago)
        {
            using (var ctx = new Contexto())
            {
                var pago = ctx.Contabilidades.First(c => c.IdContabilidad == idContabilidad);
                pago.Monto = monto;
                pago.IdMetodoPago = idMetodoPago;
                ctx.SaveChanges();
            }
        }
    }
}
