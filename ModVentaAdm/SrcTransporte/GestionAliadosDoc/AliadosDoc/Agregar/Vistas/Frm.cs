using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.SrcTransporte.GestionAliadosDoc.AliadosDoc.Agregar.Vistas
{
    public partial class Frm : Form
    {
        private IMain _controlador;
        private bool _modoInicializar = false;
        //
        private void InicializaCB()
        {
            CB_ALIADO.DisplayMember = "desc";
            CB_ALIADO.ValueMember = "id";
            CB_SERVICIO.DisplayMember = "desc";
            CB_SERVICIO.ValueMember = "id";
        }
        public Frm()
        {
            InitializeComponent();
            InicializaCB();
        }
        private void Frm_Load(object sender, EventArgs e)
        {
            _modoInicializar = true;
            CB_ALIADO.DataSource = _controlador.GestionData.Aliado.GetSource;
            CB_SERVICIO.DataSource = _controlador.GestionData.Servicio.GetSource;
            CB_ALIADO.SelectedValue = _controlador.GestionData.Aliado.GetId;
            CB_SERVICIO.SelectedValue = _controlador.GestionData.Servicio.GetId;
            TB_MONTO.Text = "0";
            TM_SERVCIO.Text = "";
            _modoInicializar = false;
        }
        private void Frm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }
        private void Frm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            if (_controlador.BtProcesar.OpcionIsOK || _controlador.BtAbandonar.OpcionIsOK)
            {
                e.Cancel = false;
            }
        }
        public void setControlador(IMain ctr)
        {
            _controlador = ctr;
        }

        private void TB_ALIADO_Leave(object sender, EventArgs e)
        {
            _controlador.GestionData.Aliado.setTextoBuscar(TB_ALIADO.Text.Trim().ToUpper());
        }
        private void TB_SERVICIO_Leave(object sender, EventArgs e)
        {
            _controlador.GestionData.Servicio.setTextoBuscar(TB_SERVICIO.Text.Trim().ToUpper());
        }
        private void CB_ALIADO_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_modoInicializar) { return; }
            _controlador.GestionData.Aliado.setFichaById("");
            if (CB_ALIADO.SelectedIndex != -1)
            {
                _controlador.GestionData.Aliado.setFichaById(CB_ALIADO.SelectedValue.ToString());
            }
        }
        private void CB_SERVICIO_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_modoInicializar) { return; }
            _controlador.GestionData.Servicio.setFichaById("");
            if (CB_SERVICIO.SelectedIndex != -1)
            {
                _controlador.GestionData.Servicio.setFichaById(CB_SERVICIO.SelectedValue.ToString());
            }
        }
        private void TB_MONTO_Leave(object sender, EventArgs e)
        {
            var monto = decimal.Parse(TB_MONTO.Text);
            _controlador.GestionData.setMonto(monto);
        }
        private void TM_SERVCIO_Leave(object sender, EventArgs e)
        {
            _controlador.GestionData.setServicio(TM_SERVCIO.Text.Trim());
        }
        private void BT_PROCESAR_Click(object sender, EventArgs e)
        {
            Procesar();
        }
        private void BT_SALIR_Click(object sender, EventArgs e)
        {
            Salir();
        }
        //
        private void Procesar()
        {
            _controlador.Procesar();
            if (_controlador.BtProcesar.OpcionIsOK)
            {
                salida();
            }
        }
        private void Salir()
        {
            _controlador.BtAbandonar.Opcion();
            if (_controlador.BtAbandonar.OpcionIsOK)
            {
                salida();
            }
        }
        private void salida()
        {
            this.Close();
        }
        //
    }
}