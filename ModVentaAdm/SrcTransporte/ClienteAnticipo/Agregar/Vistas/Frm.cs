using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.SrcTransporte.ClienteAnticipo.Agregar.Vistas
{
    public partial class Frm : Form
    {
        private CultureInfo _cult;
        private IHnd _controlador;


        private void InicializarDGV()
        {
            var f = new Font("Serif", 10, FontStyle.Bold);
            var f1 = new Font("Serif", 10, FontStyle.Regular);

            DGV.RowHeadersVisible = false;
            DGV.AllowUserToAddRows = false;
            DGV.AllowUserToDeleteRows = false;
            DGV.AutoGenerateColumns = false;
            DGV.AllowUserToResizeRows = false;
            DGV.AllowUserToResizeColumns = false;
            DGV.AllowUserToOrderColumns = false;
            DGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DGV.MultiSelect = false;

            var c1 = new DataGridViewTextBoxColumn();
            c1.DataPropertyName = "descripcion";
            c1.HeaderText = "Descripcion";
            c1.Visible = true;
            c1.HeaderCell.Style.Font = f;
            c1.DefaultCellStyle.Font = f1;
            c1.MinimumWidth = 250;
            c1.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            c1.ReadOnly = true;

            var c2 = new DataGridViewTextBoxColumn();
            c2.DataPropertyName = "SaldoActual";
            c2.HeaderText = "Saldo Actual";
            c2.Visible = true;
            c2.Width = 100;
            c2.HeaderCell.Style.Font = f;
            c2.DefaultCellStyle.Font = f1;
            c2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            c2.DefaultCellStyle.Format = "n2";
            c2.ReadOnly = true;

            var c3 = new DataGridViewTextBoxColumn();
            c3.DataPropertyName = "montoAbonar";
            c3.HeaderText = "Monto Abonar";
            c3.Visible = true;
            c3.Width = 100;
            c3.HeaderCell.Style.Font = f;
            c3.DefaultCellStyle.Font = f;
            c3.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            c3.DefaultCellStyle.Format = "n2";
            c3.ReadOnly = true;

            DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
            buttonColumn.HeaderText = ""; // Deja el encabezado en blanco
            buttonColumn.Name = "Ed";
            buttonColumn.Text = "Ed"; // Texto que se mostrará en el botón
            buttonColumn.UseColumnTextForButtonValue = true; // Usa el texto del botón para todas las celdas
            buttonColumn.Width = 60;

            DGV.Columns.Add(c1);
            DGV.Columns.Add(c2);
            DGV.Columns.Add(c3);
            DGV.Columns.Add(buttonColumn);
        }
        public Frm()
        {
            InitializeComponent();
            InicializarDGV();
            _cult = CultureInfo.CurrentCulture;
        }
        public void setControlador(IHnd ctr)
        {
            _controlador = ctr;
        }
        private bool _modoInicializa;
        private void Frm_Load(object sender, EventArgs e)
        {
            _modoInicializa = true;
            DGV.DataSource = _controlador.caja.Get_CajaSource;
            DGV.CellContentClick += DGV_CellContentClick; 
            L_ALIADO_INFO.Text = _controlador.data.Get_ClienteInfo;
            DTP_FECHA.Value= _controlador.data.Get_FechaAnticipo;
            TB_MONTO.Text = _controlador.data.Get_MontoAnticipoMonDiv.ToString("n2",_cult);
            TB_FACTOR.Text = _controlador.data.Get_TasaFactorCambio.ToString("n2",_cult);
            TB_MOTIVO.Text = _controlador.data.Get_Motivo;
            CHB_APLICA_RET.Checked = _controlador.data.Get_AplicaRet;
            ActualizarTotal();
            ActualizarRetencion();
            _modoInicializa = false;
        }
        void DGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == DGV.Columns["Ed"].Index && e.RowIndex >= 0)
            {
                _controlador.caja.EditarMontoAbonar();
                _controlador.ActualizarSaldoCaja();
                L_MONTO_PEND_MON_DIV.Text = _controlador.caja.Get_MontoPendMonDiv.ToString("n2", _cult);
                L_MONTO_PEND_MON_ACT.Text = _controlador.caja.Get_MontoPendMonAct.ToString("n2", _cult);
            }                
        }
        private void Frm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            if (_controlador.AbandonarIsOK || _controlador.ProcesarIsOK)
            {
                e.Cancel = false;
            }
        }
        private void CTRL_KEYDOWN(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }


        private void BT_ACEPTAR_Click(object sender, EventArgs e)
        {
            ProcesarFicha();
        }
        private void BT_SALIR_Click(object sender, EventArgs e)
        {
            AbandonarFicha();
        }


        private void ProcesarFicha()
        {
            _controlador.Procesar();
            if (_controlador.ProcesarIsOK)
            {
                salir();
            }
        }
        private void AbandonarFicha()
        {
            _controlador.AbandonarFicha();
            if (_controlador.AbandonarIsOK)
            {
                salir();
            }
        }
        private void salir()
        {
            this.Close();
        }

        private void DTP_FECHA_Leave(object sender, EventArgs e)
        {
            _controlador.data.setFechaAnticipo(DTP_FECHA.Value);
        }
        private void DTP_FECHA_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = DTP_FECHA.Value > _controlador.data.Get_FechaServidor;
        }
        private void TB_MONTO_Leave(object sender, EventArgs e)
        {
            var _monto = decimal.Parse(TB_MONTO.Text);
            _controlador.data.setMontoAnticipoMonDiv(_monto);
            TB_MONTO.Text = _controlador.data.Get_MontoAnticipoMonDiv.ToString("n2", _cult);
            ActualizarTotal();
        }
        private void TB_FACTOR_Leave(object sender, EventArgs e)
        {
            var _monto = decimal.Parse(TB_FACTOR.Text);
            _controlador.data.setTasaFactorCambio(_monto);
            TB_FACTOR.Text = _controlador.data.Get_TasaFactorCambio.ToString("n2", _cult);
            ActualizarTotal();
        }
        private void TB_MOTIVO_Leave(object sender, EventArgs e)
        {
            _controlador.data.setMotivo(TB_MOTIVO.Text.Trim().ToUpper());
            TB_MOTIVO.Text = _controlador.data.Get_Motivo;
        }
        private void CHB_APLICA_RET_CheckedChanged(object sender, EventArgs e)
        {
            _controlador.data.setAplicaRet(CHB_APLICA_RET.Checked);
            ActualizarTotal();
            ActualizarRetencion();
        }

        private void TB_RET_TASA_Leave(object sender, EventArgs e)
        {
            var _monto = decimal.Parse(TB_RET_TASA.Text);
            _controlador.data.setTasaRet(_monto);
            TB_RET_TASA.Text = _controlador.data.Get_TasaRetencion.ToString("n2", _cult);
            ActualizarTotal();
        }
        private void TB_RET_SUSTRAENDO_Leave(object sender, EventArgs e)
        {
            var _monto = decimal.Parse(TB_RET_SUSTRAENDO.Text);
            _controlador.data.setMontoSustraendo(_monto);
            TB_RET_SUSTRAENDO.Text = _controlador.data.Get_MontoSustraendo.ToString("n2", _cult);
            ActualizarTotal();
        }


        private void ActualizarTotal()
        {
            L_MONTO_MONEDA_ACT.Text = "Monto Bs: " + _controlador.data.Get_MontoAnticipoMonAct.ToString("n2",_cult);
            L_MONTO_RETENCION.Text = _controlador.data.Get_MontoRetencion.ToString("n2", _cult);
            L_MONTO_ABONA_MON_DIV.Text = _controlador.data.Get_MontoAbonoMonDiv.ToString("n2", _cult);
            L_MONTO_ABONA_MON_ACT.Text = _controlador.data.Get_MontoAbonoMonAct.ToString("n2", _cult);
        }
        private void ActualizarRetencion()
        {
            TB_RET_TASA.Text = _controlador.data.Get_TasaRetencion.ToString("n2", _cult);
            TB_RET_SUSTRAENDO.Text = _controlador.data.Get_MontoSustraendo.ToString("n2", _cult);
            TB_RET_TASA.Enabled = _controlador.data.Get_AplicaRet;
            TB_RET_SUSTRAENDO.Enabled = _controlador.data.Get_AplicaRet;
            L_MONTO_ABONA_MON_DIV.Text = _controlador.data.Get_MontoAbonoMonDiv.ToString("n2", _cult);
            L_MONTO_ABONA_MON_ACT.Text = _controlador.data.Get_MontoAbonoMonAct.ToString("n2", _cult);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1) 
            {
                _controlador.ActualizarSaldoCaja();
                L_MONTO_PEND_MON_DIV.Text = _controlador.caja.Get_MontoPendMonDiv.ToString("n2",_cult);
                L_MONTO_PEND_MON_ACT.Text = _controlador.caja.Get_MontoPendMonAct.ToString("n2", _cult);
            }
        }
    }
}