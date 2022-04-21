using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.Documentos.Generar.DatosDocumento
{

    public partial class DatosDocFrm : Form
    {

        private Gestion _controlador;
        private bool _modoEditar; 


        public DatosDocFrm()
        {
            _modoEditar = false;
            InitializeComponent();
            InicialzarCombos();
        }

        private void InicialzarCombos()
        {
            CB_COBRADOR.ValueMember = "id";
            CB_COBRADOR.DisplayMember = "desc";
            CB_COND_PAGO.ValueMember = "id";
            CB_COND_PAGO.DisplayMember = "desc";
            CB_DEPOSITO.ValueMember = "id";
            CB_DEPOSITO.DisplayMember = "desc";
            CB_SUCURSAL.ValueMember = "id";
            CB_SUCURSAL.DisplayMember = "desc";
            CB_TRANSPORTE.ValueMember = "id";
            CB_TRANSPORTE.DisplayMember = "desc";
            CB_VENDEDOR.ValueMember = "id";
            CB_VENDEDOR.DisplayMember = "desc";
        }


        public void setControlador(Gestion ctr)
        {
            _controlador = ctr;
        }

        private void BT_SALIR_Click(object sender, EventArgs e)
        {
            AbandonarCambios();
        }

        private void AbandonarCambios()
        {
            _controlador.AbandonarCambios();
            if (_controlador.AbandonarCambiosIsOk) 
            {
                Salir();
            }
        }

        private void Salir()
        {
            this.Close();
        }

        private void DatosDocFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            if (_controlador.AbandonarCambiosIsOk || _controlador.AceptarDatosIsOK)
            {
                e.Cancel = false;
            }
        }

        private void DatosDocFrm_Load(object sender, EventArgs e)
        {
            _modoEditar = false;
            CB_COBRADOR.DataSource = _controlador.SourceCobrador;
            CB_COND_PAGO.DataSource = _controlador.SourceCondPago;
            CB_DEPOSITO.DataSource = _controlador.SourceDeposito;
            CB_SUCURSAL.DataSource = _controlador.SourceSucursal;
            CB_TRANSPORTE.DataSource = _controlador.SourceTransporte;
            CB_VENDEDOR.DataSource = _controlador.SourceVendedor;
            L_CLIENTE.Text = _controlador.DataCliente;
            TB_FECHA.Text = _controlador.GetData.Fecha.ToShortDateString();
            TB_FECHA_VENCE.Text = _controlador.GetData.FechaVence.ToShortDateString();
            TB_ORDEN_COMPRA.Text = _controlador.DataOrdenCompra;
            TB_PEDIDO.Text = _controlador.DataPedido;
            TB_FECHA_PEDIDO.Text = _controlador.DataFechaPedido;
            TB_DIR_DESPACHO.Text = _controlador.DataDirDespacho;
            TB_DIAS_VALIDEZ.Text = _controlador.DataDiasValidez.ToString();
            TB_DIAS_CREDITO.Text = _controlador.DataDiasCredito.ToString();
            CB_COBRADOR.SelectedValue = _controlador.DataIdCobrador;
            CB_COND_PAGO.SelectedValue = _controlador.DataIdCondPago;
            CB_DEPOSITO.SelectedValue = _controlador.DataIdDeposito;
            CB_SUCURSAL.SelectedValue = _controlador.DataIdSucursal;
            CB_TRANSPORTE.SelectedValue = _controlador.DataIdTransporte;
            CB_VENDEDOR.SelectedValue = _controlador.DataIdVendedor;
            //
            TB_DIAS_VALIDEZ.Enabled = _controlador.HabilitarDiasValidez;
            TB_DIR_DESPACHO.Enabled = _controlador.HabilitarDirDespacho;
            TB_ORDEN_COMPRA.Enabled = _controlador.HabilitarOrdenCompra;
            TB_PEDIDO.Enabled = _controlador.HabilitarPedido;
            CB_SUCURSAL.Enabled = _controlador.HabilitarSucursal;
            CB_DEPOSITO.Enabled = _controlador.HabilitarDeposito;
            BT_BUSCAR_CLIENTE.Enabled = _controlador.HabilitarBusquedaCliente;
            //
            _modoEditar = true;
        }

        private void CB_COND_PAGO_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_modoEditar)
            {
                _controlador.setCondPago(CB_COND_PAGO.SelectedValue.ToString());
                TB_DIAS_CREDITO.Text = _controlador.DataDiasCredito.ToString();
                TB_FECHA_VENCE.Text = _controlador.GetData.FechaVence.ToShortDateString(); ;
            }
        }

        private void CB_SUCURSAL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_modoEditar)
            {
                _controlador.setSucursal(CB_SUCURSAL.SelectedValue.ToString());
                CB_DEPOSITO.SelectedIndex = -1;
            }
        }

        private void TB_DIAS_CREDITO_Leave(object sender, EventArgs e)
        {
            _controlador.setDiasCredito(int.Parse(TB_DIAS_CREDITO.Text));
            TB_FECHA_VENCE.Text = _controlador.GetData.FechaVence.ToShortDateString();
        }

        private void CB_VENDEDOR_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_modoEditar)
            {
                _controlador.setVendedor(CB_VENDEDOR.SelectedValue.ToString());
            }
        }

        private void CB_COBRADOR_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_modoEditar)
            {
                _controlador.setCobrador(CB_COBRADOR.SelectedValue.ToString());
            }
        }

        private void CB_DEPOSITO_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_modoEditar)
            {
                if (CB_DEPOSITO.SelectedIndex == -1) 
                {
                    _controlador.setDeposito("");
                    return;
                }
                _controlador.setDeposito(CB_DEPOSITO.SelectedValue.ToString());
            }
        }

        private void CB_TRANSPORTE_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_modoEditar)
            {
                _controlador.setTransporte(CB_TRANSPORTE.SelectedValue.ToString());
            }
        }

        private void TB_DIAS_VALIDEZ_Leave(object sender, EventArgs e)
        {
            _controlador.setDiasValidez(int.Parse(TB_DIAS_VALIDEZ.Text));
        }

        private void TB_DIR_DESPACHO_Leave(object sender, EventArgs e)
        {
            _controlador.setDirDespacho(TB_DIR_DESPACHO.Text);
        }

        private void TB_ORDEN_COMPRA_Leave(object sender, EventArgs e)
        {
            _controlador.setOrdenCompra(TB_ORDEN_COMPRA.Text);
        }

        private void TB_PEDIDO_Leave(object sender, EventArgs e)
        {
            _controlador.setPedido(TB_PEDIDO.Text);
        }

        private void DatosDocFrm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void BT_ACEPTAR_Click(object sender, EventArgs e)
        {
            AceptarDatos();
        }

        private void AceptarDatos()
        {
            _controlador.AceptarDatos();
            if (_controlador.AceptarDatosIsOK)
            {
                Salir();
            }

        }

        private void BT_BUSCAR_CLIENTE_Click(object sender, EventArgs e)
        {
            BuscarCliente();
        }

        private void BuscarCliente()
        {
            _controlador.BuscarCliente();
            L_CLIENTE.Text = _controlador.DataCliente;
            //
            _modoEditar = false;
            CB_VENDEDOR.SelectedValue = _controlador.DataIdVendedor;
            CB_COBRADOR.SelectedValue = _controlador.DataIdCobrador;
            CB_COND_PAGO.SelectedValue = _controlador.DataIdCondPago;
            TB_DIR_DESPACHO.Text = _controlador.DataDirDespacho;
            TB_DIAS_CREDITO.Text = _controlador.DataDiasCredito.ToString();
            _modoEditar = true;
            //
        }
      
    }

}