using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.Cliente.Estatus
{

    public partial class EstatusFrm : Form
    {

        private Gestion _controlador;


        public EstatusFrm()
        {
            InitializeComponent();
        }


        public void setControlador(Gestion ctr)
        {
            _controlador = ctr;
        }

        private void EstatusFrm_Load(object sender, EventArgs e)
        {
            L_PRODUCTO.Text = _controlador.Cliente;
            if (_controlador.Estatus== Gestion.EnumEstatus.Activo)
                RB_ACTIVO.Checked = true;
            else
                RB_INACTIVO.Checked = true;
        }

        private bool AbandonarOk; 
        private void BT_SALIR_Click(object sender, EventArgs e)
        {
            AbandonarOk = false;
            _controlador.Salir();
            if (_controlador.AbandonarIsOk)
            {
                AbandonarOk = true;
                Salir();
            }
        }

        private void Salir()
        {
            Close();
        }

        private void BT_PROCESAR_Click(object sender, EventArgs e)
        {
            Procesar();
        }

        private bool SalidaOk; 
        private void Procesar()
        {
            SalidaOk = false;
            _controlador.Procesar();
            if (_controlador.ProcesarIsOk)
            {
                SalidaOk = true;
                Salir();
            }
        }

        private void EstatusFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (SalidaOk || AbandonarOk)
            { }
            else
            {
                e.Cancel = true;
            }
        }

        private void RB_INACTIVO_CheckedChanged(object sender, EventArgs e)
        {
            if (RB_INACTIVO.Checked)
                _controlador.setEstatusInactivo();
        }

        private void RB_ACTIVO_CheckedChanged(object sender, EventArgs e)
        {
            if (RB_ACTIVO.Checked)
                _controlador.setEstatusActivo();
        }

    }

}