using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.SrcTransporte.ServPrestado.AgregarEditar
{
    public partial class Frm : Form
    {
        private IAgregarEditar _controlador;


        public Frm()
        {
            InitializeComponent();
        }

        private void Frm_Load(object sender, EventArgs e)
        {
            IrFoco_Identificacion();
            TB_CODIGO.Text = _controlador.Ficha.Codigo_GetData;
            TB_DESCRIPCION.Text = _controlador.Ficha.Descripcion_GetData;
            TB_DETALLE.Text = _controlador.Ficha.Detalle_GetData;
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
            if (_controlador.ProcesarIsOK || _controlador.AbandonarIsOK)
            {
                e.Cancel = false;
            }
        }


        public void setControlador(IAgregarEditar ctr)
        {
            _controlador = ctr;
        }


        private void TB_CODIGO_Leave(object sender, EventArgs e)
        {
            _controlador.Ficha.setCodigo(TB_CODIGO.Text.Trim().ToUpper());
            TB_CODIGO.Text = _controlador.Ficha.Codigo_GetData;
        }
        private void TB_DESCRIPCION_Leave(object sender, EventArgs e)
        {
            _controlador.Ficha.setDescripcion(TB_DESCRIPCION.Text);
            TB_DESCRIPCION.Text = _controlador.Ficha.Descripcion_GetData;
        }
        private void TB_DETALLE_Leave(object sender, EventArgs e)
        {
            _controlador.Ficha.setDetalle(TB_DETALLE.Text.Trim());
            TB_DETALLE.Text = _controlador.Ficha.Detalle_GetData;
        }


        private void BT_IDENTIFICACION_Click(object sender, EventArgs e)
        {
            ApagarTodos();
            GB_IDENTIFICACION.Visible = true;
            IrFoco_Identificacion();
        }
        private void BT_PROCESAR_Click(object sender, EventArgs e)
        {
            IrFoco_Identificacion();
            ProcesarFicha();
        }
        private void BT_SALIR_Click(object sender, EventArgs e)
        {
            IrFoco_Identificacion();
            AbandonarFicha();
        }


        private void ProcesarFicha()
        {
            _controlador.Procesar();
            if (_controlador.ProcesarIsOK) 
            {
                Salir();
            }
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

        private void ApagarTodos()
        {
            GB_IDENTIFICACION.Visible = false;
        }
        private void IrFoco_Identificacion()
        {
            GB_IDENTIFICACION.Focus();
            TB_CODIGO.Focus();
        }
    }
}