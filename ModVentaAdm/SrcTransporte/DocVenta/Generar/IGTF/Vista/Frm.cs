﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.SrcTransporte.DocVenta.Generar.IGTF.Vista
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
            TB_TASA.Text = _controlador.Get_TasaIGTF.ToString("n2",CultureInfo.CurrentCulture);
            TB_MONTO.Text = _controlador.Get_MontoAplicarIGTF.ToString("n2", CultureInfo.CurrentCulture);
        }
        private void Frm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            if (_controlador.BtAbandonar.OpcionIsOK || _controlador.ProcesarIsOk) 
            {
                e.Cancel = false;
            }
        }
        public void setControlador(IVista ctr)
        {
            _controlador = ctr;
        }
        private void Ctrl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }
        //
        private void TB_TASA_Leave(object sender, EventArgs e)
        {
            var _tasa = decimal.Parse(TB_TASA.Text);
            _controlador.setTasaIGTF(_tasa);
        }
        private void TB_MONTO_Leave(object sender, EventArgs e)
        {
            var _monto = decimal.Parse(TB_MONTO.Text);
            _controlador.setMontoAplicarIGTF(_monto);
        }
        private void BT_GUARDAR_Click(object sender, EventArgs e)
        {
            Procesar();
        }
        private void BT_SALIR_Click(object sender, EventArgs e)
        {
            Abandonar();
        }
        //
        private void Abandonar()
        {
            _controlador.BtAbandonar.Opcion();
            if (_controlador.BtAbandonar.OpcionIsOK)
            {
                salir();
            }
        }
        private void Procesar()
        {
            _controlador.Procesar();
            if (_controlador.ProcesarIsOk)
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