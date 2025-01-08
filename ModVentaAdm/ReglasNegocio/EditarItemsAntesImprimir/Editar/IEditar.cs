using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.ReglasNegocio.EditarItemsAntesImprimir.Editar
{
    public interface IEditar: Src.IGestion
    {
        Utils.Control.Boton.Salir.ISalir BtSalir {get;}
        Utils.Control.Boton.Procesar.IProcesar BtProcesar { get; }
        bool EdicionIsOk { get; }
        string GetDetalle { get; }
        string GetTitulo { get; }
        void setTitulo(string text);
        void setItemDescripcion(string text);
        void Guardar();
    }
}
