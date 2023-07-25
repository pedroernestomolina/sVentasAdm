using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.SrcTransporte.AdmDocumentos
{
    public interface IGestion
    {
        BindingSource ItemsSource { get; }
        string ItemsEncontrados { get; }
        BindingSource SucursalSource { get; }
        BindingSource TipoDocSource { get; }
        DateTime GetDesde { get; }
        DateTime GetHasta { get; }
        string GetIdSucursal { get; }
        string GetIdTipoDoc { get; }


        void Inicializa();
        void Inicia();
        void Buscar();
        void AnularItem();
        void LimpiarFiltros();
        void LimpiarData();
        void VisualizarDocumento();
        void Imprimir();
        void setFechaDesde(DateTime fecha);
        void setFechaHasta(DateTime fecha);
        void setSucursal(string autoId);
        void setTipoDoc(string id);
        void CorrectorDocumento();
        void Filtros();
        void VerAnulacion();
    }
}