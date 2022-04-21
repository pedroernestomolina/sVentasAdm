using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.Documentos.Generar.CambioTasa
{

    public partial class CambioTasaFrm : Form
    {

        private Gestion _controlador;


        public CambioTasaFrm()
        {
            InitializeComponent();
        }

        public void setControlador(Gestion ctr)
        {
            _controlador = ctr;
        }

        private void CambioTasaFrm_Load(object sender, EventArgs e)
        {
            ActualizarTasa();
            IrFoco();
        }

        private void ActualizarTasa()
        {
            TB_TASA.Text = _controlador.TasaCambiar.ToString("n2");
        }

        private void IrFoco()
        {
            TB_TASA.Focus();
        }

        private void TB_TASA_Leave(object sender, EventArgs e)
        {
            var t = decimal.Parse(TB_TASA.Text);
            _controlador.setTasa(t);
            ActualizarTasa();

        }

        private void BT_PROCESAR_Click(object sender, EventArgs e)
        {
            Procesar();
        }

        private void Procesar()
        {
            _controlador.Procesar();
            if (_controlador.CambioTasaIsOk)
            {
                Salir();
            }
        }

        private void Salir()
        {
            this.Close();
        }

        private void CambioTasaFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            if (_controlador.CambioTasaIsOk || _controlador.AbandonarIsOk)
            {
                e.Cancel = false;
            }
        }

        private void BT_SALIR_Click(object sender, EventArgs e)
        {
            Abandonar();
        }

        private void Abandonar()
        {
            _controlador.Abandonar();
            if (_controlador.AbandonarIsOk) 
            {
                Salir();
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