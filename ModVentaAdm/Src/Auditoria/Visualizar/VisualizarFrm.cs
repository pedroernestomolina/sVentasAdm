using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.Auditoria.Visualizar
{

    public partial class VisualizarFrm : Form
    {


        private Gestion _controlador;


        public VisualizarFrm()
        {
            InitializeComponent();
        }

        public void setControlador(Gestion ctr)
        {
            _controlador = ctr;
        }

        private void VisualizarFrm_Load(object sender, EventArgs e)
        {
            L_MOTIVO.Text = _controlador.Motivo;
            L_FECHA.Text = _controlador.Fecha;
        }

        private void BT_SALIDA_Click(object sender, EventArgs e)
        {
            Salida();
        }

        private void Salida()
        {
            this.Close();
        }

    }

}
