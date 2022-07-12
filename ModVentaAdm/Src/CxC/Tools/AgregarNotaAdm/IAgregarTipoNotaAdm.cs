using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.CxC.Tools.AgregarNotaAdm
{
    
    public interface IAgregarTipoNotaAdm: IGestion
    {


        string GetTipoNotaAdm { get; }
        string GetNumeroDocConsecutivo { get; }
        DateTime GetFechaEmision { get; }


        bool CargarData();
        bool Procesar(dataAgregar data);


    }

}