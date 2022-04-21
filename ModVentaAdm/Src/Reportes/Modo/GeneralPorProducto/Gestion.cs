using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.Reportes.Modo.GeneralPorProducto
{

    public class Gestion : IGestion
    {

        private Reportes.Filtro.IFiltro _filtro;


        public Reportes.Filtro.IFiltro Filtros { get { return _filtro; } }


        public Gestion()
        {
            _filtro = new Filtro();
        }


        public void Generar(Reportes.Filtro.data data)
        {
            var filtro = new OOB.Reportes.VentaPorProducto.Filtro()
            {
                codigoSucursal = data.GetCodigoSucursal,
                desde = data.GetDesde,
                hasta = data.GetHasta,
            };
            var r01 = Sistema.MyData.Reportes_VentaPorProducto(filtro);
            if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Helpers.Msg.Error(r01.Mensaje);
                return;
            }
            Imprimir(r01.ListaD);
        }

        private void Imprimir(List<OOB.Reportes.VentaPorProducto.Ficha> list)
        {
            var pt = AppDomain.CurrentDomain.BaseDirectory + @"Reportes\GeneralPorProducto.rdlc";
            var ds = new DS();

            foreach (var it in list.ToList())
            {
                DataRow rt = ds.Tables["GeneralPorProducto"].NewRow();
                rt["codigoPrd"] = it.codigoPrd;
                rt["nombrePrd"] = it.nombrePrd;
                rt["cantidad"] = it.cantidad;
                rt["totalMonto"] = it.totalMonto * it.signo;
                rt["totalMontoDivisa"] = it.totalMontoDivisa * it.signo;
                rt["tipoDocumento"] = it.nombreDocumento;
                ds.Tables["GeneralPorProducto"].Rows.Add(rt);
            }

            var Rds = new List<ReportDataSource>();
            var pmt = new List<ReportParameter>();
            //pmt.Add(new ReportParameter("EMPRESA_RIF", Sistema.Negocio.CiRif));
            //pmt.Add(new ReportParameter("EMPRESA_NOMBRE", Sistema.Negocio.Nombre));
            //pmt.Add(new ReportParameter("EMPRESA_DIRECCION", Sistema.Negocio.DireccionFiscal));
            //pmt.Add(new ReportParameter("DOCUMENTO", ficha.documentoModo));
            Rds.Add(new ReportDataSource("GeneralPorProducto", ds.Tables["GeneralPorProducto"]));

            var frp = new ReporteFrm();
            frp.rds = Rds;
            frp.prmts = pmt;
            frp.Path = pt;
            frp.ShowDialog();
        }

    }

}