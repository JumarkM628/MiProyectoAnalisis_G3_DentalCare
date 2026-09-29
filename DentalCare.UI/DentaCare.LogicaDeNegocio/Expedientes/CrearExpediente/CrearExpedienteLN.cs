using DentalCare.Abstraccion.AccesoADatos.Expediente.CrearExpediente;
using DentalCare.Abstraccion.LogicaDeNegocio.Expedientes.CrearExpediente;
using DentalCare.Abstraccion.Modelo.Expedientes;
using DentalCare.AccesoADatos.Expedientes.CrearExpediente;
using System;

namespace DentalCare.LogicaDeNegocio.Expedientes.CrearExpediente
{
    public class CrearExpedienteLN : ICrearExpedienteLN
    {
        private readonly ICrearExpedienteAD _crearAD;

        public CrearExpedienteLN()
        {
            _crearAD = new CrearExpedienteAD();
        }

        public string Crear(ExpedienteDto dto)
        {
            if (_crearAD.ExisteExpedientePorCedula(dto.Identificacion))
                return "El paciente ya posee un expediente registrado en el sistema.";

            try
            {
                _crearAD.Crear(dto);
                return null;
            }
            catch (Exception ex)
            {
                // Antes esta excepción no se capturaba en ningún nivel y llegaba
                // sin control al controlador, provocando una página de error genérica
                // en vez de un mensaje comprensible para el usuario.
                if (ex.Message == "No se encontró un paciente con esa identificación.")
                    return "No se encontró ningún paciente registrado con esa identificación. Verifique el número o registre primero al paciente con rol 'Paciente'.";

                // --- DIAGNÓSTICO TEMPORAL: quitar este bloque una vez identificada la causa ---
                var interna = ex;
                var cadena = "";
                int nivel = 0;
                while (interna != null)
                {
                    var stackCorto = (interna.StackTrace ?? "").Replace(Environment.NewLine, " >> ");
                    cadena += $"[NIVEL {nivel}] {interna.GetType().Name}: {interna.Message} ### STACK: {stackCorto} ||| ";
                    interna = interna.InnerException;
                    nivel++;
                }
                return "DEBUG TEMPORAL — " + cadena;

                // return "Ocurrió un error al guardar el expediente. Verifique los datos e intente nuevamente.";
            }
        }
    }
}