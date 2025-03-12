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
        public OOB.Resultado.FichaEntidad<OOB.CxC.CargarData.Cliente.Ficha> 
            CxC_CapturarData_Cliente_ById(string id)
        {
            var rt = new OOB.Resultado.FichaEntidad<OOB.CxC.CargarData.Cliente.Ficha>();
            //
            var r01 = MyData.CxC_CapturarData_Cliente_ById(id);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                throw new Exception(r01.Mensaje);
            }
            if (r01.Entidad==null)
            {
                throw new Exception("PROBLEMA AL CARGAR ENTIDAD CLIENTE");
            }
            var s= r01.Entidad;
            var nr = new OOB.CxC.CargarData.Cliente.Ficha()
            {
                aliasClient = s.aliasClient,
                ciRifClient = s.ciRifClient,
                codigoClient = s.codigoClient,
                diasCreditoClient = s.diasCreditoClient,
                dirFiscalClient = s.dirFiscalClient,
                emailClient = s.emailClient,
                estatusClient = s.estatusClient,
                estatusCreditoClient = s.estatusCreditoClient,
                fechaUltPagoClient = s.fechaUltPagoClient,
                fechaUltVentaClient = s.fechaUltVentaClient,
                idClient = s.idClient,
                idCobrador = s.idCobrador,
                idVendedor = s.idVendedor,
                limiteCreditoClient = s.limiteCreditoClient,
                montoAnticiposClient = s.montoAnticiposClient,
                nombreCobrad = s.nombreCobrad,
                nombreRazonSocialClient = s.nombreRazonSocialClient,
                nombreVend = s.nombreVend,
                telefono2Client = s.telefono2Client,
                telefonoClient = s.telefonoClient,
                codigoCobrad= s.codigoCobrad,
                codigoVend=s.codigoVend,
            };
            rt.Entidad = nr;
            //
            return rt;
        }
    }
}