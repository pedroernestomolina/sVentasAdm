using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.CxC.Tools.AgregarCta
{
    
    public class Agregar: IAgregar
    {


        private bool _procesarIsOk;
        private bool _abandonarIsOk;
        private dataAgregar _data;
        private Gestion.HndCombo.IOpcion _gVend;
        private Gestion.HndCombo.IOpcion _gTipoDoc;
        private Documentos.Generar.BuscarCliente.IBuscar _gCliente;


        public bool AbandonarIsOK { get { return _abandonarIsOk; } }
        public bool ProcesarIsOK { get { return _procesarIsOk; } }
        public bool AgregarIsOk { get { return _procesarIsOk; } }


        public string SerieGet { get { return _data.SerieDocGet; } }
        public DateTime FechaVencDocGet { get { return _data.FechaVencimientoDocGet; } }
        public string NumeroDocGet { get { return _data.NumeroDocGet; } }
        public DateTime FechaEmisionDocGet { get { return _data.FechaEmisionDocGet; } }
        public int DiasCreditoDocGet { get { return _data.DiasCreditoDocGet; } }
        public decimal MontoDocGet { get { return _data.MontDivisaDocGet; } }
        public string NotasDocGet { get { return _data.NotasDocGet; } }
        public decimal TasaFactorDocGet { get { return _data.TasaFactorDocGet; } }


        public Agregar() 
        {
            _procesarIsOk = false;
            _abandonarIsOk = false;
            _data = new dataAgregar();
            _gVend = new Gestion.HndCombo.Opcion();
            _gTipoDoc = new Gestion.HndCombo.Opcion();
            _gCliente = new Documentos.Generar.BuscarCliente.Gestion();
        }


        AgregarCtaFrm frm;
        public void Inicializa()
        {
            _procesarIsOk = false;
            _abandonarIsOk = false;
            _data.Inicializa();
            _gVend.Inicializa();
            _gTipoDoc.Inicializa();
            _gCliente.Inicializa();
        }
        public void Inicia()
        {
            if (CargarData()) 
            {
                if (frm == null) 
                {
                    frm = new AgregarCtaFrm();
                    frm.setControlador(this);
                }
                frm.ShowDialog();
            }
        }


        public void AbandonarFicha()
        {
            _abandonarIsOk = false;
            var rt = MessageBox.Show("Abandona Ficha ?", "*** ALERTA ***", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (rt == DialogResult.Yes)
            {
                _abandonarIsOk = true;
            }
        }
        public void Procesar()
        {
            _procesarIsOk = false;
            if (_data.IsOk())
            {
                var r00 = Sistema.MyData.Sistema_TipoDocumento_GetFichaById(_data.TipoDocGet.id);
                if (r00.Result== OOB.Resultado.Enumerados.EnumResult.isError)
                {
                    Helpers.Msg.Error(r00.Mensaje);
                    return ;
                }
                var _tipoDoc=r00.Entidad;

                var rt = MessageBox.Show("Procesar/Guardar Ficha ?", "*** ALERTA ***", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (rt == DialogResult.Yes)
                {
                    var fichaOOb = new OOB.CxC.AgregarCta.Ficha()
                    {
                        autoCliente = _data.ClienteGet.id,
                        autoVendedor = _data.VendedorGet.id,
                        ciRifCliente = _data.ClienteGet.ciRif,
                        codigoCliente = _data.ClienteGet.codigo,
                        codSucursal = Sistema.Sucursal.codigo,
                        diasCreditoDoc = _data.DiasCreditoDocGet,
                        fechaEmisionDoc = _data.FechaEmisionDocGet,
                        fechaVencDoc = _data.FechaVencimientoDocGet,
                        montoDivisaDoc = _data.MontDivisaDocGet,
                        montoDoc = _data.MontoDoc,
                        nombreCliente = _data.ClienteGet.razonSocial,
                        notasDoc = _data.NotasDocGet,
                        numeroDoc = _data.NumeroDocGet,
                        serieDoc = _data.SerieDocGet,
                        signoDoc = _tipoDoc.signo,
                        tasaCambioDoc = _data.TasaFactorDocGet,
                        tipoDoc = _tipoDoc.siglas,
                    };
                    var r01 = Sistema.MyData.CxC_Agregar(fichaOOb);
                    if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError) 
                    {
                        Helpers.Msg.Error(r01.Mensaje);
                        return;
                    }
                    _procesarIsOk = true;
                }
            }
        }


        public BindingSource VendGetSource { get { return _gVend.Source; } }
        public string VendGetId { get { return _gVend.GetId; } }
        public void setVend(string id)
        {
            _gVend.setFicha(id);
            _data.setVend(_gVend.Item);

        }

        public BindingSource TipoDocGetSource { get { return _gTipoDoc.Source; } }
        public string TipoDocGetId { get { return _gTipoDoc.GetId; } }
        public void setTipoDoc(string id)
        {
            _gTipoDoc.setFicha(id);
            _data.setTipoDoc(_gTipoDoc.Item);
        }
     

        public void setSerieDoc(string p)
        {
            _data.setSerieDoc(p);
        }
        public void setNumDoc(string p)
        {
            _data.setNumDoc(p);
        }
        public void setFechaEmisionDoc(DateTime fecha)
        {
            _data.setFechaEmisionDoc(fecha);
        }
        public void setDiasCreditoDoc(int cnt)
        {
            _data.setDiasCreditoDoc(cnt);
        }
        public void setMontoDoc(decimal monto)
        {
            _data.setMontoDoc(monto);
        }
        public void setFactor(decimal tasa)
        {
            _data.setFactor(tasa);
        }
        public void setNotas(string p)
        {
            _data.setNotas(p);
        }


        private bool _clienteSeleccionadoIsOk;
        public bool ClienteSeleccionadoIsOk { get { return _clienteSeleccionadoIsOk; } }
        public string ClienteDataGet { get { return _data.ClienteDataGet; } }
        public void BuscarCliente()
        {
            _clienteSeleccionadoIsOk = false;
            _gCliente.Inicializa();
            _gCliente.setActivarSeleccionItem(true);
            _gCliente.Inicia();
            if (_gCliente.ItemSeleccionadoIsOk) 
            {
                string idCliente = _gCliente.IdItemSeleccionado;
                var r01 = Sistema.MyData.Cliente_GetFicha(idCliente);
                if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError) 
                {
                    Helpers.Msg.Error(r01.Mensaje);
                    return;
                }
                _clienteSeleccionadoIsOk = true;
                _data.setCliente(r01.Entidad);
                setVend(r01.Entidad.idVendedor);
                setDiasCreditoDoc(r01.Entidad.diasCredito);
            }
        }


        private bool CargarData()
        {
            var rt = true;

            var r01 = Sistema.MyData.Sistema_Vendedor_GetLista();
            if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Helpers.Msg.Error(r01.Mensaje);
                return false;
            }
            var r02 = Sistema.MyData.Configuracion_FactorDivisa();
            if (r02.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Helpers.Msg.Error(r02.Mensaje);
                return false;
            }
            _data.setFactor(r02.Entidad);

            var lst = new List<Gestion.ficha>();
            foreach (var rg in r01.ListaD.OrderBy(o => o.nombre).ToList())
            {
                var nr = new Gestion.ficha()
                {
                    codigo = rg.codigo,
                    desc = rg.nombre,
                    id = rg.id,
                };
                lst.Add(nr);
            }
            _gVend.setData(lst);

            var lst_2 = new List<Gestion.ficha>();
            lst_2.Add(new Gestion.ficha() { codigo = "01", desc = "VENTA", id = "0000000001", });
            lst_2.Add(new Gestion.ficha() { codigo = "02", desc = "NOTA DEBITO", id = "0000000002", });
            lst_2.Add(new Gestion.ficha() { codigo = "03", desc = "NOTA CREDITO", id = "0000000003", });
            lst_2.Add(new Gestion.ficha() { codigo = "04", desc = "NOTA ENTREGA", id = "0000000004", });
            _gTipoDoc.setData(lst_2);

            return rt;
        }

    }

}