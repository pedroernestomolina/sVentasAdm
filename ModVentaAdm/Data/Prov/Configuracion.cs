using ModVentaAdm.Data.Infra;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Data.Prov
{
    
    public partial class DataPrv : IData
    {

        public OOB.Resultado.FichaEntidad<OOB.Configuracion.BusquedaCliente.Entidad.Ficha> Configuracion_BusquedaCliente()
        {
            var rt = new OOB.Resultado.FichaEntidad<OOB.Configuracion.BusquedaCliente.Entidad.Ficha>();

            var r01 = MyData.ConfiguracionAdm_BusquedaCliente();
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                rt.Mensaje = r01.Mensaje;
                rt.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return rt;
            }

            var nr = new OOB.Configuracion.BusquedaCliente.Entidad.Ficha()
            {
                Usuario = r01.Entidad.Usuario,
            };
            rt.Entidad = nr;

            return rt;
        }

        public OOB.Resultado.FichaEntidad<decimal> Configuracion_FactorDivisa()
        {
            var rt = new OOB.Resultado.FichaEntidad<decimal>();

            var r01 = MyData.Configuracion_FactorDivisa();
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                rt.Mensaje = r01.Mensaje;
                rt.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return rt;
            }
            var m1 = 0.0m;
            var cnf = r01.Entidad;
            if (cnf.Trim() != "")
            {
                var style = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands;
                var culture = CultureInfo.CreateSpecificCulture("es-ES");
                //var culture = CultureInfo.CreateSpecificCulture("en-EN");
                Decimal.TryParse(cnf, style, culture, out m1);
            }
            rt.Entidad = m1;

            return rt;
        }

        public OOB.Resultado.FichaEntidad<OOB.Configuracion.BusquedaProducto.Enumerado.EnumPreferenciaBusqueda> Configuracion_BusquedaPreferenciaProducto()
        {
            var rt = new OOB.Resultado.FichaEntidad<OOB.Configuracion.BusquedaProducto.Enumerado.EnumPreferenciaBusqueda>();

            var r01 = MyData.ConfiguracionAdm_PreferenciaBusquedaProducto();
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                rt.Mensaje = r01.Mensaje;
                rt.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return rt;
            }

            rt.Entidad = (OOB.Configuracion.BusquedaProducto.Enumerado.EnumPreferenciaBusqueda)r01.Entidad;
            return rt;
        }

        public OOB.Resultado.FichaEntidad<bool> Configuracion_RupturaPorExistencia()
        {
            var rt = new OOB.Resultado.FichaEntidad<bool>();

            var r01 = MyData.ConfiguracionAdm_RupturaPorExistencia();
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                rt.Mensaje = r01.Mensaje;
                rt.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return rt;
            }
            rt.Entidad = r01.Entidad == "SI";

            return rt;
        }

    }

}