using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.CxC.Tools.GestionPago
{

    public interface IGestionPago : IGestion, Gestion.IAbandonar
    {


        string GetClienteData { get; }
        decimal GetMontoResta { get; }
        decimal GetMontoAbonar { get; }
        decimal GetMontoPend { get; }
        int GetCantDoc { get; }
        BindingSource DocPendGetSource { get; }
        string GetNotas { get; }


        void setIdCliente(string id);
        void VerFichaCliente();


        void MarcarDesmarcarDoc();
        void LimpiarAbonoMarcado();
        void AplicarPagos();
        bool ProcesarPagoIsOk { get; }

    }

}