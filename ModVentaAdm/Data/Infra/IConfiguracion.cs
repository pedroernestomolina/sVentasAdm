using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Data.Infra
{
    
    public interface IConfiguracion
    {

        OOB.Resultado.FichaEntidad<OOB.Configuracion.BusquedaCliente.Entidad.Ficha> 
            Configuracion_BusquedaCliente();
        OOB.Resultado.FichaEntidad<OOB.Configuracion.BusquedaProducto.Enumerado.EnumPreferenciaBusqueda> 
            Configuracion_BusquedaPreferenciaProducto();
        OOB.Resultado.FichaEntidad<decimal> 
            Configuracion_FactorDivisa();
        OOB.Resultado.FichaEntidad<bool> 
            Configuracion_RupturaPorExistencia();
        OOB.Resultado.FichaEntidad <int> 
            Configuracion_CantDocVisualizar();

    }

}