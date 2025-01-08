using Microsoft.Reporting.WinForms;
using ModVentaAdm.Helpers.Imprimir.Grafico;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.Reportes.Factura
{
    public class Gestion: IFactura
    {
        private string _idDoc;


        public Gestion()
        {
            _idDoc = "";
        }
        private bool _modoResumen=false;
        private bool _modoDocumento=false;
        public void Generar()
        {
            var msg = "Imprimir En Modo Resumen ?";
            _modoResumen= Helpers.Msg.ProcesarGuardar(msg);
            if (!_modoResumen) 
            {
                msg = "Imprimir En Modo Documento ?";
                _modoDocumento = Helpers.Msg.ProcesarGuardar(msg);
            }
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
                if (Sistema.Fabrica.ReglasNegocio.EditarEncabezadoAntesImprimirDocumentoVenta)
                {
                    EditarEncabezadoAntesImprimir(r01.Entidad);
                }
                if (_modoResumen)
                {
                    if (Sistema.Fabrica.ReglasNegocio.EditarItemsAntesImprimirDocumentoVenta)
                    {
                        if (EditarItemsAntesImprimir(r01.Entidad, 1))
                            generarDocModResumen(r01.Entidad);
                    }
                    else 
                    {
                        generarDocModResumen(r01.Entidad);
                    }
                }
                else if (_modoDocumento)
                    if (Sistema.Fabrica.ReglasNegocio.EditarItemsAntesImprimirDocumentoVenta)
                    {
                        if (EditarItemsAntesImprimir(r01.Entidad, 2))
                            generarDocModDocumento(r01.Entidad);
                    }
                    else
                    {
                        generarDocModDocumento(r01.Entidad);
                    }
                else
                    generarDoc(r01.Entidad);
            }
            catch (Exception e)
            {
                Helpers.Msg.Error(e.Message);
            }
        }

        private ReglasNegocio.EditarItemsAntesImprimir.Editar.IEditar hndEditarEncabezadoAntesImprimir;
        private void EditarEncabezadoAntesImprimir(OOB.Transporte.Documento.Entidad.Venta.Ficha ficha)
        {
            hndEditarEncabezadoAntesImprimir = new ReglasNegocio.EditarItemsAntesImprimir.Editar.HndEditar();
            if (hndEditarEncabezadoAntesImprimir != null)
            {
                hndEditarEncabezadoAntesImprimir.Inicializa();
                hndEditarEncabezadoAntesImprimir.setTitulo("EDITAR: ( Direcciòn Fiscal Cliente )");
                hndEditarEncabezadoAntesImprimir.setItemDescripcion(ficha.encabezado.clienteDirFiscal);
                hndEditarEncabezadoAntesImprimir.Inicia();
                if (hndEditarEncabezadoAntesImprimir.BtProcesar.OpcionIsOK) 
                {
                    ficha.encabezado.clienteDirFiscal = hndEditarEncabezadoAntesImprimir.GetDetalle;
                };
            }
        }

        private ReglasNegocio.EditarItemsAntesImprimir.IHnd hndEditarItemsAntesImprimir;
        private bool EditarItemsAntesImprimir(OOB.Transporte.Documento.Entidad.Venta.Ficha ficha,int modo)
        {
            hndEditarItemsAntesImprimir= Sistema.Fabrica.EditarItemsAntesImprimir();
            if (hndEditarItemsAntesImprimir != null) 
            {
                hndEditarItemsAntesImprimir.Inicializa();
                if (modo == 1) 
                {
                    hndEditarItemsAntesImprimir.setItems(ficha.detalles,1);
                }
                else if (modo == 2) 
                {
                    hndEditarItemsAntesImprimir.setItems(ficha.detDoc,2);
                    //hndEditarItemsAntesImprimir.setItems(ficha.detalles);
                }
                hndEditarItemsAntesImprimir.Inicia();
                return hndEditarItemsAntesImprimir.BtProcesar.OpcionIsOK;
            }
            return false;
        }

        private void generarDoc(OOB.Transporte.Documento.Entidad.Venta.Ficha ficha)
        {
            var clt = CultureInfo.CurrentCulture;
            var pt = AppDomain.CurrentDomain.BaseDirectory + @"\SrcTransporte\Reportes\Transp_Factura.rdlc";
            var ds = new DS_TRANSP();
            //
            DataRow re = ds.Tables["PresupuestoEnc"].NewRow();
            re["numeroDoc"] = ficha.encabezado.docNumero;
            re["fechaDoc"] = ficha.encabezado.docFechaEmision;
            re["cliente"] = ficha.encabezado.clienteCiRif + Environment.NewLine + ficha.encabezado.clienteNombre+ Environment.NewLine+ ficha.encabezado.clienteDirFiscal;
            re["solicitadoPor"] = ficha.encabezado.docSolicitadoPor;
            re["modulo"] = ficha.encabezado.docModulo;
            re["tasaDivisa"] = ficha.encabezado.factorCambio.ToString("n2", clt);
            re["condicionPago"] = ficha.encabezado.condPago + " ("+ficha.encabezado.diasCredito.ToString()+") Dia(s)";
            re["cirif"] = ficha.encabezado.clienteCiRif;
            re["nombreRazonSocial"] = ficha.encabezado.clienteNombre ;
            re["dirFiscal"] = ficha.encabezado.clienteDirFiscal;
            re["notasPeriodoLapso"] = ficha.encabezado.notasPeriodoLapso;
            ds.Tables["PresupuestoEnc"].Rows.Add(re);
            //
            DataRow rp = ds.Tables["PresupuestoPie"].NewRow();
            rp["subTotal"] = ficha.encabezado.subTotal ;
            rp["exento"] = ficha.encabezado.montoExento ;
            rp["iva"] = ficha.encabezado.montoImpuesto ;
            rp["total"] = ficha.encabezado.docTotal ;
            rp["igtfTasa"] = ficha.encabezado.igtfTasa;
            rp["igtfMono"] = ficha.encabezado.igtfMontoMonAct;
            rp["notas"] = ficha.encabezado.notasObs;
            rp["totalDiv"] = ficha.encabezado.montoDivisa;
            ds.Tables["PresupuestoPie"].Rows.Add(rp);
            //
            var i = 0;
            foreach (var it in ficha.detalles)
            {
                if (it.esItemBasico)
                //if (it.mostrarItemDocFinal.Trim().ToUpper() == "1")
                {
                    i++;
                    DataRow rt = ds.Tables["PresupItem"].NewRow();
                    rt["descripcion"] = "";
                    rt["detalle"] = it.detalle;
                    rt["cnt_dias"] = 0;
                    rt["cnt_und"] = 1;
                    rt["cnt"] = it.cntDias;
                    rt["item"] = i;
                    //rt["precio_unit"] = it.precioNetoMonDivisa;
                    //rt["importe"] = it.importeTotalMonDivisa;
                    rt["precio_unit"] = it.precioNetoMonLocal;
                    rt["importe"] = it.importeTotalMonLocal;
                    rt["desc_und"] = "";
                    ds.Tables["PresupItem"].Rows.Add(rt);
                }
                else 
                {
                    foreach (var dt in ficha.detTurnos.Where(w => w.idDocRef == it.idDocRef).ToList())
                    {
                        var _pne = dt.pnetoDiv * ficha.encabezado.factorCambio;
                        _pne = Math.Round(_pne, 2, MidpointRounding.AwayFromZero);
                        var _importe = dt.importe * ficha.encabezado.factorCambio;
                        _importe = Math.Round(_importe, 2, MidpointRounding.AwayFromZero);
                        //
                        i++;
                        DataRow rt = ds.Tables["PresupItem"].NewRow();
                        rt["descripcion"] = "";
                        rt["detalle"] = dt.turnDesc+"/"+dt.servDesc+Environment.NewLine+dt.notas;
                        rt["cnt_dias"] = 0;
                        rt["cnt_und"] = dt.cntVehic;
                        rt["cnt"] = dt.cntDias;
                        rt["item"] = i;
                        rt["precio_unit"] = _pne ;
                        rt["importe"] = _importe;
                        rt["desc_und"] = "";
                        ds.Tables["PresupItem"].Rows.Add(rt);
                    }
                }
            }
            //
            //foreach (var it in ficha.turnos)
            //{
            //    i++;
            //    DataRow rt = ds.Tables["PresupItem"].NewRow();
            //    rt["descripcion"] = "";
            //    rt["detalle"] = it.detalle + Environment.NewLine + it.ruta;
            //    rt["cnt_dias"] = 0;
            //    rt["cnt_und"] = 0;
            //    rt["cnt"] = 1;
            //    rt["item"] = i;
            //    rt["precio_unit"] = it.importe * ficha.encabezado.factorCambio;
            //    rt["importe"] = it.importe * ficha.encabezado.factorCambio;
            //    rt["desc_und"] = "";
            //    ds.Tables["PresupItem"].Rows.Add(rt);
            //}
            //
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
            //
            var frp = new ReporteFrm();
            frp.rds = Rds;
            frp.prmts = pmt;
            frp.Path = pt;
            frp.ShowDialog();
        }
        private void generarDocModResumen(OOB.Transporte.Documento.Entidad.Venta.Ficha ficha)
        {
            var clt = CultureInfo.CurrentCulture;
            var pt = AppDomain.CurrentDomain.BaseDirectory + @"\SrcTransporte\Reportes\Transp_Factura.rdlc";
            var ds = new DS_TRANSP();
            //
            DataRow re = ds.Tables["PresupuestoEnc"].NewRow();
            re["numeroDoc"] = ficha.encabezado.docNumero;
            re["fechaDoc"] = ficha.encabezado.docFechaEmision;
            re["cliente"] = ficha.encabezado.clienteCiRif + Environment.NewLine + ficha.encabezado.clienteNombre + Environment.NewLine + ficha.encabezado.clienteDirFiscal;
            re["solicitadoPor"] = ficha.encabezado.docSolicitadoPor;
            re["modulo"] = ficha.encabezado.docModulo;
            re["tasaDivisa"] = ficha.encabezado.factorCambio.ToString("n2", clt);
            re["condicionPago"] = ficha.encabezado.condPago + " (" + ficha.encabezado.diasCredito.ToString() + ") Dia(s)";
            re["cirif"] = ficha.encabezado.clienteCiRif;
            re["nombreRazonSocial"] = ficha.encabezado.clienteNombre;
            re["dirFiscal"] = ficha.encabezado.clienteDirFiscal;
            re["notasPeriodoLapso"] = ficha.encabezado.notasPeriodoLapso;
            ds.Tables["PresupuestoEnc"].Rows.Add(re);
            //
            DataRow rp = ds.Tables["PresupuestoPie"].NewRow();
            rp["subTotal"] = ficha.encabezado.subTotal;
            rp["exento"] = ficha.encabezado.montoExento;
            rp["iva"] = ficha.encabezado.montoImpuesto;
            rp["total"] = ficha.encabezado.docTotal;
            rp["igtfTasa"] = ficha.encabezado.igtfTasa;
            rp["igtfMono"] = ficha.encabezado.igtfMontoMonAct;
            rp["notas"] = ficha.encabezado.notasObs;
            rp["totalDiv"] = ficha.encabezado.montoDivisa;
            ds.Tables["PresupuestoPie"].Rows.Add(rp);
            //
            var i = 0;
            foreach (var it in ficha.detalles)
            {
                i++;
                DataRow rt = ds.Tables["PresupItem"].NewRow();
                rt["descripcion"] = "";
                rt["detalle"] = it.detalle;
                rt["cnt_dias"] = 0;
                rt["cnt_und"] = 1;
                rt["cnt"] = it.cntDias;
                rt["item"] = i;
                //rt["precio_unit"] = it.precioNetoMonDivisa;
                //rt["importe"] = it.importeTotalMonDivisa;
                rt["precio_unit"] = it.precioNetoMonLocal;
                rt["importe"] = it.importeTotalMonLocal;
                rt["desc_und"] = "";
                ds.Tables["PresupItem"].Rows.Add(rt);
            }
            //
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
            //
            var frp = new ReporteFrm();
            frp.rds = Rds;
            frp.prmts = pmt;
            frp.Path = pt;
            frp.ShowDialog();
        }
        private void generarDocModDocumento(OOB.Transporte.Documento.Entidad.Venta.Ficha ficha)
        {
            var clt = CultureInfo.CurrentCulture;
            var pt = AppDomain.CurrentDomain.BaseDirectory + @"\SrcTransporte\Reportes\Transp_Factura.rdlc";
            var ds = new DS_TRANSP();
            //
            DataRow re = ds.Tables["PresupuestoEnc"].NewRow();
            re["numeroDoc"] = ficha.encabezado.docNumero;
            re["fechaDoc"] = ficha.encabezado.docFechaEmision;
            re["cliente"] = ficha.encabezado.clienteCiRif + Environment.NewLine + ficha.encabezado.clienteNombre + Environment.NewLine + ficha.encabezado.clienteDirFiscal;
            re["solicitadoPor"] = ficha.encabezado.docSolicitadoPor;
            re["modulo"] = ficha.encabezado.docModulo;
            re["tasaDivisa"] = ficha.encabezado.factorCambio.ToString("n2", clt);
            re["condicionPago"] = ficha.encabezado.condPago + " (" + ficha.encabezado.diasCredito.ToString() + ") Dia(s)";
            re["cirif"] = ficha.encabezado.clienteCiRif;
            re["nombreRazonSocial"] = ficha.encabezado.clienteNombre;
            re["dirFiscal"] = ficha.encabezado.clienteDirFiscal;
            re["notasPeriodoLapso"] = ficha.encabezado.notasPeriodoLapso;
            ds.Tables["PresupuestoEnc"].Rows.Add(re);
            //
            DataRow rp = ds.Tables["PresupuestoPie"].NewRow();
            rp["subTotal"] = ficha.encabezado.subTotal;
            rp["exento"] = ficha.encabezado.montoExento;
            rp["iva"] = ficha.encabezado.montoImpuesto;
            rp["total"] = ficha.encabezado.docTotal;
            rp["igtfTasa"] = ficha.encabezado.igtfTasa;
            rp["igtfMono"] = ficha.encabezado.igtfMontoMonAct;
            rp["notas"] = ficha.encabezado.notasObs;
            rp["totalDiv"] = ficha.encabezado.montoDivisa;
            ds.Tables["PresupuestoPie"].Rows.Add(rp);
            //
            var i = 0;
            foreach (var it in ficha.detDoc)
            {
                var _precioNetoMonLocal = it.importe*ficha.encabezado.factorCambio;
                _precioNetoMonLocal= Math.Round(_precioNetoMonLocal,2, MidpointRounding.AwayFromZero);
                i++;
                DataRow rt = ds.Tables["PresupItem"].NewRow();
                rt["descripcion"] = "";
                rt["detalle"] = it.detalle;
                rt["cnt_dias"] = 0;
                rt["cnt_und"] = 1;
                rt["cnt"] = 1;
                rt["item"] = i;
                rt["precio_unit"] = _precioNetoMonLocal;
                rt["importe"] = _precioNetoMonLocal;
                rt["desc_und"] = "";
                ds.Tables["PresupItem"].Rows.Add(rt);
            }
            foreach (var it in ficha.detalles)
            {
                if (it.esItemBasico)
                {
                    i++;
                    DataRow rt = ds.Tables["PresupItem"].NewRow();
                    rt["descripcion"] = "";
                    rt["detalle"] = it.detalle;
                    rt["cnt_dias"] = 0;
                    rt["cnt_und"] = 1;
                    rt["cnt"] = it.cntDias;
                    rt["item"] = i;
                    //rt["precio_unit"] = it.precioNetoMonDivisa;
                    //rt["importe"] = it.importeTotalMonDivisa;
                    rt["precio_unit"] = it.precioNetoMonLocal;
                    rt["importe"] = it.importeTotalMonLocal;
                    rt["desc_und"] = "";
                    ds.Tables["PresupItem"].Rows.Add(rt);
                }
            }
            //
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
            //
            var frp = new ReporteFrm();
            frp.rds = Rds;
            frp.prmts = pmt;
            frp.Path = pt;
            frp.ShowDialog();
        }
    }
}