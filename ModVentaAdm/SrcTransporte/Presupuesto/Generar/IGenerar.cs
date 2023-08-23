using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.SrcTransporte.Presupuesto.Generar
{
    public interface IGenerar : Src.IGestion, Src.Gestion.IAbandonar, Src.Gestion.IProcesar, Src.Gestion.IPendiente
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
        bool LimpiarDocumentoIsOK { get;  }
        void EditarDocumento();
        bool EditarDocumentoIsOK { get; }
        void MostrarBeneficio();

        bool RemisionIsOK { get; }
        void BuscarRemision();
        bool GetEsPorRemisionLaCargaDocumento { get; }
        void setEsPorRemisionLaCargaDocumento();
        void IniciarEnLimpio();

        int CntDocPendiente { get;  }
        bool AbrirPedienteIsOK { get; }
        void BuscarPendiente();
    }
}