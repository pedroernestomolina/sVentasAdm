using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.SrcTransporte.DocVenta.Generar.EntradaNumeroDoc.Vista
{
    public partial class Frm : Form
    {
        private IVista _controlador;
        //
        public Frm()
        {
            InitializeComponent();
        }
        private void Frm_Load(object sender, EventArgs e)
        {
            TB_NUMERO_DOC.Text = _controlador.Get_NumDocGenerar;
        }
        private void Frm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            if (_controlador.BtAbandonar.OpcionIsOK || _controlador.BtAceptar.OpcionIsOK) 
            {
                e.Cancel = false;
            }
        }

        private void TB_NUMERO_DOC_Leave(object sender, EventArgs e)
        {
            _controlador.setNumeroDoc(TB_NUMERO_DOC.Text);
        }

        public void setControlador(IVista ctr)
        {
            _controlador = ctr;
        }

        private void BT_PROCESAR_DOC_Click(object sender, EventArgs e)
        {
            AceptarFicha();
        }
        private void BT_SALIDA_Click(object sender, EventArgs e)
        {
            AbandonarFicha();
        }
        //
        private void AceptarFicha()
        {
            _controlador.BtAceptar.Opcion();
            if (_controlador.BtAceptar.OpcionIsOK)
            {
                salir();
            }
        }
        private void AbandonarFicha()
        {
            _controlador.BtAbandonar.Opcion();
            if (_controlador.BtAbandonar.OpcionIsOK) 
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