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

    }

}