using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.Reportes.Modo.LibroVenta
{

    public class Gestion : IGestion
    {

        private Reportes.Filtro.IFiltro _filtro;
        private string _primerDia;
        private string _ultimoDia;


        public Reportes.Filtro.IFiltro Filtros { get { return _filtro; } }


        public Gestion()
        {
            _filtro = new Filtro();
        }


        public void Generar(Reportes.Filtro.data data)
        {
            var pd = new DateTime(data.GetAnoRelacion, data.GetMesRelacion, 1);
            _primerDia = "Desde: "+(pd.ToShortDateString());
            _ultimoDia = "Hasta: " + (pd.AddMonths(1).AddDays(-1).ToShortDateString());

            var filtro = new OOB.Reportes.LibroVenta.Filtro()
            {
                mesRelacion = data.GetMesRelacion.ToString().Trim().PadLeft(2, '0'),
                anoRelacion = data.GetAnoRelacion.ToString().Trim().PadLeft(4, '0'),
            };
            var r01 = Sistema.MyData.ReportesAdm_LibroVenta(filtro);
            if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Helpers.Msg.Error(r01.Mensaje);
                return;
            }
            Imprimir(r01.ListaD);
        }

        private void Imprimir(List<OOB.Reportes.LibroVenta.Ficha> list)
        {
            //var lst = new List<data>();
            //var k1 = DateTime.Now.Date;//list[0].fechaDoc;
            //var k2 = "";// list[0].codigoSucursalDoc;
            //var k3 = "";// list[0].codigoDoc;
            //var k4 = false;
            //var k5 = "";
            //data reg = null;
            //foreach (var rg in list.OrderBy(o => o.fechaDoc).ThenBy(o => o.codigoSucursalDoc).ThenBy(o => o.codigoDoc).ThenBy(o=>o.estacion).ThenBy(o => o.numDoc).ToList())
            //{
            //    if (rg.fechaDoc == k1 && rg.codigoSucursalDoc == k2 && rg.codigoDoc == k3 && k4==rg.isResumen && k5==rg.estacion)
            //    {
            //        if (reg.isResumen == false)
            //        {
            //            reg = new data(rg);
            //            lst.Add(reg);
            //        }
            //        else 
            //        {
            //            reg.Agregar(rg);
            //        }
            //    }
            //    else 
            //    {
            //        reg = new data(rg);
            //        lst.Add(reg);
            //        k1 = rg.fechaDoc;
            //        k2 = rg.codigoSucursalDoc;
            //        k3 = rg.codigoDoc;
            //        k4 = rg.isResumen;
            //        k5 = rg.estacion;
            //    }
            //}

            var pt = AppDomain.CurrentDomain.BaseDirectory + @"Reportes\LibroVenta.rdlc";
            var ds = new DS();

            DataRow rt2 = ds.Tables["LibroVentaEnc"].NewRow();
            rt2["periodo_desde"] = _primerDia;
            rt2["periodo_hasta"] = _ultimoDia;
            rt2["tipo_contribuyente"] = "ESPECIAL";
            rt2["titulo"] = "Libro de Ventas " + _primerDia + ", " + _ultimoDia;
            rt2["nombreRazonSocial"] = Sistema.DatosEmpresa.Nombre.Trim();
            rt2["ciRif"] = Sistema.DatosEmpresa.CiRif.Trim();
            rt2["dirFiscal"] = Sistema.DatosEmpresa.Direccion.Trim();
            rt2["descTasa1"] = "Tasa General  " + 16.ToString("n2");
            rt2["descTasa2"] = "Tasa Reducida " + 8.ToString("n2");
            rt2["descTasa3"] = "Tasa Gen+Adic " + 31.ToString("n2");
            ds.Tables["LibroVentaEnc"].Rows.Add(rt2);

            var item = 1;
            var _factura = "";
            var _ncr = "";
            var _ndb = "";
            foreach (var it in list.OrderBy(o=>o.fechaDoc).ThenBy(o=>o.numDoc).ToList())
            {
                switch (it.codigoDoc.Trim().ToUpper())
                { 
                    case "01":
                        _factura = it.numDoc;
                        _ncr = "";
                        _ndb = "";
                        break;
                    case "02":
                        _factura = "";
                        _ncr = "";
                        _ndb = it.numDoc;
                        break;
                    case "03":
                        _factura = "";
                        _ncr = it.numDoc;
                        _ndb = "";
                        break;
                }
                DataRow rt = ds.Tables["LibroVenta"].NewRow();
                rt["item"] = item;
                rt["fecha"] = it.fechaDoc;
                rt["ciRif"] = it.ciRifDoc;
                rt["razonSocial"] = it.nombreRazonSocialDoc;
                rt["numFactura"] = _factura;

                rt["total"] = it.montoTotal * it.signoDoc;
                rt["exento"] = it.montoTotal * it.signoDoc;
                rt["base1"] = it.montoBase1 * it.signoDoc;
                rt["tasa1"] = it.tasaIva1;
                rt["iva1"] = it.montoImpuesto1 * it.signoDoc;
                rt["base2"] = it.montoBase2 * it.signoDoc;
                rt["tasa2"] = it.tasaIva2;
                rt["iva2"] = it.montoImpuesto2 * it.signoDoc;
                if (it.estatus == "1")
                {
                    rt["total"] = 0m;
                    rt["exento"] = 0m;
                    rt["base1"] = 0m;
                    rt["tasa1"] = 0m; 
                    rt["iva1"] = 0m;
                    rt["base2"] = 0m;
                    rt["tasa2"] = 0m; 
                    rt["iva2"] = 0m;
                }
                rt["comprobanteRetencion"] = it.comprobanteRetencionIva;
                rt["montoRetencion"] = it.montoRetencionIva * it.signoDoc;
                rt["notaDebito"] = _ndb;
                rt["notaCredito"] = _ncr;
                rt["facturaAfecta"] = it.numAplicaDoc;
                rt["tipoTrans"] = "1-reg";
                ds.Tables["LibroVenta"].Rows.Add(rt);
                item++;
            }

            var Rds = new List<ReportDataSource>();
            var pmt = new List<ReportParameter>();
            //pmt.Add(new ReportParameter("EMPRESA_RIF", Sistema.Negocio.CiRif));
            //pmt.Add(new ReportParameter("EMPRESA_NOMBRE", Sistema.Negocio.Nombre));
            //pmt.Add(new ReportParameter("EMPRESA_DIRECCION", Sistema.Negocio.DireccionFiscal));
            //pmt.Add(new ReportParameter("DOCUMENTO", ficha.documentoModo));
            Rds.Add(new ReportDataSource("LibroVentaEnc", ds.Tables["LibroVentaEnc"]));
            Rds.Add(new ReportDataSource("LibroVenta", ds.Tables["LibroVenta"]));

            var frp = new ReporteFrm();
            frp.rds = Rds;
            frp.prmts = pmt;
            frp.Path = pt;
            frp.ShowDialog();
        }
    }
}