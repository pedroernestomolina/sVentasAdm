using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.CxC.Tools.GestionPago.MediosCobro
{
    
    public class MedCobro : IMedCobro
    {


        private List<data> _lst;
        private BindingList<data> _bl;
        private BindingSource _bs;
        private bool _procesarIsOk;
        private bool _abandonarIsOk;
        private decimal _montoCobrar;
        private bool _generarNotaCredito;
        private MetodoCobro.Agregar.IMetAgregar _gMetCobroAgregar;
        private MetodoCobro.Editar.IMetEditar _gMetCobroEditar;


        public BindingSource Source { get { return _bs; } }
        public decimal GetMontoCobrar { get { return _montoCobrar; } }
        public decimal GetMontoRecibido { get { return _bl.Sum(s => s.Importe); } }
        public decimal GetMontoPend { get { return _montoCobrar-GetMontoRecibido; } }
        public string GetRestaCambio { get { return GetMontoPend > 0 ? "Resta:" : "Cambio:"; } }
        public bool AbandonarIsOK { get { return _abandonarIsOk; } }
        public bool ProcesarIsOK { get { return _procesarIsOk; } }
        public bool MedioCobroIsOk { get { return _procesarIsOk; } }
        public decimal GetImporteMonedaLocal { get { return _bl.Sum(s => s.item.ImporteMonedaLocal); } }
        public IEnumerable<data> GetListaMedCobro { get { return _bl.ToList(); } }


        public MedCobro() 
        {
            _lst = new List<data>();
            _bl = new BindingList<data>(_lst);
            _bs = new BindingSource();
            _bs.DataSource = _bl;
            _montoCobrar = 0m;
            _abandonarIsOk = false;
            _procesarIsOk = false;
            _generarNotaCredito = false;
            _gMetCobroAgregar= new MetodoCobro.Agregar.MetAgregar();
            _gMetCobroEditar = new MetodoCobro.Editar.MetEditar();
        }


        public void Inicializa()
        {
            limpiar();
        }

        private void limpiar()
        {
            _bl.Clear();
            _montoCobrar = 0m;
            _abandonarIsOk = false;
            _procesarIsOk = false;
            _generarNotaCredito=false;
        }

        public void setMontoCobrar(decimal monto)
        {
            _montoCobrar = monto;
        }

        MediosCobroFrm frm;
        public void Inicia()
        {
            if (CargarData()) 
            {
                if (frm == null) 
                {
                    frm = new MediosCobroFrm();
                    frm.setControlador(this);
                }
                frm.ShowDialog();
            }
        }


        public bool AgregarFichaIsOk { get { return _gMetCobroAgregar.AgregarIsOk; } }
        public void AgregarFicha()
        {
            _gMetCobroAgregar.Inicializa();
            _gMetCobroAgregar.setMontoResta(GetMontoPend);
            _gMetCobroAgregar.Inicia();
            if (_gMetCobroAgregar.AgregarIsOk) 
            {
                AgregarItem(_gMetCobroAgregar.ItemAgregarEditar);
            }
        }

        private bool _eliminarMetodoPago;
        public bool EliminarMetodoPagoIsOk { get { return _eliminarMetodoPago; } }
        public void EliminarMetodoPago()
        {
            _eliminarMetodoPago = false;
            if (_bs.Current != null)
            {
                var it = (data)_bs.Current;
                var xmsg = "Eliminar Metodo De Pago ?";
                var msg = MessageBox.Show(xmsg, "*** ALERTA ***", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (msg == DialogResult.Yes) 
                {
                    _bl.Remove(it);
                    _bs.CurrencyManager.Refresh();
                    _eliminarMetodoPago = true;
                }
            }
        }

        public bool EditarMetodoPagoIsOk { get { return _gMetCobroEditar.EditarIsOk; } }
        public void EditarMetodoPago()
        {
            if (_bs.Current != null)
            {
                var it = (data)_bs.Current;
                _gMetCobroEditar.Inicializa();
                _gMetCobroEditar.setMontoResta(GetMontoPend);
                _gMetCobroEditar.setItemEditar(it.item);
                _gMetCobroEditar.Inicia();
                if (_gMetCobroEditar.EditarIsOk)
                {
                    _bs.Remove(it);
                    AgregarItem(_gMetCobroEditar.ItemAgregarEditar);
                }
            }
        }


        public void setGenerarNotaCredito(bool p)
        {
            _generarNotaCredito = p;
        }


        public void Procesar()
        {
            _procesarIsOk = false;
            if (GetMontoCobrar > GetMontoRecibido)
            {
                Helpers.Msg.Error("MONTO RECIBIDO INFERIOR AL MONTO A COBRAR");
                return;
            }
            if (GetMontoRecibido > GetMontoCobrar)
            {
                if (!_generarNotaCredito)
                {
                    Helpers.Msg.Alerta("HABILITAR POR FAVOR CASILLA [ GENERAR NOTA DE CREDITO A FAVOR DEL CLIENTE ]");
                    return;
                }
            }
            else 
            {
                if (_generarNotaCredito)
                {
                    Helpers.Msg.Alerta("POR FAVOR DESHABILITAR CASILLA [ GENERAR NOTA DE CREDITO A FAVOR DEL CLIENTE ]");
                    return;
                }
            }
            var msg = "Procesar y Guardar Los Cambios ?";
            var r = MessageBox.Show(msg, "*** ALERTA ***", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (r == DialogResult.Yes)
            {
                _procesarIsOk = true;
            }
        }
        public void AbandonarFicha()
        {
            _abandonarIsOk = false;
            var msg = "Abandonar y Perder Los Cambios ?";
            var r = MessageBox.Show(msg, "*** ALERTA ***", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (r == DialogResult.Yes)
            {
                _abandonarIsOk = true;
            }
        }


        public string GetMetodoPagoOp { get { return _bs.Current != null ? ((data)_bs.Current).item.GetMetodo.desc : ""; } }
        public decimal GetMontoOp { get { return _bs.Current != null ? ((data)_bs.Current).item.GetMonto: 0m; } }
        public DateTime GetFechaOp { get { return _bs.Current != null ? ((data)_bs.Current).item.GetFechaOp: DateTime.Now.Date; } }
        public string GetDetalleOp { get { return _bs.Current != null ? ((data)_bs.Current).item.GetDetalleOp: ""; } }
        public string GetNroCtaOp { get { return _bs.Current != null ? ((data)_bs.Current).item.GetNroCta: ""; } }
        public string GetRefOp { get { return _bs.Current != null ? ((data)_bs.Current).item.GetCheqRefTranf: ""; } }
        public string GetBancoOp { get { return _bs.Current != null ? ((data)_bs.Current).item.GetBanco : ""; } }
        public string GetAplicaFactorOp
        {
            get
            {
                var rt = "";
                if (_bs.Current != null)
                {
                    var it= (data)_bs.Current;
                    if (it.item.GetAplicaFactor)
                    {
                        rt += "Si Aplica, "+Environment.NewLine+"Tasa: " + it.item.GetFactorCambio.ToString("n2");
                    }
                    else 
                    {
                        rt += "No Aplica,"+Environment.NewLine+"Monto en ($)";
                    }
                }
                return rt;
            }
        }


        private bool CargarData()
        {
            return true;
        }
        private void AgregarItem(MetodoCobro.dataItem item)
        {
            var id = 0;
            if (_bl.Count == 0)
            {
                id = 1;
            }
            else
            {
                id = _bl.Max(m => id) + 1;
            }
            var rg = new data(id, item);
            _bl.Add(rg);
        }

    }

}