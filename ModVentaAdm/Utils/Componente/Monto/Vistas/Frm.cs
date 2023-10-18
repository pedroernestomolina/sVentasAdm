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


namespace ModVentaAdm.Utils.Componente.Monto.Vistas
{
    public partial class Frm : Form
    {
        private Vistas.IMonto _controlador;
        private CultureInfo _cult;


        public Frm()
        {
            InitializeComponent();
            _cult = CultureInfo.CurrentCulture;
        }
        public void setControlador(Vistas.IMonto ctr)
        {
            _controlador = ctr;
        }
        private void Frm_Load(object sender, EventArgs e)
        {
            TB_MONTO.Text = _controlador.Get_Monto.ToString("n2", _cult);
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
                this.SelectNextControl((System.Windows.Forms.Control)sender, true, true, true, true);
            }
        }


        private void TB_MONTO_Leave(object sender, EventArgs e)
        {
            var _monto = decimal.Parse(TB_MONTO.Text);
            _controlador.setMonto(_monto);
            TB_MONTO.Text = _controlador.Get_Monto.ToString("n2", _cult);
        }


        private void BT_PROCESAR_Click(object sender, EventArgs e)
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
    }
}