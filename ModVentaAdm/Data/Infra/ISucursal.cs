using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Data.Infra
{
    
    public interface ISucursal
    {


        OOB.Resultado.FichaEntidad<OOB.Sucursal.Entidad.Ficha> 
            Sucursal_GetFichaById(string id);
        OOB.Resultado.Lista<OOB.Sucursal.Entidad.Ficha> 
            Sucursal_GetLista();
        OOB.Resultado.FichaEntidad<string>
            Sucursal_GetId_ByCodigo(string codigoSuc);


    }

}