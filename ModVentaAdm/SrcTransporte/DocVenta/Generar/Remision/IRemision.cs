using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.SrcTransporte.DocVenta.Generar.Remision
{
    public interface IRemision
    {
        BindingSource SourceItems_Get { get; }
        string ItemId_Get { get;  }
        data DocRemision { get; }
        string DocNombre_Get { get; }
        string DocNumero_Get { get; }
        string DocFecha_Get { get; }
        void setFichaId(string id);
        void setClienteBuscar(OOB.Maestro.Cliente.Entidad.Ficha ficha);

        void Inicializa();
        void CargarData();
        void Limpiar();

        bool RemisionIsOK { get; }
        void Buscar();

        void setHabilitarCargarDocRemision(bool hab);
    }
}