using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModVentaAdm.ReglasNegocio.EditarItemsAntesImprimir.Editar
{
    public partial class Frm : Form
    {
        private IEditar _controlador;
        //
        public Frm()
        {
            InitializeComponent();
        }
        public void setControlador(HndEditar ctr)
        {
            _controlador = ctr;
        }
        private void Frm_Load(object sender, EventArgs e)
        {
            L_TITULO.Text = _controlador.GetTitulo;
            TB_DETALLE.Text = _controlador.GetDetalle;
        }
        private void TB_DETALLE_Leave(object sender, EventArgs e)
        {
            _controlador.setItemDescripcion(TB_DETALLE.Text);
        }
        private void BT_ACEPTAR_Click(object sender, EventArgs e)
        {
            Guardar();
        }
        private void BT_SALIR_Click(object sender, EventArgs e)
        {
            Salir();
        }
        //
        private void Guardar()
        {
            _controlador.Guardar();
            if (_controlador.BtProcesar.OpcionIsOK)
            {
                cerrar();
            }

        }
        private void Salir()
        {
            _controlador.BtSalir.Opcion();
            if (_controlador.BtSalir.OpcionIsOK) 
            {
                cerrar();
            }
        }
        private void cerrar()
        {
            this.Close();
        }
    }
}