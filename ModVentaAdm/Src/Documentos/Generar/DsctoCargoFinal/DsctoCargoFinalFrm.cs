using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.Documentos.Generar.DsctoCargoFinal
{

    public partial class DsctoCargoFinalFrm : Form
    {


        private Gestion _controlador;


        public DsctoCargoFinalFrm()
        {
            InitializeComponent();
        }


        public void setControlador(Gestion ctr)
        {
            _controlador = ctr;
        }

        private void BT_PROCESAR_Click(object sender, EventArgs e)
        {
            Procesar();
        }

        private void Procesar()
        {
            _controlador.Procesar();
            if (_controlador.IsOk) 
            {
                Salir();
            }
        }

        private void Salir()
        {
            this.Close();
        }

        private void DsctoCargoFinalFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            if (_controlador.IsOk || _controlador.AbandonarIsOk) 
            {
                e.Cancel = false;
            }
        }

        private void BT_SALIR_Click(object sender, EventArgs e)
        {
            Abandonar();
        }

        private void Abandonar()
        {
            _controlador.Abandonar();
        }

        private void DsctoCargoFinalFrm_Load(object sender, EventArgs e)
        {
            L_MONTO.Text = _controlador.Monto.ToString("n2");
            Actualizar();
        }

        private void TB_DSCTO_Leave(object sender, EventArgs e)
        {
            var dscto = decimal.Parse(TB_DSCTO.Text);
            _controlador.setDscto(dscto);
            Actualizar();
        }

        private void Actualizar()
        {
            TB_DSCTO.Text = _controlador.DsctoFinal.ToString("n2");
            TB_CARGO.Text = _controlador.CargoFinal.ToString("n2");
            L_TOTAL.Text = _controlador.Total.ToString("n2");
            L_TOTAL_DIVISA.Text = _controlador.TotalDivisa.ToString("n2");
        }

        private void TB_CARGO_Leave(object sender, EventArgs e)
        {
            var cargo = decimal.Parse(TB_CARGO.Text);
            _controlador.setCargo(cargo);
            Actualizar();
        }

        private void TB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void BT_ELIMINAR_DSCTO_Click(object sender, EventArgs e)
        {
            EliminarDscto();
        }

        private void EliminarDscto()
        {
            _controlador.EliminarDscto();
            Actualizar();
        }

        private void BT_ELIMINAR_CARGO_Click(object sender, EventArgs e)
        {
            EliminarCargo();
        }

        private void EliminarCargo()
        {
            _controlador.EliminarCargo();
            Actualizar();
        }

    }

}