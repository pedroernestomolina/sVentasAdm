using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.CxC.Tools.PanelPrincipal.Pago.vistas
{
    public partial class vCliente : Form
    {
        private PanelPrincipal.Pago.ICliente _controlador;
        //
        public vCliente()
        {
            InitializeComponent();
        }
        public void setControlador(PanelPrincipal.Pago.ICliente ctr)
        {
            _controlador = ctr;
        }
        private void vCliente_Load(object sender, EventArgs e)
        {
        }
        private void vCliente_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            if (_controlador.AbandonarFichaIsOk || _controlador.ProcesarFichaIsOk)
            {
                e.Cancel = false;
            }
        }
        private void BT_PROCESAR_Click(object sender, EventArgs e)
        {
            ProcesarFicha();
        }
        private void BT_SALIR_Click(object sender, EventArgs e)
        {
            AbandonarFicha();
        }
        //
        private void ProcesarFicha()
        {
            _controlador.ProcesarFicha();
            if (_controlador.ProcesarFichaIsOk) 
            {
                salir();
            }
        }
        private void AbandonarFicha()
        {
            _controlador.AbandonarFicha();
            if (_controlador.AbandonarFichaIsOk) 
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