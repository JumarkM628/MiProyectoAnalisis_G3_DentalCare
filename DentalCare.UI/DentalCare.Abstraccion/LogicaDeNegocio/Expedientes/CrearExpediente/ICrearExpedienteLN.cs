using DentalCare.Abstraccion.Modelo.Expedientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.Abstraccion.LogicaDeNegocio.Expedientes.CrearExpediente
{
    public interface ICrearExpedienteLN
    {
        string Crear(ExpedienteDto dto);


    }
}
