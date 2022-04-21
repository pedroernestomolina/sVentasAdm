using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.Reportes.Modo.Utilidad.Ventas
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
            var filtro = new OOB.Reportes.Utilidad.Filtro()
            {
                desde = data.GetDesde,
                hasta = data.GetHasta,
                codSucursal = data.GetCodigoSucursal,
            };
            var r01 = Sistema.MyData.Reportes_UtilidadVenta(filtro);
            if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError) 
            {
                Helpers.Msg.Error(r01.Mensaje);
                return;
            }
            Imprimir(r01.ListaD);
        }

        private void Imprimir(List<OOB.Reportes.Utilidad.Venta.Ficha> list)
        {
            var pt = AppDomain.CurrentDomain.BaseDirectory + @"Reportes\UtilidadVenta.rdlc";
            var ds = new DS();

            foreach (var it in list.ToList())
            {
                DataRow rt = ds.Tables["UtilidadVenta"].NewRow();
                rt["FechaHora"] = it.fecha.ToShortDateString();
                rt["Documento"] = it.documento;
                rt["Cliente"] = it.clienteCiRif.Trim() + Environment.NewLine + it.clienteNombre.Trim();
                rt["docNombre"] = it.nombreDoc;
                rt["Sucursal"] = it.sucNombre+Environment.NewLine+it.sucCodigo;
                rt["costo"] = (it.costoNeto * it.signoDoc)/it.factorDoc;
                rt["venta"] = (it.ventaNeta * it.signoDoc)/it.factorDoc;
                rt["utilidad"] = (it.utilidad * it.signoDoc)/it.factorDoc;
                rt["utilidadP"] = it.utilidadp * it.signoDoc;
                ds.Tables["UtilidadVenta"].Rows.Add(rt);
            }

            var Rds = new List<ReportDataSource>();
            var pmt = new List<ReportParameter>();
            //pmt.Add(new ReportParameter("EMPRESA_RIF", Sistema.Negocio.CiRif));
            //pmt.Add(new ReportParameter("EMPRESA_NOMBRE", Sistema.Negocio.Nombre));
            //pmt.Add(new ReportParameter("EMPRESA_DIRECCION", Sistema.Negocio.DireccionFiscal));
            //pmt.Add(new ReportParameter("DOCUMENTO", ficha.documentoModo));
            Rds.Add(new ReportDataSource("UtilidadVenta", ds.Tables["UtilidadVenta"]));

            var frp = new ReporteFrm();
            frp.rds = Rds;
            frp.prmts = pmt;
            frp.Path = pt;
            frp.ShowDialog();
        }

    }

}