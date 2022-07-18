using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Data.Prov
{

    class Helpers
    {

        public static OOB.Resultado.FichaEntidad<OOB.Permiso.Entidad.Ficha>
            PermisoRt(Func<string, DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha>> met, string grupo)
        {
            var rs = new OOB.Resultado.FichaEntidad<OOB.Permiso.Entidad.Ficha>();

            var rt = met(grupo);
            if (rt.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                rs.Mensaje = rt.Mensaje;
                rs.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return rs;
            }
            var s = rt.Entidad;
            var nr = new OOB.Permiso.Entidad.Ficha()
            {
                estatus = s.estatus,
                seguridad = s.seguridad,
            };
            rs.Entidad = nr;

            return rs;
        }
    }

}
