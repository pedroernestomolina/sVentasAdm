using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.SrcTransporte.DocVenta.Generar.NotasPeriodo.Vista
{
    public partial class Frm : Form
    {
        private Vista.INotas _controlador;


        public Frm()
        {
            InitializeComponent();
        }
        private void Frm_Load(object sender, EventArgs e)
        {
            L_TITULO.Text = _controlador.Titulo_Get;
            TB_NOTAS.Text = _controlador.Notas_Get;
            TB_NOTAS.Focus();
        }
        private void Frm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            if (_controlador.AbandonarIsOK || _controlador.ProcesarIsOK)
            {
                e.Cancel = false;
            }
        }
        private void Ctr_KeyDown(object sender, KeyEventArgs e)
        {

        }
        public void setControlador(Vista.INotas ctr)
        {
            _controlador = ctr;
        }


        private void TB_NOTAS_Leave(object sender, EventArgs e)
        {
            _controlador.setNotas(TB_NOTAS.Text.Trim());
        }


        private void BT_PROCESAR_Click(object sender, EventArgs e)
        {
            Procesar();
        }
        private void BT_SALIR_Click(object sender, EventArgs e)
        {
            Abandonar();
        }


        private void Procesar()
        {
            irInicio();
            _controlador.Procesar();
            if (_controlador.ProcesarIsOK)
            {
                Salir();
            }
        }
        private void Abandonar()
        {
            irInicio();
            _controlador.AbandonarFicha();
            if (_controlador.AbandonarIsOK)
            {
                Salir();
            }
        }
        private void irInicio()
        {
            TB_NOTAS.Focus();
        }
        private void Salir()
        {
            this.Close();
        }
    }
}