using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.Reportes.Modo.GeneralDocumento
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
            var filtro = new OOB.Reportes.GeneralDocumento.Filtro()
            {
                desde = data.GetDesde,
                hasta = data.GetHasta,
                idSucursal = data.GetCodigoSucursal,
                tipoDocFactura = data.GetTipoDocFactura,
                tipoDocNtCredito = data.GetTipoDocNtCredito,
                tipoDocNtDebito = data.GetTipoDocNtDebito,
                tipoDocNtEntrega = data.GetTipoDocNtEntrega,
            };
            var r01 = Sistema.MyData.Reportes_GeneralDocumento(filtro);
            if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError) 
            {
                Helpers.Msg.Error(r01.Mensaje);
                return;
            }
            Imprimir(r01.ListaD);
        }

        private void Imprimir(List<OOB.Reportes.GeneralDocumento.Ficha> list)
        {
            var pt = AppDomain.CurrentDomain.BaseDirectory + @"Reportes\GeneralDocumento.rdlc";
            var ds = new DS();

            foreach (var it in list.ToList())
            {
                DataRow rt = ds.Tables["GeneralDocumento"].NewRow();
                rt["FechaHora"] = it.fecha.ToShortDateString();
                rt["Documento"] = it.documento;
                rt["Control"] = it.control;
                rt["Serie"] = it.serie;
                rt["Cliente"] = it.clienteNombre+Environment.NewLine+it.clienteCiRif;
                rt["monto"] = it.total*it.signoDoc;
                rt["montoDivisa"] = it.totalDivisa*it.signoDoc;
                rt["docNombre"] = it.nombreDoc;
                rt["Renglones"] = it.renglones;
                rt["Dscto"] = it.montoDscto;
                rt["Cargo"] = it.montoCargo;
                rt["Sucursal"] = it.sucNombre+Environment.NewLine+it.sucCodigo;
                rt["Estacion"] = it.estacion;
                ds.Tables["GeneralDocumento"].Rows.Add(rt);
            }

            var Rds = new List<ReportDataSource>();
            var pmt = new List<ReportParameter>();
            //pmt.Add(new ReportParameter("EMPRESA_RIF", Sistema.Negocio.CiRif));
            //pmt.Add(new ReportParameter("EMPRESA_NOMBRE", Sistema.Negocio.Nombre));
            //pmt.Add(new ReportParameter("EMPRESA_DIRECCION", Sistema.Negocio.DireccionFiscal));
            //pmt.Add(new ReportParameter("DOCUMENTO", ficha.documentoModo));
            Rds.Add(new ReportDataSource("GeneralDocumento", ds.Tables["GeneralDocumento"]));

            var frp = new ReporteFrm();
            frp.rds = Rds;
            frp.prmts = pmt;
            frp.Path = pt;
            frp.ShowDialog();
        }

    }

}