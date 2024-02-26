using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.DocVenta.NotaCreditoAdm.Generar.Vista
{
    public interface IDoc
    {
        string Get_DocAplicarNotaCredito_DatosCliente { get; }
        string Get_DocAplicarNotaCredito_DatosDocumento { get; }
        IDocGenerar DocGenerar { get; }
        string Get_CadenaBusq { get; }
        bool BusquedaIsOk { get; }
        IdataGuardar Get_DatosGuardar { get; }
        //
        void setCadenaBuscar(string cadena);
        void BuscarDocumentos();
        void Inicializa();
        void Limpiar();
        bool ValidarDataIsOk();
    }
}