using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.DocVenta.NotaCreditoAdm.GenerarAdm.Vista
{
    public interface IDoc
    {
        string Get_EntidadAplicarNotaCredito_DatosCliente { get; }
        IDocGenerar DocGenerar { get; }
        string Get_CadenaBusq { get; }
        bool BusquedaIsOk { get; }
        IdataGuardar Get_DatosGuardar { get; }
        //
        void setCadenaBuscar(string cadena);
        void setFechaServidor(DateTime fecha);
        void BuscarCliente();
        void Inicializa();
        void Limpiar();
        bool ValidarDataIsOk();
    }
}