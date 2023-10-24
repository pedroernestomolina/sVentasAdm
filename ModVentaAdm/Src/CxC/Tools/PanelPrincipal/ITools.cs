using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.CxC.Tools.PanelPrincipal
{
    public interface ITools: IGestion
    {
        decimal GetMontoPendientePorCobrar { get; }
        BindingSource CtasPendGetSource { get; }
        bool AbandonarIsOk { get; }
        void AbandonarFicha();


        void BuscarCtasPendientes();
        void AgregarCta();
        void AgregarNCrAdm();
        void AgregarNDbAdm();
        bool AgregarCtaIsOk { get; }
        bool AgregarNCrAdmIsOk { get; }
        bool AgregarNDbAdmIsOk { get; }


        void ListadoCtasPend();
        void DocDetallesPend();
        void GestionPago();


        //
        void AgregarAnticipo();
        void AdmDocAnticipos();
        void AdmPagosRecibidos();
    }
}