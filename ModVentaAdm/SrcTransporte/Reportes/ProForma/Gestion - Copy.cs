//using Microsoft.Reporting.WinForms;
//using ModVentaAdm.Helpers.Imprimir.Grafico;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Globalization;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;


//namespace ModVentaAdm.SrcTransporte.Reportes.ProForma
//{
//    public class Gestion: IProForma
//    {
//        private string _idDoc;
//        private List<OOB.Transporte.Documento.Entidad.Venta.DetTurno> detalleTurnos;


//        public Gestion()
//        {
//            _idDoc = "";
//        }
//        public void Generar()
//        {
//            cargarDoc();
//        }
//        public void setIdDocVisualizar(string idDoc)
//        {
//            _idDoc = idDoc;
//        }

//        private void cargarDoc()
//        {
//            try
//            {
//                var r01 = Sistema.MyData.TransporteDocumento_EntidadVenta_GetById(_idDoc);
//                detalleTurnos = r01.Entidad.detTurnos;
//                generarDoc(r01.Entidad);
//            }
//            catch (Exception e)
//            {
//                Helpers.Msg.Error(e.Message);
//            }
//        }

//        private void generarDoc(OOB.Transporte.Documento.Entidad.Venta.Ficha ficha)
//        {
//            var clt = CultureInfo.CurrentCulture;
//            var pt = AppDomain.CurrentDomain.BaseDirectory + @"\SrcTransporte\Reportes\Transp_ProForma.rdlc";
//            var ds = new DS_TRANSP();
//            //
//            DataRow re = ds.Tables["PresupuestoEnc"].NewRow();
//            re["numeroDoc"] = ficha.encabezado.docNumero;
//            re["fechaDoc"] = ficha.encabezado.docFechaEmision;
//            re["cliente"] = ficha.encabezado.clienteCiRif + Environment.NewLine + ficha.encabezado.clienteNombre+ Environment.NewLine+ ficha.encabezado.clienteDirFiscal;
//            re["solicitadoPor"] = ficha.encabezado.docSolicitadoPor;
//            re["modulo"] = ficha.encabezado.docModulo;
//            re["tasaDivisa"] = ficha.encabezado.factorCambio.ToString("n2", clt);
//            re["condicionPago"] = ficha.encabezado.condPago + " ("+ficha.encabezado.diasCredito.ToString()+") Dia(s)";
//            re["notasPeriodoLapso"] = ficha.encabezado.notasPeriodoLapso;
//            ds.Tables["PresupuestoEnc"].Rows.Add(re);
//            //
//            DataRow rp = ds.Tables["PresupuestoPie"].NewRow();
//            rp["sTotal"] = ficha.encabezado.montoDivisa.ToString("n2", clt) + "$";
//            rp["notas"] = ficha.encabezado.notasObs;
//            ds.Tables["PresupuestoPie"].Rows.Add(rp);
//            //
//            var i = 0;
//            foreach (var it in ficha.detalles)
//            {
//                if (it.mostrarItemDocFinal.Trim().ToUpper() == "1")
//                {
//                    i++;
//                    DataRow rt = ds.Tables["PresupItem"].NewRow();
//                    rt["descripcion"] = "";
//                    rt["detalle"] = it.detalle;
//                    rt["cnt_dias"] = 0;
//                    rt["cnt_und"] = 0;
//                    rt["cnt"] = it.cntDias;
//                    rt["item"] = i;
//                    rt["precio_unit"] = it.precioNetoMonDivisa;
//                    rt["importe"] = it.importeTotalMonDivisa;
//                    rt["desc_und"] = "";
//                    rt["turnoDesc"] = "";
//                    rt["turnoCntDias"] = 0;
//                    ds.Tables["PresupItem"].Rows.Add(rt);
//                }
//                else 
//                {
//                    foreach (var dt in ficha.detTurnos.Where(w => w.idVenta == it.idDocRef).ToList())
//                    {
//                        var _procedencia = "";
//                        switch (dt.docTipoProcedencia.Trim()) 
//                        {
//                            case "P":
//                                _procedencia += "PRESUPUESTO Nro: ";
//                                break;
//                            case "H":
//                                _procedencia += "HOJA DE SERVICIO Nro: ";
//                                break;
//                        }
//                        _procedencia += dt.docNroRef;
//                        i++;
//                        DataRow rt = ds.Tables["PresupItem"].NewRow();
//                        rt["descripcion"] = "";
//                        rt["detalle"] =_procedencia+", Turno: "+dt.turnDesc.Trim()+", Cnt/Dias: "+dt.turnCntDias.ToString()+", Cnt/Vehic: "+dt.cntVehic.ToString()+
//                                          ", Ruta: "+dt.notas;
//                        rt["cnt_dias"] = 0;
//                        rt["cnt_und"] = 0;
//                        rt["cnt"] = dt.cntVehic*dt.turnCntDias;
//                        rt["item"] = i;
//                        rt["precio_unit"] = dt.pnetoDiv ;
//                        rt["importe"] = dt.importe;
//                        rt["desc_und"] = "";
//                        rt["turnoDesc"] = dt.turnDesc.Trim();
//                        rt["turnoCntDias"] = 0;
//                        ds.Tables["PresupItem"].Rows.Add(rt);
//                    }
//                }
//            }
//            //
//            //foreach (var it in ficha.turnos)
//            //{
//            //    i++;
//            //    DataRow rt = ds.Tables["PresupItem"].NewRow();
//            //    rt["descripcion"] = "";
//            //    rt["detalle"] = it.detalle+Environment.NewLine+it.ruta;
//            //    rt["cnt_dias"] = 0;
//            //    rt["cnt_und"] = 0;
//            //    rt["cnt"] = 1;
//            //    rt["item"] = i;
//            //    rt["precio_unit"] = it.importe;
//            //    rt["importe"] = it.importe;
//            //    rt["desc_und"] = "";
//            //    ds.Tables["PresupItem"].Rows.Add(rt);
//            //}
//            //
//            LocalReport localReport = new LocalReport();
//            localReport.ReportPath = AppDomain.CurrentDomain.BaseDirectory + @"\SrcTransporte\Reportes\Transp_ProForma.rdlc";
//            localReport.DataSources.Add(new ReportDataSource("PresupuestoEnc", ds.Tables["PresupuestoEnc"]));
//            localReport.DataSources.Add(new ReportDataSource("PresupItem", ds.Tables["PresupItem"]));
//            localReport.DataSources.Add(new ReportDataSource("PresupuestoPie", ds.Tables["PresupuestoPie"]));
//            var Rds = new List<ReportDataSource>();
//            var pmt = new List<ReportParameter>();
//            //pmt.Add(new ReportParameter("EMPRESA_RIF", Sistema.Negocio.CiRif));
//            //pmt.Add(new ReportParameter("EMPRESA_NOMBRE", Sistema.Negocio.Nombre));
//            //pmt.Add(new ReportParameter("EMPRESA_DIRECCION", Sistema.Negocio.DireccionFiscal));
//            //pmt.Add(new ReportParameter("DOCUMENTO", ficha.documentoModo));
//            //pmt.Add(new ReportParameter("FILTRO", filt));
//            var frp = new ReporteFrm();
//            //frp.rds = rds;
//            frp.rds = localReport.DataSources;
//            frp.prmts = pmt;
//            frp.Path = pt;
//            frp.ShowDialog();
//        }
//    }
//}