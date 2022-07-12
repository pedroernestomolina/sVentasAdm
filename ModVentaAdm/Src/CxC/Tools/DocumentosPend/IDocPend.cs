using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.CxC.Tools.DocumentosPend
{
    
    public interface IDocPend: IGestion, Gestion.IAbandonar
    {

        string GetClienteData { get; }
        decimal GetMontoImporte { get; }
        decimal GetMontoAcumulado { get; }
        decimal GetMontoResta { get; }
        int GetCantDoc { get; }
        BindingSource DocPendGetSource { get; }
        string GetNotas { get; }


        void setIdCliente(string id);
        void VerFichaCliente();
        void ReporteDocPend();

    }

}