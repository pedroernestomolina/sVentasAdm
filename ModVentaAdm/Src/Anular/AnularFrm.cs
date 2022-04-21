﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.Anular
{

    public partial class AnularFrm : Form
    {


        private Gestion _controlador;


        public AnularFrm()
        {
            InitializeComponent();
        }


        public void setControlador(Gestion ctr)
        {
            _controlador = ctr;
        }

        private void AnularFrm_Load(object sender, EventArgs e)
        {
            TB_MOTIVO.Text = _controlador.Motivo;
            TB_MOTIVO.Focus();
        }

        private void BT_PROCESAR_Click(object sender, EventArgs e)
        {
            Procesar();
        }

        private void Procesar()
        {
            _controlador.Procesar();
            if (_controlador.ProcesarIsOK)
            {
                Salir();
            }
        }

        private void BT_SALIR_Click(object sender, EventArgs e)
        {
            Abandonar();
        }

        private void Abandonar()
        {
            _controlador.Abandonar();
            if (_controlador.AbandonarIsOK)
            {
                Salir();
            }
        }

        private void Salir()
        {
            this.Close();
        }

        private void Ctr_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void TB_MOTIVO_Leave(object sender, EventArgs e)
        {
            _controlador.setMotivo(TB_MOTIVO.Text.Trim());
        }

        private void AnularFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            if (_controlador.AbandonarIsOK || _controlador.ProcesarIsOK)
            {
                e.Cancel = false;
            }
        }

    }

}