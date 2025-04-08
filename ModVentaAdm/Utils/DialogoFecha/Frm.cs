using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModVentaAdm.Utils.DialogoFecha
{
    public partial class Frm : Form
    {
        private IDialogoFecha _controlador;
        //
        public Frm()
        {
            InitializeComponent();
        }
        public void setControador(IDialogoFecha ctr)
        {
            _controlador = ctr;
        }
        private void Frm_Load(object sender, EventArgs e)
        {
            DTP_DESDE.Value = _controlador.GetFecha;
        }
        private void DTP_DESDE_Leave(object sender, EventArgs e)
        {
            _controlador.setFecha(DTP_DESDE.Value.Date);
        }
        private void DTP_DESDE_ValueChanged(object sender, EventArgs e)
        {
            _controlador.setFecha(DTP_DESDE.Value.Date);
        }
        private void BT_OK_Click(object sender, EventArgs e)
        {
            _controlador.Procesar();
        }
        private void BT_SALIR_Click(object sender, EventArgs e)
        {
            salir();
        }
        //
        private void salir()
        {
            this.Close();
        }
    }
}