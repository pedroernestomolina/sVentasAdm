using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.CxC.Tools.GestionPago.DetalleCobro
{

    public partial class DetalleFrm : Form
    {

        private IDetalle _controlador;


        public DetalleFrm()
        {
            InitializeComponent();
            InicializaCombo();
        }
        private void InicializaCombo()
        {
            CB_COBRADOR.ValueMember="id";
            CB_COBRADOR.DisplayMember = "desc";
        }

        public void setControlador(IDetalle ctr)
        {
            _controlador = ctr;
        }

        private bool _modoIncializar;
        private void DetalleFrm_Load(object sender, EventArgs e)
        {
            _modoIncializar = true;
            CB_COBRADOR.DataSource = _controlador.CobradorSource;
            CB_COBRADOR.SelectedValue = _controlador.GetIdCobrador;
            TB_NOTAS.Text = _controlador.GetNotas;
            _modoIncializar = false;
        }

        private void BT_PROCESAR_Click(object sender, EventArgs e)
        {
            Procesar();
        }
        private void Procesar()
        {
            _controlador.Procesar();
            if (_controlador.ProcesarIsOK) 
            {
                Salir();
            }
        }
        private void BT_SALIR_Click(object sender, EventArgs e)
        {
            AbandonarFicha();
        }
        private void AbandonarFicha()
        {
            _controlador.AbandonarFicha();
            if (_controlador.AbandonarIsOK) 
            {
                Salir();
            }
        }

        private void Salir()
        {
            this.Close();
        }
        private void DetalleFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            if (_controlador.ProcesarIsOK || _controlador.AbandonarIsOK) 
            {
                e.Cancel = false;
            }
        }

        private void TB_NOTAS_Leave(object sender, EventArgs e)
        {
            _controlador.setNotas(TB_NOTAS.Text.Trim());
        }
        private void CB_COBRADOR_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_modoIncializar) { return; }
            _controlador.setCobrador("");
            if (CB_COBRADOR.SelectedIndex != -1) 
            {
                _controlador.setCobrador(CB_COBRADOR.SelectedValue.ToString());
            }
        }
        private void TB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

    }

}