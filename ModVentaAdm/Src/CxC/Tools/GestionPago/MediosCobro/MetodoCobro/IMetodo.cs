using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.CxC.Tools.GestionPago.MediosCobro.MetodoCobro
{
    
    public interface IMetodo: IGestion, Gestion.IAbandonar, Gestion.IProcesar
    {

    }

}