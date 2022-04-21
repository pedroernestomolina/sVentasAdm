using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.Maestros.Zona
{

    public partial class AgregarEditarFrm : Form
    {

        private AgregarEditar _controlador;


        public AgregarEditarFrm()
        {
            InitializeComponent();
        }

        public void setTitulo(string p)
        {
            L_TITULO.Text = p;
        }

        private void AgregarEditarFrm_Load(object sender, EventArgs e)
        {
            TB_CODIGO.Text = _controlador.Codigo;
            TB_NOMBRE.Text = _controlador.Nombre;
            TB_CODIGO.Focus();
        }

        private void BT_SALIR_Click(object sender, EventArgs e)
        {
            Salir();
        }

        private void Salir()
        {
            TB_CODIGO.Focus();
            this.Close();
        }

        private void BT_GUARDAR_Click(object sender, EventArgs e)
        {
            GuardarData();
        }

        private void GuardarData()
        {
            _controlador.Guardar();
            if (_controlador.IsOk)
            {
                Salir();
            }
        }

        public void setControlador(AgregarEditar ctr)
        {
            _controlador = ctr;
        }

        private void TB_NOMBRE_TextChanged(object sender, EventArgs e)
        {
            _controlador.setNombre(TB_NOMBRE.Text.Trim().ToUpper());
        }

        private void TB_CODIGO_TextChanged(object sender, EventArgs e)
        {
            _controlador.setCodigo(TB_CODIGO.Text.Trim().ToUpper());
        }

        private void TB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void AgregarEditarFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_controlador.IsOk)
            {
                if (!_controlador.AbandonarDocumento())
                {
                    e.Cancel = true;
                }
            }
        }

    }

}