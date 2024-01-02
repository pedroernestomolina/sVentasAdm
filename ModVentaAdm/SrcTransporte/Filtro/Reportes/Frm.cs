using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.SrcTransporte.Filtro.Reportes
{
    public partial class Frm : Form
    {
        private Vistas.IFiltro _controlador;


        private void InicializaCB() 
        {
            CB_ALIADO.DisplayMember = "desc";
            CB_ALIADO.ValueMember = "id";
            CB_CLIENTE.DisplayMember = "desc";
            CB_CLIENTE.ValueMember = "id";
            CB_ESTATUS .DisplayMember = "desc";
            CB_ESTATUS.ValueMember = "id";
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
            CB_ALIADO.DataSource = _controlador.HndFiltro.Aliado.GetSource;
            CB_CLIENTE.DataSource = _controlador.HndFiltro.Cliente.GetSource;
            CB_ESTATUS.DataSource = _controlador.HndFiltro.EstatusDoc.GetSource;
            TB_ALIADO.Text = _controlador.HndFiltro.Aliado.Get_TextoBuscar;
            TB_CLIENTE.Text = _controlador.HndFiltro.Cliente.Get_TextoBuscar;
            CB_ALIADO.SelectedValue = _controlador.HndFiltro.Aliado.GetId;
            CB_CLIENTE.SelectedValue = _controlador.HndFiltro.Cliente.GetId;
            CB_ESTATUS.SelectedValue = _controlador.HndFiltro.EstatusDoc.GetId;
            DTP_DESDE.ShowCheckBox= _controlador.ActivarFiltroPor.PorEntreFechas.MostrarCheck;
            DTP_DESDE.Checked = _controlador.HndFiltro.Desde.IsActiva;
            DTP_DESDE.Value = _controlador.HndFiltro.Desde.Fecha;
            DTP_HASTA.ShowCheckBox = _controlador.ActivarFiltroPor.PorEntreFechas.MostrarCheck;
            DTP_HASTA.Checked = _controlador.HndFiltro.Hasta.IsActiva;
            DTP_HASTA.Value = _controlador.HndFiltro.Hasta.Fecha;
            P_ALIADO.Enabled = _controlador.ActivarFiltroPor.PorAliado;
            P_CLIENTE.Enabled = _controlador.ActivarFiltroPor.PorCliente;
            P_ESTATUS_DOC.Enabled = _controlador.ActivarFiltroPor.PorEstatusDoc;
            P_ENTRE_FECHAS.Enabled = _controlador.ActivarFiltroPor.PorEntreFechas.Activar;
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
            _controlador.HndFiltro.Aliado.setTextoBuscar(TB_ALIADO.Text.Trim().ToUpper());
        }
        private void TB_CLIENTE_Leave(object sender, EventArgs e)
        {
            _controlador.HndFiltro.Cliente.setTextoBuscar(TB_CLIENTE.Text.Trim().ToUpper());
        }
        private void CB_ALIADO_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_modoInicializar) { return; }
            _controlador.HndFiltro.Aliado.setFichaById("");
            if (CB_ALIADO.SelectedIndex != -1)
            {
                _controlador.HndFiltro.Aliado.setFichaById(CB_ALIADO.SelectedValue.ToString());
            }
        }
        private void CB_CLIENTE_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_modoInicializar) { return; }
            _controlador.HndFiltro.Cliente.setFichaById("");
            if (CB_CLIENTE.SelectedIndex != -1)
            {
                _controlador.HndFiltro.Cliente.setFichaById(CB_CLIENTE.SelectedValue.ToString());
            }
        }
        private void CB_ESTATUS_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_modoInicializar) { return; }
            _controlador.HndFiltro.EstatusDoc.setFichaById("");
            if (CB_ESTATUS.SelectedIndex != -1)
            {
                _controlador.HndFiltro.EstatusDoc.setFichaById(CB_ESTATUS.SelectedValue.ToString());
            }
        }
        private void DTP_DESDE_ValueChanged(object sender, EventArgs e)
        {
            if (_modoInicializar) { return; }
            _controlador.HndFiltro.Desde.setActivar(DTP_DESDE.Checked);
            if (DTP_DESDE.Checked) 
            {
                _controlador.HndFiltro.Desde.setFecha(DTP_DESDE.Value);
            }
        }
        private void DTP_HASTA_ValueChanged(object sender, EventArgs e)
        {
            if (_modoInicializar) { return; }
            _controlador.HndFiltro.Hasta.setActivar(DTP_HASTA.Checked);
            if (DTP_HASTA.Checked)
            {
                _controlador.HndFiltro.Hasta.setFecha(DTP_HASTA.Value);
            }
        }
        private void L_ALIADO_Click(object sender, EventArgs e)
        {
            CB_ALIADO.SelectedIndex = -1;
        }
        private void L_CLIENTE_Click(object sender, EventArgs e)
        {
            CB_CLIENTE.SelectedIndex = -1;
        }
        private void L_ESTATUS_DOC_Click(object sender, EventArgs e)
        {
            CB_ESTATUS.SelectedIndex = -1;
        }
        private void L_ENTRA_FECHAS_Click(object sender, EventArgs e)
        {
            DTP_DESDE.Value = DateTime.Now.Date;
            DTP_HASTA.Value = DateTime.Now.Date;
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