using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.Reportes.Modo.GeneralPorGrupo
{
    
    public class Gestion: IGestion
    {

        private Reportes.Filtro.IFiltro _filtro;


        public Reportes.Filtro.IFiltro Filtros { get { return _filtro; } }


        public Gestion()
        {
            _filtro = new Filtro();
        }


        public void Generar(Reportes.Filtro.data data)
        {
            var filtro = new OOB.Reportes.GeneralPorGrupo.Filtro()
            {
                desde = data.GetDesde,
                hasta = data.GetHasta,
                codigoSucursal = data.GetCodigoSucursal,
            };
            var r01 = Sistema.MyData.Reportes_GeneralPorGrupo(filtro);
            if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Helpers.Msg.Error(r01.Mensaje);
                return;
            }
            Imprimir(r01.ListaD);
        }

        private void Imprimir(List<OOB.Reportes.GeneralPorGrupo.Ficha> list)
        {
            var pt = AppDomain.CurrentDomain.BaseDirectory + @"Reportes\GeneralPorGrupo.rdlc";
            var ds = new DS();

            foreach (var it in list.ToList())
            {
                DataRow rt = ds.Tables["GeneralPorGrupo"].NewRow();
                rt["costo"] = it.costo;
                rt["venta"] = it.venta;
                rt["utilidadMonto"] = it.utilidadMonto;
                rt["utilidadPorc"] = it.utilidadPorc;
                rt["grupo"] = it.nombreGrupo;
                rt["costoDivisa"] = it.costoDivisa;
                rt["ventaDivisa"] = it.ventaDivisa;
                rt["utilidadDivisa"] = it.utilidadMontoDivisa;
                ds.Tables["GeneralPorGrupo"].Rows.Add(rt);
            }

            var Rds = new List<ReportDataSource>();
            var pmt = new List<ReportParameter>();
            //pmt.Add(new ReportParameter("EMPRESA_RIF", Sistema.Negocio.CiRif));
            //pmt.Add(new ReportParameter("EMPRESA_NOMBRE", Sistema.Negocio.Nombre));
            //pmt.Add(new ReportParameter("EMPRESA_DIRECCION", Sistema.Negocio.DireccionFiscal));
            //pmt.Add(new ReportParameter("DOCUMENTO", ficha.documentoModo));
            Rds.Add(new ReportDataSource("GeneralPorGrupo", ds.Tables["GeneralPorGrupo"]));

            var frp = new ReporteFrm();
            frp.rds = Rds;
            frp.prmts = pmt;
            frp.Path = pt;
            frp.ShowDialog();
        }

    }

}