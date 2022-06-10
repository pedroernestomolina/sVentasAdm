using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Data.Infra
{
    
    public interface ISistema
    {

        OOB.Resultado.FichaEntidad<OOB.Sistema.Empresa.Entidad.Ficha> 
            Sistema_Empresa_GetFicha();
        OOB.Resultado.FichaEntidad<OOB.Sistema.TipoDocumento.Entidad.Ficha> 
            Sistema_TipoDocumento_GetFichaById(string id);


        OOB.Resultado.Lista<OOB.Sistema.Vendedor.Entidad.Ficha> 
            Sistema_Vendedor_GetLista();
        OOB.Resultado.Lista<OOB.Sistema.Cobrador.Entidad.Ficha> 
            Sistema_Cobrador_GetLista();
        OOB.Resultado.Lista<OOB.Sistema.Estado.Entidad.Ficha> 
            Sistema_Estado_GetLista();
        OOB.Resultado.Lista<OOB.Sistema.Transporte.Entidad.Ficha> 
            Sistema_Transporte_GetLista();
        OOB.Resultado.Lista<OOB.Sistema.Deposito.Entidad.Ficha> 
            Deposito_GetLista(OOB.Sistema.Deposito.Lista.Filtro filtro);
        OOB.Resultado.Lista<OOB.Sistema.Fiscal.Entidad.Ficha> 
            Sistema_TasaFiscal_GetLista();


        OOB.Resultado.FichaEntidad<string>
            Sistema_GetCodigoSucursal();

    }

}