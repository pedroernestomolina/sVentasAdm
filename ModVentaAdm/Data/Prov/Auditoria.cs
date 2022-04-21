using ModVentaAdm.Data.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Data.Prov
{

    public partial class DataPrv : IData
    {

        public OOB.Resultado.FichaEntidad<OOB.Auditoria.Entidad.Ficha> Auditoria_Documento_GetFichaBy(OOB.Auditoria.Buscar.Ficha ficha)
        {
            var rt = new OOB.Resultado.FichaEntidad<OOB.Auditoria.Entidad.Ficha>();

            var fichaDTO = new DtoLibPos.Auditoria.Buscar.Ficha()
            {
                autoDocumento = ficha.autoDocumento,
                autoTipoDocumento = ficha.autoTipoDocumento,
            };
            var r01 = MyData.Auditoria_Documento_GetFichaBy(fichaDTO);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                rt.Mensaje = r01.Mensaje;
                rt.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return rt;
            }

            var s = r01.Entidad;
            var nr = new OOB.Auditoria.Entidad.Ficha()
            {
                estacionEquipo = s.estacionEquipo,
                fecha = s.fecha,
                hora = s.hora,
                motivo = s.motivo,
                usuAuto = s.usuAuto,
                usuCodigo = s.usuCodigo,
                usuNombre = s.usuNombre,
            };
            rt.Entidad = nr;

            return rt;
        }

    }

}