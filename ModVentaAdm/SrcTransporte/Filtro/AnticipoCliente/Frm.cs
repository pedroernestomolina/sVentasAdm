using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.SrcTransporte.Filtro.AnticipoCliente
{
    public partial class Frm : Form
    {
        private Vistas.IFiltro _controlador;


        private void InicializaCB() 
        {
            CB_ESTATUS.DisplayMember = "desc";
            CB_ESTATUS.ValueMember = "id";
            CB_ALIADO.DisplayMember = "desc";
            CB_ALIADO.ValueMember = "id";
        }
        public Frm()
        {
            InitializeComponent();
            InicializaCB();
        }
        private bool _modoInicializar = false;
        private void Frm_Load(object sender, EventArgs e)
        {
            _modoInicializar = true;
            CB_ESTATUS.DataSource = _controlador.HndFiltro.Get_EstatusSource;
            CB_ALIADO.DataSource = _controlador.HndFiltro.Get_ClienteSource;
            TB_ALIADO.Text = _controlador.HndFiltro.GetCliente_TextoBuscar;
            CB_ESTATUS.SelectedValue = _controlador.HndFiltro.Get_EstatusById;
            CB_ALIADO.SelectedValue = _controlador.HndFiltro.Get_ClienteById;
            _modoInicializar = false;
        }
        private void CTR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }
        public void setControlador(Vistas.IFiltro ctr)
        {
            _controlador = ctr;
        }


        private void TB_ALIADO_Leave(object sender, EventArgs e)
        {
            _controlador.HndFiltro.setClienteBuscar(TB_ALIADO.Text.Trim().ToUpper());
        }
        private void CB_ESTATUS_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_modoInicializar) { return; }
            _controlador.HndFiltro.setEstatusById("");
            if (CB_ESTATUS.SelectedIndex != -1) 
            {
                _controlador.HndFiltro.setEstatusById(CB_ESTATUS.SelectedValue.ToString());
            }
        }
        private void CB_ALIADO_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_modoInicializar) { return; }
            _controlador.HndFiltro.setClienteById("");
            if (CB_ALIADO.SelectedIndex != -1)
            {
                _controlador.HndFiltro.setClienteById(CB_ALIADO.SelectedValue.ToString());
            }
        }
        private void L_ESTATUS_DOC_Click(object sender, EventArgs e)
        {
            CB_ESTATUS.SelectedIndex = -1;
        }
        private void L_ALIADO_Click(object sender, EventArgs e)
        {
            CB_ALIADO.SelectedIndex = -1;
        }


        private void BT_FILTRAR_Click(object sender, EventArgs e)
        {
            ProcesarFiltros();
        }
        private void BT_SALIR_Click(object sender, EventArgs e)
        {
            AbandonarFicha();
        }


        private void ProcesarFiltros()
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
    }
}