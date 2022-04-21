using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.Seguridad
{

    public partial class SeguridadFrm : Form
    {

        public bool IsClaveExitosa { get; set; }
        public string Clave { get; set; }


        public SeguridadFrm()
        {
            InitializeComponent();
        }

        private void SeguridadFrm_Load(object sender, EventArgs e)
        {
            IsClaveExitosa = false;
            Clave = "";
            TB_CLAVE.Text = "";
            TB_CLAVE.Focus();
        }

        private void BT_ACEPTAR_Click(object sender, EventArgs e)
        {
            Clave = TB_CLAVE.Text.Trim().ToUpper();
            IsClaveExitosa = !(Clave == "");
            Close();
        }

        private void BT_SALIR_Click(object sender, EventArgs e)
        {
            Salir();
        }

        private void Salir()
        {
            this.Close();
        }

        private void TB_CLAVE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

    }

}