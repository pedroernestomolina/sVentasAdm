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


namespace ModVentaAdm.SrcTransporte.DocVenta.Generar.TasaDivisa
{
    public partial class Frm : Form
    {
        private ITasa _controlador;


        public Frm()
        {
            InitializeComponent();
        }
        private void Frm_Load(object sender, EventArgs e)
        {
            L_TASA.Text = _controlador.Titulo_Get;
            TB_TASA.Text = _controlador.TasaActual_Get.ToString("n2",CultureInfo.CurrentCulture);
        }
        private void Frm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            if (_controlador.ProcesarIsOK || _controlador.AbandonarIsOK) 
            {
                e.Cancel = false;
            }
        }
        public void setControlador(ITasa ctr)
        {
            _controlador = ctr;
        }
        private void Ctrl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }


        private void TB_TASA_Leave(object sender, EventArgs e)
        {
            var _tasa = decimal.Parse(TB_TASA.Text);
            _controlador.setTasaDivisa(_tasa);
            TB_TASA.Text = _controlador.TasaActual_Get.ToString("n2", CultureInfo.CurrentCulture);
        }


        private void BT_GUARDAR_Click(object sender, EventArgs e)
        {
            Procesar();
        }
        private void BT_SALIR_Click(object sender, EventArgs e)
        {
            Abandonar();
        }


        private void Abandonar()
        {
            _controlador.AbandonarFicha();
            if (_controlador.AbandonarIsOK)
            {
                Salir();
            }
        }
        private void Procesar()
        {
            _controlador.Procesar();
            if (_controlador.ProcesarIsOK)
            {
                Salir();
            }
        }
        private void Salir()
        {
            this.Close();
        }
    }
}