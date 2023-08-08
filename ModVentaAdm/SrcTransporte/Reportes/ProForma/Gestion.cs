﻿using Microsoft.Reporting.WinForms;
using ModVentaAdm.Helpers.Imprimir.Grafico;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.Reportes.ProForma
{
    public class Gestion: IProForma
    {
        private string _idDoc;


        public Gestion()
        {
            _idDoc = "";
        }
        public void Generar()
        {
            cargarDoc();
        }
        public void setIdDocVisualizar(string idDoc)
        {
            _idDoc = idDoc;
        }

        private void cargarDoc()
        {
            try
            {
                var r01 = Sistema.MyData.TransporteDocumento_EntidadVenta_GetById(_idDoc);
                generarDoc(r01.Entidad);
            }
            catch (Exception e)
            {
                Helpers.Msg.Error(e.Message);
            }
        }

        private void generarDoc(OOB.Transporte.Documento.Entidad.Venta.Ficha ficha)
        {
            var clt = CultureInfo.CurrentCulture;
            var pt = AppDomain.CurrentDomain.BaseDirectory + @"\SrcTransporte\Reportes\Transp_ProForma.rdlc";
            var ds = new DS_TRANSP();

            DataRow re = ds.Tables["PresupuestoEnc"].NewRow();
            re["numeroDoc"] = ficha.encabezado.docNumero;
            re["fechaDoc"] = ficha.encabezado.docFechaEmision;
            re["cliente"] = ficha.encabezado.clienteCiRif + Environment.NewLine + ficha.encabezado.clienteNombre+ Environment.NewLine+ ficha.encabezado.clienteDirFiscal;
            re["solicitadoPor"] = ficha.encabezado.docSolicitadoPor;
            re["modulo"] = ficha.encabezado.docModulo;
            re["tasaDivisa"] = ficha.encabezado.factorCambio.ToString("n2", clt);
            re["condicionPago"] = ficha.encabezado.condPago + " ("+ficha.encabezado.diasCredito.ToString()+") Dia(s)";

            ds.Tables["PresupuestoEnc"].Rows.Add(re);

            DataRow rp = ds.Tables["PresupuestoPie"].NewRow();
            rp["total"] = ficha.encabezado.montoDivisa.ToString("n2", clt) + "$";
            rp["notas"] = ficha.encabezado.notasObs;
            ds.Tables["PresupuestoPie"].Rows.Add(rp);

            var i = 0;
            foreach (var it in ficha.detalles)
            {
                i++;
                DataRow rt = ds.Tables["PresupItem"].NewRow();
                rt["descripcion"] = "";
                rt["detalle"] = it.detalle;
                rt["cnt_dias"] = 0;
                rt["cnt_und"] = 0;
                rt["cnt"] = it.cntDias;
                rt["item"] = i;
                rt["precio_unit"] = it.precioNetoMonDivisa;
                rt["importe"] = it.importeTotalMonDivisa;
                rt["desc_und"] = "";
                ds.Tables["PresupItem"].Rows.Add(rt);
            }

            var Rds = new List<ReportDataSource>();
            var pmt = new List<ReportParameter>();
            //pmt.Add(new ReportParameter("EMPRESA_RIF", Sistema.Negocio.CiRif));
            //pmt.Add(new ReportParameter("EMPRESA_NOMBRE", Sistema.Negocio.Nombre));
            //pmt.Add(new ReportParameter("EMPRESA_DIRECCION", Sistema.Negocio.DireccionFiscal));
            //pmt.Add(new ReportParameter("DOCUMENTO", ficha.documentoModo));
            //pmt.Add(new ReportParameter("FILTRO", filt));
            Rds.Add(new ReportDataSource("PresupuestoEnc", ds.Tables["PresupuestoEnc"]));
            Rds.Add(new ReportDataSource("PresupItem", ds.Tables["PresupItem"]));
            Rds.Add(new ReportDataSource("PresupuestoPie", ds.Tables["PresupuestoPie"]));

            var frp = new ReporteFrm();
            frp.rds = Rds;
            frp.prmts = pmt;
            frp.Path = pt;
            frp.ShowDialog();
        }
    }
}