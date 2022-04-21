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


        public Reportes.Filtro.IFiltro Filtros { get { return _filtro; } }


        public Gestion()
        {
            _filtro = new Filtro();
        }


        public void Generar(Reportes.Filtro.data data)
        {
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
            var lst = new List<data>();
            var k1 = DateTime.Now.Date;//list[0].fechaDoc;
            var k2 = "";// list[0].codigoSucursalDoc;
            var k3 = "";// list[0].codigoDoc;
            var k4 = false;
            var k5 = "";
            data reg = null;
            foreach (var rg in list.OrderBy(o => o.fechaDoc).ThenBy(o => o.codigoSucursalDoc).ThenBy(o => o.codigoDoc).ThenBy(o=>o.estacion).ThenBy(o => o.numDoc).ToList())
            {
                if (rg.fechaDoc == k1 && rg.codigoSucursalDoc == k2 && rg.codigoDoc == k3 && k4==rg.isResumen && k5==rg.estacion)
                {
                    if (reg.isResumen == false)
                    {
                        reg = new data(rg);
                        lst.Add(reg);
                    }
                    else 
                    {
                        reg.Agregar(rg);
                    }
                }
                else 
                {
                    reg = new data(rg);
                    lst.Add(reg);
                    k1 = rg.fechaDoc;
                    k2 = rg.codigoSucursalDoc;
                    k3 = rg.codigoDoc;
                    k4 = rg.isResumen;
                    k5 = rg.estacion;
                }
            }

            var pt = AppDomain.CurrentDomain.BaseDirectory + @"Reportes\LibroVenta.rdlc";
            var ds = new DS();


            var item = 1;
            foreach (var it in lst.ToList())
            {
                DataRow rt = ds.Tables["LibroVenta"].NewRow();
                rt["item"] = item;
                rt["fecha"] = it.fechaDoc;
                rt["ciRif"] = it.ciRifDoc;
                rt["razonSocial"] = it.nombreRazonSocialDoc;
                rt["numFactura"] = it.desdeNumDoc+":"+it.hastaNumDoc;
                rt["total"] = it.montoTotal*it.signoDoc;
                rt["exento"] = it.montoTotal * it.signoDoc;
                rt["base1"] = it.montoBase1 * it.signoDoc;
                rt["tasa1"] = it.tasaIva1 ;
                rt["iva1"] = it.montoImpuesto1 * it.signoDoc;
                rt["base2"] = it.montoBase2 * it.signoDoc;
                rt["tasa2"] = it.tasaIva2;
                rt["iva2"] = it.montoImpuesto2 * it.signoDoc;
                rt["comprobanteRetencion"] = it.comprobanteRetencionIva;
                rt["montoRetencion"] = it.montoRetencionIva * it.signoDoc;
                rt["notaDebito"] = it.notaDebito;
                rt["notaCredito"] = it.notaCredito;
                rt["facturaAfecta"] = it.numAplicaDoc;
                rt["tipoTrans"] = it.trans.ToString()+"-reg";
                ds.Tables["LibroVenta"].Rows.Add(rt);
                item++;
            }

            var Rds = new List<ReportDataSource>();
            var pmt = new List<ReportParameter>();
            //pmt.Add(new ReportParameter("EMPRESA_RIF", Sistema.Negocio.CiRif));
            //pmt.Add(new ReportParameter("EMPRESA_NOMBRE", Sistema.Negocio.Nombre));
            //pmt.Add(new ReportParameter("EMPRESA_DIRECCION", Sistema.Negocio.DireccionFiscal));
            //pmt.Add(new ReportParameter("DOCUMENTO", ficha.documentoModo));
            Rds.Add(new ReportDataSource("LibroVenta", ds.Tables["LibroVenta"]));

            var frp = new ReporteFrm();
            frp.rds = Rds;
            frp.prmts = pmt;
            frp.Path = pt;
            frp.ShowDialog();
        }

    }

}