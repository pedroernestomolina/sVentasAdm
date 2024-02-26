using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.SrcTransporte.DocVenta.Generar
{
    public interface IGenerar : Src.IGestion, Src.Gestion.IAbandonar, Src.Gestion.IProcesar
    {
        BindingSource SourceItems_Get { get; }
        data Ficha { get; }
        string NotasObserv_Get { get; }
        Remision.IRemision Remision { get; }


        void NuevoDocumento();
        void AgregarItem();
        void EliminarItem();
        void EditarItem();
        void setNotas(string desc);
        void LimpiarDocumento();
        bool LimpiarDocumentoIsOK { get; }
        void EditarDocumento();
        bool EditarDocumentoIsOK { get; }

        bool RemisionIsOK { get; }
        void BuscarRemision();
        void IniciarEnLimpio();

        string TipoDocumento_Get { get; }
        void EditarFactorDivisa();
        void ActivarIGTF();
        void PeriodoLapso();
        void ActivarISLR();
        void DocumentoNumeroGenerar();
        bool DocumentoNumeroGenerarIsOk { get; }
    }
}