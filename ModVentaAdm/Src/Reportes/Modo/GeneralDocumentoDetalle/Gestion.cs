using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.Reportes.Modo.GeneralDocumentoDetalle
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
            var filtro = new OOB.Reportes.GeneralDocumentoDetalle.Filtro()
            {
                palabraClave = data.PalabraClave,
                codigoSucursal = data.GetCodigoSucursal,
                desdeFecha = data.GetDesde,
                hastaFecha = data.GetHasta,
                tipoDocFactura = data.GetTipoDocFactura,
                tipoDocNtCredito = data.GetTipoDocNtCredito,
                tipoDocNtDebito = data.GetTipoDocNtDebito,
                tipoDocNtEntrega = data.GetTipoDocNtEntrega,
            };
            var r01 = Sistema.MyData.Reportes_GeneralDocumentoDetalle(filtro);
            if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError) 
            {
                Helpers.Msg.Error(r01.Mensaje);
                return;
            }
            Imprimir(r01.ListaD);
        }

        private void Imprimir(List<OOB.Reportes.GeneralDocumentoDetalle.Ficha> list)
        {
            var pt = AppDomain.CurrentDomain.BaseDirectory + @"Reportes\GeneralDocumentoDet.rdlc";
            var ds = new DS();

            var td = "";
            foreach (var it in list.ToList())
            {
                if (td != it.documento)
                {
                    td = it.documento;
                }
                else 
                {
                    it.renglones = 0;
                    it.total = 0;
                }

                DataRow rt = ds.Tables["GeneralDocumentoDet"].NewRow();
                rt["fechaHora"] = it.fecha.ToShortDateString() + ", " + it.hora;
                rt["documentoNro"] = it.documento;
                rt["cliente"] = it.ciRif+Environment.NewLine+it.razonSocial;
                rt["documentoNombre"] = it.documentoNombre;
                //rt["usuarioEstacion"] = it.usuarioCodigo.Trim() + "(" + it.usuarioNombre.Trim() + "), " + Environment.NewLine + it.CajaEstacion;
                rt["renglones"] = it.renglones.ToString("n0");
                rt["total"] = it.total * it.signo;
                rt["nombrePrd"] = it.nombreProducto;
                rt["cantidad"] = it.cantidadUnd.ToString("n2");
                rt["precio"] = it.precioUnd;
                rt["totalRenglon"] = it.totalRenglon * it.signo;
                rt["sucursal"] = it.sucNombre+Environment.NewLine+it.sucCodigo;
                rt["estacion"] = it.estacion;
                ds.Tables["GeneralDocumentoDet"].Rows.Add(rt);
            }

            var Rds = new List<ReportDataSource>();
            var pmt = new List<ReportParameter>();
            //pmt.Add(new ReportParameter("EMPRESA_RIF", Sistema.Negocio.CiRif));
            //pmt.Add(new ReportParameter("EMPRESA_NOMBRE", Sistema.Negocio.Nombre));
            //pmt.Add(new ReportParameter("EMPRESA_DIRECCION", Sistema.Negocio.DireccionFiscal));
            //pmt.Add(new ReportParameter("DOCUMENTO", ficha.documentoModo));
            Rds.Add(new ReportDataSource("GeneralDocumentoDet", ds.Tables["GeneralDocumentoDet"]));

            var frp = new ReporteFrm();
            frp.rds = Rds;
            frp.prmts = pmt;
            frp.Path = pt;
            frp.ShowDialog();
        }

    }

}